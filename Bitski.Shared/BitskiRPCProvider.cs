using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UnityEngine;
using UnityEngine.Networking;
using RpcRequest = Nethereum.JsonRpc.Client.RpcRequest;

namespace Bitski.Rpc {
    [JsonObject]
    public class JSONRpcRequest
    {
        public JSONRpcRequest(object id, string method, params object[] parameterList)
        {
            this.Id = id;
            this.JsonRpcVersion = "2.0";
            this.Method = method;
            this.RawParameters = parameterList;
        }

        [JsonProperty("id")]
        public object Id { get; private set; }

        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string JsonRpcVersion { get; private set; }

        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; private set; }

        [JsonProperty("params")]
        [JsonConverter(typeof(JSONRpcParametersJsonConverter))]
        public object RawParameters { get; private set; }

    }

    [JsonObject]
    public class JSONRpcError
    {
        [JsonConstructor]
        public JSONRpcError()
        {
        }

        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; private set; }

        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; private set; }

        [JsonProperty("data")]
        public JToken Data { get; private set; }
    }

    [JsonObject]
    public class JSONRpcResponse
    {
        [JsonConstructor]
        protected JSONRpcResponse()
        {
            JsonRpcVersion = "2.0";
        }

        public JSONRpcResponse(object id, JSONRpcError error) : this(id)
        {
            this.Error = error;
        }

        public JSONRpcResponse(object id)
        {
            this.Id = id;
            JsonRpcVersion = "2.0";
        }

        public JSONRpcResponse(object id, JToken result) : this(id)
        {
            this.Result = result;
        }

        [JsonProperty("id", Required = Required.AllowNull)]
        public object Id { get; private set; }


        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string JsonRpcVersion { get; private set; }

        [JsonProperty("result", Required = Required.Default)] //TODO somehow enforce this or an error, not both
        public JToken Result { get; private set; }

        [JsonProperty("error", Required = Required.Default)]
        public JSONRpcError Error { get; private set; }

        [JsonIgnore]
        public bool HasError { get { return this.Error != null; } }
    }



    static class RpcResponseExtensions
    {
        public static T GetResult<T>(this JSONRpcResponse response, bool returnDefaultIfNull = true, JsonSerializerSettings settings = null)
        {
            if (response.Result == null)
            {
                if (!returnDefaultIfNull && default(T) != null)
                {
                    throw new Exception("Unable to convert the result (null) to type " + typeof(T));
                }
                return default(T);
            }
            try
            {
                if (settings == null)
                {
                    return response.Result.ToObject<T>();
                }
                else
                {
                    JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
                    return response.Result.ToObject<T>(jsonSerializer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to convert the result to type " + typeof(T), ex);
            }
        }
    }

    internal class JSONRpcParametersJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    try
                    {
                        JObject jObject = JObject.Load(reader);
                        return jObject.ToObject<Dictionary<string, object>>();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Request parameters can only be an associative array, list or null.");
                    }
                case JsonToken.StartArray:
                    return JArray.Load(reader).ToObject<object[]>(serializer);
                case JsonToken.Null:
                    return null;
            }
            throw new Exception("Request parameters can only be an associative array, list or null.");
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }


    public class UnityRpcClient<TResult> : Nethereum.JsonRpc.UnityClient.UnityRequest<TResult>
    {
        private readonly string url = "https://api.bitski.com/v1/web3/kovan";

        private readonly string accessToken;

        public UnityRpcClient(string accessToken, JsonSerializerSettings jsonSerializerSettings = null)
        {
            this.accessToken = accessToken;
        }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public IEnumerator SendRequest(RpcRequest request)
        {
            var requestFormatted = new JSONRpcRequest(request.Id, request.Method, request.RawParameters);
            var rpcRequestJson = JsonConvert.SerializeObject(requestFormatted, JsonSerializerSettings);
            var requestBytes = Encoding.UTF8.GetBytes(rpcRequestJson);
            Debug.Log("Request json: " + rpcRequestJson);

            var unityRequest = new UnityWebRequest(url, "POST");
            var uploadHandler = new UploadHandlerRaw(requestBytes);
            unityRequest.SetRequestHeader("Content-Type", "application/json");
            unityRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);
            uploadHandler.contentType = "application/json";
            unityRequest.uploadHandler = uploadHandler;

            unityRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityRequest.SendWebRequest();

            if (unityRequest.error != null)
            {
                Debug.Log("Got an exception: " + unityRequest.error);
                this.Exception = new Exception(unityRequest.error);
            }
            else
            {
                try
                {
                    byte[] results = unityRequest.downloadHandler.data;
                    var responseJson = Encoding.UTF8.GetString(results);
                    Debug.Log("Response json: " + responseJson);
                    var responseObject = JsonConvert.DeserializeObject<JSONRpcResponse>(responseJson, JsonSerializerSettings);
                    this.Result = responseObject.GetResult<TResult>();
                    //this.Exception = HandleRpcError(responseObject);
                }
                catch (Exception ex)
                {
                    Debug.Log("Got an exception: " + ex.Message);
                    this.Exception = new Exception(ex.Message);
                }
            }
        }

    }

    public class EthCallBitskiUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthCall _ethCall;

        public EthCallBitskiUnityRequest(string accessToken, JsonSerializerSettings jsonSerializerSettings = null) : base(accessToken, jsonSerializerSettings)
        {
            _ethCall = new Nethereum.RPC.Eth.Transactions.EthCall(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.CallInput callInput)
        {
            var request = _ethCall.BuildRequest(null, callInput, "latest");
            yield return base.SendRequest(request);
        }

    }

    public class EthAccountsBitskiUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Eth.EthAccounts _ethAccounts;

        public EthAccountsBitskiUnityRequest(string accessToken, JsonSerializerSettings jsonSerializerSettings = null) : base(accessToken, jsonSerializerSettings)
        {
            _ethAccounts = new Nethereum.RPC.Eth.EthAccounts(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethAccounts.BuildRequest();
            yield return SendRequest(request);
        }
    }
}