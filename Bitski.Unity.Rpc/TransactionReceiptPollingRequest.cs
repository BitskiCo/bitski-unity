using System.Collections;
using Nethereum.RPC.Eth.DTOs;
using UnityEngine;

namespace Bitski.Unity.Rpc
{
    public class TransactionReceiptPollingRequest : UnityRequest<TransactionReceipt>
    {
        private readonly EthGetTransactionReceiptUnityRequest _ethGetTransactionReceipt;
        public bool CancelPolling { get; set; } = false;

        public TransactionReceiptPollingRequest(string networkName)
        {
            _ethGetTransactionReceipt = new EthGetTransactionReceiptUnityRequest(networkName);
        }

        public IEnumerator PollForReceipt(string transactionHash, float secondsToWait)
        {
            TransactionReceipt receipt = null;
            Result = null;
            while (receipt == null)
            {
                if (!CancelPolling)
                {
                    yield return _ethGetTransactionReceipt.SendRequest(transactionHash);

                    if (_ethGetTransactionReceipt.Exception == null)
                    {
                        receipt = _ethGetTransactionReceipt.Result;
                    }
                    else
                    {
                        this.Exception = _ethGetTransactionReceipt.Exception;
                        yield break;
                    }
                }
                else
                {
                    yield break;
                }

                yield return new WaitForSeconds(secondsToWait);
            }

            Result = receipt;
        }
    }
}