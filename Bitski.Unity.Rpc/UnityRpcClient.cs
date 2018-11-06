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

namespace Bitski.Unity.Rpc
{

    public class UnityRpcClient<TResult> : UnityRequest<TResult>
    {
        private readonly String network;

        public UnityRpcClient(String network = "mainnet", JsonSerializerSettings jsonSerializerSettings = null)
        {
            if (jsonSerializerSettings == null)
                jsonSerializerSettings = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            this.network = network;
            //check for nulls
            JsonSerializerSettings = jsonSerializerSettings;
        }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        private RpcResponseException HandleRpcError(RpcResponse response)
        {
            if (response.HasError)
                return new RpcResponseException(new RpcError(response.Error.Code, response.Error.Message,
                    response.Error.Data));
            return null;
        }

        public IEnumerator SendRequest(RpcRequest request)
        {
            var requestFormatted = new RpcModel.RpcRequest(request.Id, request.Method, request.RawParameters);

            var user = BitskiSDK.CurrentUser;

            var rpcRequestJson = JsonConvert.SerializeObject(requestFormatted, JsonSerializerSettings);
            Debug.Log(rpcRequestJson);
            var requestBytes = Encoding.UTF8.GetBytes(rpcRequestJson);
            var url = "https://api.bitski.com/v1/web3/" + network;
            var unityRequest = new UnityWebRequest(url, "POST");
            var uploadHandler = new UploadHandlerRaw(requestBytes);
            unityRequest.SetRequestHeader("Content-Type", "application/json");
            unityRequest.SetRequestHeader("Authorization", "Bearer " + user.AccessToken);
            uploadHandler.contentType = "application/json";
            unityRequest.uploadHandler = uploadHandler;

            unityRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityRequest.SendWebRequest();

            if (unityRequest.error != null)
            {
                this.Exception = new Exception(unityRequest.error);
                Debug.Log(unityRequest.error);
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
                    Debug.Log(ex.Message);
                }
            }
        }
    }
}