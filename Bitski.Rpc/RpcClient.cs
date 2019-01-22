using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bitski;
using Bitski.Auth;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using UnityEngine;

using Newtonsoft.Json;
namespace Bitski.Rpc
{
    public class RpcClient : ClientBase
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly HttpClient httpClient;

        private readonly AuthProvider authProvider;

        public RpcClient(String network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            if (!BitskiSDK.IsInitialized) {
                throw new Exception("Initialize the Bitski SDK first.");
            }

            if (authProvider == null)
                authProvider = BitskiSDK.AuthProviderImpl;
            this.authProvider = authProvider;

            if (jsonSerializerSettings == null)
                jsonSerializerSettings = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            this.jsonSerializerSettings = jsonSerializerSettings;

            this.httpClient = new HttpClient();
            if (network == "ganache") {
                this.httpClient.BaseAddress = new Uri("http://127.0.0.1:7545");
            } else {
                this.httpClient.BaseAddress = new Uri("https://api.bitski.com/v1/web3/" + network);
            }
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-API-Key", authProvider.ClientId);
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Client-Id", authProvider.ClientId);
            this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authProvider.CurrentUser.AccessToken);
        }

        protected override async Task<RpcResponseMessage> SendAsync(RpcRequestMessage request, string route = null)
        {
            try
            {
                var rpcRequestJson = JsonConvert.SerializeObject(request, this.jsonSerializerSettings);
                Debug.Log(rpcRequestJson);
                var httpContent = new StringContent(rpcRequestJson, Encoding.UTF8, "application/json");

                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(ConnectionTimeout);

                var httpResponseMessage = await this.httpClient.PostAsync(route, httpContent, cancellationTokenSource.Token)
                    .ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();

                var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                using (var streamReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(streamReader))
                {
                    var serializer = JsonSerializer.Create(this.jsonSerializerSettings);
                    var message = serializer.Deserialize<RpcResponseMessage>(reader);

                    return message;
                }
            }
            catch (TaskCanceledException ex)
            {
                throw new RpcClientTimeoutException($"Rpc timeout after {ConnectionTimeout.TotalMilliseconds} milliseconds", ex);
            }
            catch (Exception ex)
            {
                throw new RpcClientUnknownException("Error occurred when trying to send rpc requests(s)", ex);
            }
        }
    }
}
