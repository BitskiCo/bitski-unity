using Common.Logging;
using Nethereum.JsonRpc.Client.RpcMessages;
//using Nethereum.JsonRpc.Client;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

namespace Bitski.Rpc
{
#if UNITY_EDITOR
    public class BitskiRpcClient : Nethereum.JsonRpc.Client.ClientBase
    {
        private readonly AuthProvider authProvider;

        private const int NUMBER_OF_SECONDS_TO_RECREATE_HTTP_CLIENT = 60;
        private readonly Uri _baseUrl;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly ILog _log;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private volatile bool _firstHttpClient;
        private HttpClient _httpClient;
        private HttpClient _httpClient2;
        private DateTime _httpClientLastCreatedAt;
        private readonly object _lockObject = new object();

        public BitskiRpcClient(AuthProvider authProvider, string networkName)
        {
            this.authProvider = authProvider;
            _baseUrl = new Uri("https://api.bitski.com/v1/web3/" + networkName);
            CreateNewHttpClient();
        }

        protected override async Task<T> SendInnerRequestAync<T>(Nethereum.JsonRpc.Client.RpcRequest request, string route = null)
        {
            var response =
                await SendAsync(
                        new RpcRequestMessage(request.Id, request.Method, request.RawParameters), route)
                    .ConfigureAwait(false);
            HandleRpcError(response);
            return response.GetResult<T>();
        }

        protected override async Task<T> SendInnerRequestAync<T>(string method, string route = null,
            params object[] paramList)
        {
            var request = new RpcRequestMessage(Guid.NewGuid().ToString(), method, paramList);
            var response = await SendAsync(request, route).ConfigureAwait(false);
            HandleRpcError(response);
            return response.GetResult<T>();
        }

        private void HandleRpcError(RpcResponseMessage response)
        {
            if (response.HasError)
            {
                Debug.Log("Throwing error: " + response);
                throw new Nethereum.JsonRpc.Client.RpcResponseException(new Nethereum.JsonRpc.Client.RpcError(response.Error.Code, response.Error.Message,
    response.Error.Data));
            }
        }

        public override async Task SendRequestAsync(Nethereum.JsonRpc.Client.RpcRequest request, string route = null)
        {
            var response =
                await SendAsync(
                        new RpcRequestMessage(request.Id, request.Method, request.RawParameters), route)
                    .ConfigureAwait(false);
            HandleRpcError(response);
        }

        public override async Task SendRequestAsync(string method, string route = null, params object[] paramList)
        {
            var request = new RpcRequestMessage(Guid.NewGuid().ToString(), method, paramList);
            var response = await SendAsync(request, route).ConfigureAwait(false);
            HandleRpcError(response);
        }

        private async Task<RpcResponseMessage> SendAsync(RpcRequestMessage request, string route = null)
        {
            var user = await authProvider.GetUser();

            var logger = new Nethereum.JsonRpc.Client.RpcLogger(_log);
            try
            {
                var httpClient = GetOrCreateHttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

                var rpcRequestJson = JsonConvert.SerializeObject(request, _jsonSerializerSettings);
                var httpContent = new StringContent(rpcRequestJson, Encoding.UTF8, "application/json");
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(ConnectionTimeout));

                logger.LogRequest(rpcRequestJson);
                Debug.Log("Request: " + rpcRequestJson);

                var httpResponseMessage = await httpClient.PostAsync(route, httpContent, cancellationTokenSource.Token).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();

                var body = await httpResponseMessage.Content.ReadAsStreamAsync();
                using (var streamReader = new StreamReader(body))
                {
                    var content = streamReader.ReadToEnd();
                    Debug.Log("Response: " + content);
                }

                var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                using (var streamReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(streamReader))
                {
                    var serializer = JsonSerializer.Create(_jsonSerializerSettings);
                    var message = serializer.Deserialize<RpcResponseMessage>(reader);

                    logger.LogResponse(message);

                    return message;
                }
            }
            catch (TaskCanceledException ex)
            {
                throw new Nethereum.JsonRpc.Client.RpcClientTimeoutException($"Rpc timeout afer {ConnectionTimeout} milliseconds", ex);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                throw new Nethereum.JsonRpc.Client.RpcClientUnknownException("Error occurred when trying to send rpc requests(s)", ex);
            }
        }



        private HttpClient GetOrCreateHttpClient()
        {
            lock (_lockObject)
            {
                var timeSinceCreated = DateTime.UtcNow - _httpClientLastCreatedAt;
                if (timeSinceCreated.TotalSeconds > NUMBER_OF_SECONDS_TO_RECREATE_HTTP_CLIENT)
                    CreateNewHttpClient();
                return GetClient();
            }
        }

        private HttpClient GetClient()
        {
            lock (_lockObject)
            {
                return _firstHttpClient ? _httpClient : _httpClient2;
            }
        }

        private void CreateNewHttpClient()
        {
            var httpClient = _httpClientHandler != null ? new HttpClient(_httpClientHandler) : new HttpClient();
            httpClient.BaseAddress = _baseUrl;
            _httpClientLastCreatedAt = DateTime.UtcNow;
            if (_firstHttpClient)
                lock (_lockObject)
                {
                    _firstHttpClient = false;
                    _httpClient2 = httpClient;
                }
            else
                lock (_lockObject)
                {
                    _firstHttpClient = true;
                    _httpClient = httpClient;
                }
        }
    }
#endif
}