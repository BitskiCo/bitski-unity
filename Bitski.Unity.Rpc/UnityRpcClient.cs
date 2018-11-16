using System;
using System.Text;
using Bitski.Unity.RpcModel;
using Nethereum.JsonRpc.Client;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using RpcError = Nethereum.JsonRpc.Client.RpcError;
using RpcRequest = Nethereum.JsonRpc.Client.RpcRequest;
using Bitski.Auth;
using Bitski;

namespace Bitski.Unity.Rpc
{

    public class UnityRpcClient<TResult> : UnityRequest<TResult>
    {
        internal readonly String network;
        internal readonly AuthProvider authProvider;


        public UnityRpcClient(String network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            this.authProvider = authProvider ?? BitskiSDK.AuthProviderImpl;
            if (jsonSerializerSettings == null)
                jsonSerializerSettings = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            this.network = network;
            //check for nulls
            JsonSerializerSettings = jsonSerializerSettings;
        }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        internal RpcResponseException HandleRpcError(RpcResponse response)
        {
            if (response.HasError)
                return new RpcResponseException(new RpcError(response.Error.Code, response.Error.Message,
                    response.Error.Data));
            return null;
        }

        public String URLForNetwork(String network) {
            if (network == "ganache") {
                return "http://localhost:9545";
            }

            var url = "https://api.bitski.com/v1/web3/" + network;
            return url;
        }

        public IEnumerator SendRequest(RpcRequest request)
        {
            var requestFormatted = new RpcModel.RpcRequest(request.Id, request.Method, request.RawParameters);
            var rpcRequestJson = JsonConvert.SerializeObject(requestFormatted, JsonSerializerSettings);
            Debug.Log(rpcRequestJson);
            var requestBytes = Encoding.UTF8.GetBytes(rpcRequestJson);
            var url = URLForNetwork(network);
            Debug.Log(url);
            var unityRequest = new UnityWebRequest(url, "POST");
            var uploadHandler = new UploadHandlerRaw(requestBytes);
            unityRequest.SetRequestHeader("Content-Type", "application/json");
            if (authProvider.CurrentUser != null && authProvider.CurrentUser.AccessToken != null) {
                Debug.Log("Auth token: authProvider.CurrentUser.AccessToken");
                unityRequest.SetRequestHeader("Authorization", "Bearer " + authProvider.CurrentUser.AccessToken);
            } else {
                Debug.Log("Not signed in, anonymous request");
            }
            uploadHandler.contentType = "application/json";
            unityRequest.uploadHandler = uploadHandler;

            unityRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityRequest.SendWebRequest();

            if (unityRequest.error != null)
            {
                this.Exception = new Exception(unityRequest.error);
            }
            else
            {
                try
                {
                    byte[] results = unityRequest.downloadHandler.data;
                    var responseJson = Encoding.UTF8.GetString(results);
                    Debug.Log(responseJson);
                    var responseObject = JsonConvert.DeserializeObject<RpcResponse>(responseJson, JsonSerializerSettings);
                    this.Result = responseObject.GetResult<TResult>(true, JsonSerializerSettings);
                    this.Exception = HandleRpcError(responseObject);
                }
                catch (Exception ex)
                {
                    this.Exception = new Exception(ex.Message);
                }
            }
        }
    }
}