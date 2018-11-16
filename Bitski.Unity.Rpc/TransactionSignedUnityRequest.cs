using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.Hex.HexTypes;
using System.Collections;
using System;
using Nethereum.Contracts;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Extensions;

namespace Bitski.Unity.Rpc
{
    public class TransactionSignedUnityRequest:UnityRequest<string>
    {
        private string _network;
        private string _account;
        private readonly EthSendTransactionUnityRequest _ethSendTransactionRequest;
        private readonly EthEstimateGasUnityRequest _ethEstimateGasUnityRequest;
        public bool EstimateGas { get; set; } = true;

        public TransactionSignedUnityRequest(string network, string account)
        {
            _network = network;
            _account = account;
            _ethSendTransactionRequest = new EthSendTransactionUnityRequest(_network);
            _ethEstimateGasUnityRequest = new EthEstimateGasUnityRequest(network);
        }

        public IEnumerator SignAndSendTransaction<TContractFunction>(TContractFunction function, string contractAdress) where TContractFunction : FunctionMessage
        {
            var transactionInput = function.CreateTransactionInput(contractAdress);
            yield return SignAndSendTransaction(transactionInput);
        }

        public IEnumerator SignAndSendDeploymentContractTransaction<TDeploymentMessage>(TDeploymentMessage deploymentMessage)
            where TDeploymentMessage : ContractDeploymentMessage
        {
            var transactionInput = deploymentMessage.CreateTransactionInput();
            yield return SignAndSendTransaction(transactionInput);
        }

        public IEnumerator SignAndSendDeploymentContractTransaction<TDeploymentMessage>()
            where TDeploymentMessage : ContractDeploymentMessage, new()
        {
            var deploymentMessage = new TDeploymentMessage();
            yield return SignAndSendDeploymentContractTransaction(deploymentMessage);
        }

        public IEnumerator SignAndSendTransaction(TransactionInput transactionInput)
        {
            if (transactionInput == null) throw new ArgumentNullException("transactionInput");

            if (transactionInput.Gas == null)
            {
                if (EstimateGas)
                {
                    yield return _ethEstimateGasUnityRequest.SendRequest(transactionInput);

                    if (_ethEstimateGasUnityRequest.Exception == null)
                    {
                        var gas = _ethEstimateGasUnityRequest.Result;
                        transactionInput.Gas = gas;
                    }
                    else
                    {
                        this.Exception = _ethEstimateGasUnityRequest.Exception;
                        yield break;
                    }
                }
                else
                {
                    transactionInput.Gas = new HexBigInteger(new System.Numerics.BigInteger(21000));
                }
            }

            if (transactionInput.Value == null)
                transactionInput.Value = new HexBigInteger(0);

            if (transactionInput.From == null)
                transactionInput.From = _account;

            yield return _ethSendTransactionRequest.SendRequest(transactionInput);
                        
            if(_ethSendTransactionRequest.Exception == null) 
            {
                this.Result = _ethSendTransactionRequest.Result;
            }
            else
            {
                this.Exception = _ethSendTransactionRequest.Exception;
                yield break;
            }
        }
    }
}