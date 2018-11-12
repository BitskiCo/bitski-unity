using Nethereum.RPC.Eth.DTOs;
using System.Collections;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Extensions;
using Bitski.Auth;

namespace Bitski.Unity.Rpc
{
    public class QueryUnityRequest<TFunctionMessage, TResponse> : UnityRequest<TResponse>
        where TFunctionMessage : FunctionMessage, new()
        where TResponse : IFunctionOutputDTO, new()
    {
        private readonly EthCallUnityRequest _ethCallUnityRequest;
        public string DefaultAccount { get; set; }

        public QueryUnityRequest(string defaultAccount, AuthProvider authProvider, string networkName = "mainnet")
        {
            DefaultAccount = defaultAccount;
            _ethCallUnityRequest = new EthCallUnityRequest(authProvider, networkName);
        }

        public IEnumerator Query(TFunctionMessage functionMessage, string contractAddress, BlockParameter blockParameter = null)
        {
            if (blockParameter == null) blockParameter = BlockParameter.CreateLatest();

            functionMessage.SetDefaultFromAddressIfNotSet(DefaultAccount);
            var callInput = functionMessage.CreateCallInput(contractAddress);

            yield return _ethCallUnityRequest.SendRequest(callInput, blockParameter);

            if (_ethCallUnityRequest.Exception == null)
            {
                var result = new TResponse();
                Result = result.DecodeOutput(_ethCallUnityRequest.Result);
            }
            else
            {
                this.Exception = _ethCallUnityRequest.Exception;
                yield break;
            }
        }

        public IEnumerator Query(string contractAddress, BlockParameter blockParameter = null)
        {
            yield return Query(new TFunctionMessage(), contractAddress, blockParameter);
        }
    }
}