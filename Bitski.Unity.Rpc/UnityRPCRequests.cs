﻿namespace Bitski.Unity.Rpc
{
    using Newtonsoft.Json;
    using System.Collections;
    using Bitski.Auth;
    using Bitski;
    using Bitski.Unity.RpcModel;
    using UnityEngine;
    public class Web3ClientVersionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Web3.Web3ClientVersion _web3ClientVersion;

        public Web3ClientVersionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _web3ClientVersion = new Nethereum.RPC.Web3.Web3ClientVersion(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _web3ClientVersion.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class Web3Sha3UnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Web3.Web3Sha3 _web3Sha3;

        public Web3Sha3UnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _web3Sha3 = new Nethereum.RPC.Web3.Web3Sha3(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexUTF8String valueToConvert)
        {
            var request = _web3Sha3.BuildRequest(valueToConvert);
            yield return SendRequest(request);
        }
    }


    public class ShhNewIdentityUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Shh.ShhNewIdentity _shhNewIdentity;

        public ShhNewIdentityUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _shhNewIdentity = new Nethereum.RPC.Shh.ShhNewIdentity(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _shhNewIdentity.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class ShhVersionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Shh.ShhVersion _shhVersion;

        public ShhVersionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _shhVersion = new Nethereum.RPC.Shh.ShhVersion(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _shhVersion.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class PersonalListAccountsUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Personal.PersonalListAccounts _personalListAccounts;

        public PersonalListAccountsUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _personalListAccounts = new Nethereum.RPC.Personal.PersonalListAccounts(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _personalListAccounts.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class PersonalLockAccountUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Personal.PersonalLockAccount _personalLockAccount;

        public PersonalLockAccountUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _personalLockAccount = new Nethereum.RPC.Personal.PersonalLockAccount(null);
        }

        public IEnumerator SendRequest(System.String account)
        {
            var request = _personalLockAccount.BuildRequest(account);
            yield return SendRequest(request);
        }
    }


    public class PersonalNewAccountUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Personal.PersonalNewAccount _personalNewAccount;

        public PersonalNewAccountUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _personalNewAccount = new Nethereum.RPC.Personal.PersonalNewAccount(null);
        }

        public IEnumerator SendRequest(System.String passPhrase)
        {
            var request = _personalNewAccount.BuildRequest(passPhrase);
            yield return SendRequest(request);
        }
    }


    public class PersonalSignAndSendTransactionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Personal.PersonalSignAndSendTransaction _personalSignAndSendTransaction;

        public PersonalSignAndSendTransactionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _personalSignAndSendTransaction = new Nethereum.RPC.Personal.PersonalSignAndSendTransaction(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.TransactionInput txn, System.String password)
        {
            var request = _personalSignAndSendTransaction.BuildRequest(txn, password);
            yield return SendRequest(request);
        }
    }


    public class PersonalUnlockAccountUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Personal.PersonalUnlockAccount _personalUnlockAccount;

        public PersonalUnlockAccountUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _personalUnlockAccount = new Nethereum.RPC.Personal.PersonalUnlockAccount(null);
        }

        public IEnumerator SendRequest(System.String address, System.String passPhrase, int durationInSeconds)
        {
            var request = _personalUnlockAccount.BuildRequest(address, passPhrase, durationInSeconds);
            yield return SendRequest(request);
        }
    }


    public class NetListeningUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Net.NetListening _netListening;

        public NetListeningUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _netListening = new Nethereum.RPC.Net.NetListening(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _netListening.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class NetPeerCountUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Net.NetPeerCount _netPeerCount;

        public NetPeerCountUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _netPeerCount = new Nethereum.RPC.Net.NetPeerCount(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _netPeerCount.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class NetVersionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Net.NetVersion _netVersion;

        public NetVersionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _netVersion = new Nethereum.RPC.Net.NetVersion(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _netVersion.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthAccountsUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Eth.EthAccounts _ethAccounts;

        public EthAccountsUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethAccounts = new Nethereum.RPC.Eth.EthAccounts(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethAccounts.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthCoinBaseUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.EthCoinBase _ethCoinBase;

        public EthCoinBaseUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethCoinBase = new Nethereum.RPC.Eth.EthCoinBase(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethCoinBase.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthGasPriceUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.EthGasPrice _ethGasPrice;

        public EthGasPriceUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGasPrice = new Nethereum.RPC.Eth.EthGasPrice(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethGasPrice.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthGetBalanceUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.EthGetBalance _ethGetBalance;

        public EthGetBalanceUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBalance = new Nethereum.RPC.Eth.EthGetBalance(null);
        }

        public IEnumerator SendRequest(System.String address, Nethereum.RPC.Eth.DTOs.BlockParameter block)
        {
            var request = _ethGetBalance.BuildRequest(address, block);
            yield return SendRequest(request);
        }
    }


    public class EthGetCodeUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.EthGetCode _ethGetCode;

        public EthGetCodeUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetCode = new Nethereum.RPC.Eth.EthGetCode(null);
        }

        public IEnumerator SendRequest(System.String address, Nethereum.RPC.Eth.DTOs.BlockParameter block)
        {
            var request = _ethGetCode.BuildRequest(address, block);
            yield return SendRequest(request);
        }
    }


    public class EthGetStorageAtUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.EthGetStorageAt _ethGetStorageAt;

        public EthGetStorageAtUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetStorageAt = new Nethereum.RPC.Eth.EthGetStorageAt(null);
        }

        public IEnumerator SendRequest(System.String address, Nethereum.Hex.HexTypes.HexBigInteger position, Nethereum.RPC.Eth.DTOs.BlockParameter block)
        {
            var request = _ethGetStorageAt.BuildRequest(address, position, block);
            yield return SendRequest(request);
        }
    }


    public class EthProtocolVersionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.EthProtocolVersion _ethProtocolVersion;

        public EthProtocolVersionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethProtocolVersion = new Nethereum.RPC.Eth.EthProtocolVersion(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethProtocolVersion.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthSignUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.EthSign _ethSign;

        public EthSignUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethSign = new Nethereum.RPC.Eth.EthSign(null);
        }

        public IEnumerator SendRequest(System.String address, System.String data)
        {
            var request = _ethSign.BuildRequest(address, data);
            yield return SendRequest(request);
        }
    }


    public class EthSyncingUnityRequest : UnityRpcClient<System.Object>
    {
        private readonly Nethereum.RPC.Eth.EthSyncing _ethSyncing;

        public EthSyncingUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethSyncing = new Nethereum.RPC.Eth.EthSyncing(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethSyncing.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthGetUncleByBlockHashAndIndexUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.BlockWithTransactionHashes>
    {
        private readonly Nethereum.RPC.Eth.Uncles.EthGetUncleByBlockHashAndIndex _ethGetUncleByBlockHashAndIndex;

        public EthGetUncleByBlockHashAndIndexUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetUncleByBlockHashAndIndex = new Nethereum.RPC.Eth.Uncles.EthGetUncleByBlockHashAndIndex(null);
        }

        public IEnumerator SendRequest(System.String blockHash, Nethereum.Hex.HexTypes.HexBigInteger uncleIndex)
        {
            var request = _ethGetUncleByBlockHashAndIndex.BuildRequest(blockHash, uncleIndex);
            yield return SendRequest(request);
        }
    }


    public class EthGetUncleByBlockNumberAndIndexUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.BlockWithTransactionHashes>
    {
        private readonly Nethereum.RPC.Eth.Uncles.EthGetUncleByBlockNumberAndIndex _ethGetUncleByBlockNumberAndIndex;

        public EthGetUncleByBlockNumberAndIndexUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetUncleByBlockNumberAndIndex = new Nethereum.RPC.Eth.Uncles.EthGetUncleByBlockNumberAndIndex(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, Nethereum.Hex.HexTypes.HexBigInteger uncleIndex)
        {
            var request = _ethGetUncleByBlockNumberAndIndex.BuildRequest(blockParameter, uncleIndex);
            yield return SendRequest(request);
        }
    }


    public class EthGetUncleCountByBlockHashUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Uncles.EthGetUncleCountByBlockHash _ethGetUncleCountByBlockHash;

        public EthGetUncleCountByBlockHashUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetUncleCountByBlockHash = new Nethereum.RPC.Eth.Uncles.EthGetUncleCountByBlockHash(null);
        }

        public IEnumerator SendRequest(System.String hash)
        {
            var request = _ethGetUncleCountByBlockHash.BuildRequest(hash);
            yield return SendRequest(request);
        }
    }


    public class EthGetUncleCountByBlockNumberUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Uncles.EthGetUncleCountByBlockNumber _ethGetUncleCountByBlockNumber;

        public EthGetUncleCountByBlockNumberUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetUncleCountByBlockNumber = new Nethereum.RPC.Eth.Uncles.EthGetUncleCountByBlockNumber(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger blockNumber)
        {
            var request = _ethGetUncleCountByBlockNumber.BuildRequest(blockNumber);
            yield return SendRequest(request);
        }
    }


    public class EthCallUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthCall _ethCall;

        public EthCallUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethCall = new Nethereum.RPC.Eth.Transactions.EthCall(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.CallInput callInput, Nethereum.RPC.Eth.DTOs.BlockParameter block)
        {
            var request = _ethCall.BuildRequest(callInput, block);
            yield return SendRequest(request);
        }
    }


    public class EthEstimateGasUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthEstimateGas _ethEstimateGas;

        public EthEstimateGasUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethEstimateGas = new Nethereum.RPC.Eth.Transactions.EthEstimateGas(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.CallInput callInput)
        {
            var request = _ethEstimateGas.BuildRequest(callInput);
            yield return SendRequest(request);
        }
    }


    public class EthGetTransactionByBlockHashAndIndexUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.Transaction>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthGetTransactionByBlockHashAndIndex _ethGetTransactionByBlockHashAndIndex;

        public EthGetTransactionByBlockHashAndIndexUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetTransactionByBlockHashAndIndex = new Nethereum.RPC.Eth.Transactions.EthGetTransactionByBlockHashAndIndex(null);
        }

        public IEnumerator SendRequest(System.String blockHash, Nethereum.Hex.HexTypes.HexBigInteger transactionIndex)
        {
            var request = _ethGetTransactionByBlockHashAndIndex.BuildRequest(blockHash, transactionIndex);
            yield return SendRequest(request);
        }
    }


    public class EthGetTransactionByBlockNumberAndIndexUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.Transaction>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthGetTransactionByBlockNumberAndIndex _ethGetTransactionByBlockNumberAndIndex;

        public EthGetTransactionByBlockNumberAndIndexUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetTransactionByBlockNumberAndIndex = new Nethereum.RPC.Eth.Transactions.EthGetTransactionByBlockNumberAndIndex(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger blockNumber, Nethereum.Hex.HexTypes.HexBigInteger transactionIndex)
        {
            var request = _ethGetTransactionByBlockNumberAndIndex.BuildRequest(blockNumber, transactionIndex);
            yield return SendRequest(request);
        }
    }


    public class EthGetTransactionByHashUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.Transaction>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthGetTransactionByHash _ethGetTransactionByHash;

        public EthGetTransactionByHashUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetTransactionByHash = new Nethereum.RPC.Eth.Transactions.EthGetTransactionByHash(null);
        }

        public IEnumerator SendRequest(System.String hashTransaction)
        {
            var request = _ethGetTransactionByHash.BuildRequest(hashTransaction);
            yield return SendRequest(request);
        }
    }


    public class EthGetTransactionCountUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthGetTransactionCount _ethGetTransactionCount;

        public EthGetTransactionCountUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetTransactionCount = new Nethereum.RPC.Eth.Transactions.EthGetTransactionCount(null);
        }

        public IEnumerator SendRequest(System.String address, Nethereum.RPC.Eth.DTOs.BlockParameter block)
        {
            var request = _ethGetTransactionCount.BuildRequest(address, block);
            yield return SendRequest(request);
        }
    }


    public class EthGetTransactionReceiptUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.TransactionReceipt>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthGetTransactionReceipt _ethGetTransactionReceipt;

        public EthGetTransactionReceiptUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetTransactionReceipt = new Nethereum.RPC.Eth.Transactions.EthGetTransactionReceipt(null);
        }

        public IEnumerator SendRequest(System.String transactionHash)
        {
            var request = _ethGetTransactionReceipt.BuildRequest(transactionHash);
            yield return SendRequest(request);
        }
    }


    public class EthSendRawTransactionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthSendRawTransaction _ethSendRawTransaction;

        public EthSendRawTransactionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethSendRawTransaction = new Nethereum.RPC.Eth.Transactions.EthSendRawTransaction(null);
        }

        public IEnumerator SendRequest(System.String signedTransactionData)
        {
            var request = _ethSendRawTransaction.BuildRequest(signedTransactionData);
            yield return SendRequest(request);
        }
    }


    public class EthSendTransactionUnityRequest : UnityRpcClient<System.String>
    {
        private readonly Nethereum.RPC.Eth.Transactions.EthSendTransaction _ethSendTransaction;

        public EthSendTransactionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethSendTransaction = new Nethereum.RPC.Eth.Transactions.EthSendTransaction(null);
        }

        public void SendRequestCallback(string result)
        {
            Debug.Log(result);
            var responseObject = JsonConvert.DeserializeObject<RpcResponse>(result, JsonSerializerSettings);
            this.Result = responseObject.GetResult<System.String>(true, JsonSerializerSettings);
            this.Exception = HandleRpcError(responseObject);       
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.TransactionInput input)
        {
            var request = _ethSendTransaction.BuildRequest(input);

            if (this.network == "ganache") {
                Debug.Log("Using ganache, just forwarding request on...");
                yield return base.SendRequest(request);
                yield break;
            }

            var requestFormatted = new RpcModel.RpcRequest(request.Id, request.Method, request.RawParameters);
            var rpcRequestJson = JsonConvert.SerializeObject(requestFormatted, JsonSerializerSettings);
            Debug.Log(rpcRequestJson);
            this.authProvider.SendTransaction(this.network, request.Method, rpcRequestJson, SendRequestCallback);

            var waitCount = 0;

            while (this.Result == null && this.Exception == null) {
                if (waitCount > 100) {
                    break;
                }
                waitCount++;
                yield return new WaitForSeconds((float)0.1);
            }
        }
    }


    public class EthGetWorkUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Eth.Mining.EthGetWork _ethGetWork;

        public EthGetWorkUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetWork = new Nethereum.RPC.Eth.Mining.EthGetWork(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethGetWork.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthHashrateUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Mining.EthHashrate _ethHashrate;

        public EthHashrateUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethHashrate = new Nethereum.RPC.Eth.Mining.EthHashrate(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethHashrate.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthMiningUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Eth.Mining.EthMining _ethMining;

        public EthMiningUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethMining = new Nethereum.RPC.Eth.Mining.EthMining(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethMining.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthSubmitHashrateUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Eth.Mining.EthSubmitHashrate _ethSubmitHashrate;

        public EthSubmitHashrateUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethSubmitHashrate = new Nethereum.RPC.Eth.Mining.EthSubmitHashrate(null);
        }

        public IEnumerator SendRequest(System.String hashRate, System.String clientId)
        {
            var request = _ethSubmitHashrate.BuildRequest(hashRate, clientId);
            yield return SendRequest(request);
        }
    }


    public class EthSubmitWorkUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Eth.Mining.EthSubmitWork _ethSubmitWork;

        public EthSubmitWorkUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethSubmitWork = new Nethereum.RPC.Eth.Mining.EthSubmitWork(null);
        }

        public IEnumerator SendRequest(System.String nonce, System.String header, System.String mix)
        {
            var request = _ethSubmitWork.BuildRequest(nonce, header, mix);
            yield return SendRequest(request);
        }
    }


    public class EthGetFilterChangesForEthNewFilterUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.FilterLog[]>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthGetFilterChangesForEthNewFilter _ethGetFilterChangesForEthNewFilter;

        public EthGetFilterChangesForEthNewFilterUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetFilterChangesForEthNewFilter = new Nethereum.RPC.Eth.Filters.EthGetFilterChangesForEthNewFilter(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            var request = _ethGetFilterChangesForEthNewFilter.BuildRequest(filterId);
            yield return SendRequest(request);
        }
    }


    public class EthGetFilterChangesForBlockOrTransactionUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthGetFilterChangesForBlockOrTransaction _ethGetFilterChangesForBlockOrTransaction;

        public EthGetFilterChangesForBlockOrTransactionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetFilterChangesForBlockOrTransaction = new Nethereum.RPC.Eth.Filters.EthGetFilterChangesForBlockOrTransaction(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            var request = _ethGetFilterChangesForBlockOrTransaction.BuildRequest(filterId);
            yield return SendRequest(request);
        }
    }


    public class EthGetFilterLogsForBlockOrTransactionUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthGetFilterLogsForBlockOrTransaction _ethGetFilterLogsForBlockOrTransaction;

        public EthGetFilterLogsForBlockOrTransactionUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetFilterLogsForBlockOrTransaction = new Nethereum.RPC.Eth.Filters.EthGetFilterLogsForBlockOrTransaction(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            var request = _ethGetFilterLogsForBlockOrTransaction.BuildRequest(filterId);
            yield return SendRequest(request);
        }
    }


    public class EthGetFilterLogsForEthNewFilterUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.FilterLog[]>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthGetFilterLogsForEthNewFilter _ethGetFilterLogsForEthNewFilter;

        public EthGetFilterLogsForEthNewFilterUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetFilterLogsForEthNewFilter = new Nethereum.RPC.Eth.Filters.EthGetFilterLogsForEthNewFilter(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            var request = _ethGetFilterLogsForEthNewFilter.BuildRequest(filterId);
            yield return SendRequest(request);
        }
    }


    public class EthGetLogsUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.FilterLog[]>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthGetLogs _ethGetLogs;

        public EthGetLogsUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetLogs = new Nethereum.RPC.Eth.Filters.EthGetLogs(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.NewFilterInput newFilter)
        {
            var request = _ethGetLogs.BuildRequest(newFilter);
            yield return SendRequest(request);
        }
    }


    public class EthNewBlockFilterUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthNewBlockFilter _ethNewBlockFilter;

        public EthNewBlockFilterUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethNewBlockFilter = new Nethereum.RPC.Eth.Filters.EthNewBlockFilter(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethNewBlockFilter.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthNewFilterUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthNewFilter _ethNewFilter;

        public EthNewFilterUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethNewFilter = new Nethereum.RPC.Eth.Filters.EthNewFilter(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.NewFilterInput newFilterInput)
        {
            var request = _ethNewFilter.BuildRequest(newFilterInput);
            yield return SendRequest(request);
        }
    }


    public class EthNewPendingTransactionFilterUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthNewPendingTransactionFilter _ethNewPendingTransactionFilter;

        public EthNewPendingTransactionFilterUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethNewPendingTransactionFilter = new Nethereum.RPC.Eth.Filters.EthNewPendingTransactionFilter(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethNewPendingTransactionFilter.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthUninstallFilterUnityRequest : UnityRpcClient<System.Boolean>
    {
        private readonly Nethereum.RPC.Eth.Filters.EthUninstallFilter _ethUninstallFilter;

        public EthUninstallFilterUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethUninstallFilter = new Nethereum.RPC.Eth.Filters.EthUninstallFilter(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            var request = _ethUninstallFilter.BuildRequest(filterId);
            yield return SendRequest(request);
        }
    }


    public class EthCompileLLLUnityRequest : UnityRpcClient<Newtonsoft.Json.Linq.JObject>
    {
        private readonly Nethereum.RPC.Eth.Compilation.EthCompileLLL _ethCompileLLL;

        public EthCompileLLLUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethCompileLLL = new Nethereum.RPC.Eth.Compilation.EthCompileLLL(null);
        }

        public IEnumerator SendRequest(System.String lllcode)
        {
            var request = _ethCompileLLL.BuildRequest(lllcode);
            yield return SendRequest(request);
        }
    }


    public class EthCompileSerpentUnityRequest : UnityRpcClient<Newtonsoft.Json.Linq.JObject>
    {
        private readonly Nethereum.RPC.Eth.Compilation.EthCompileSerpent _ethCompileSerpent;

        public EthCompileSerpentUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethCompileSerpent = new Nethereum.RPC.Eth.Compilation.EthCompileSerpent(null);
        }

        public IEnumerator SendRequest(System.String serpentCode)
        {
            var request = _ethCompileSerpent.BuildRequest(serpentCode);
            yield return SendRequest(request);
        }
    }


    public class EthCompileSolidityUnityRequest : UnityRpcClient<Newtonsoft.Json.Linq.JToken>
    {
        private readonly Nethereum.RPC.Eth.Compilation.EthCompileSolidity _ethCompileSolidity;

        public EthCompileSolidityUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethCompileSolidity = new Nethereum.RPC.Eth.Compilation.EthCompileSolidity(null);
        }

        public IEnumerator SendRequest(System.String contractCode)
        {
            var request = _ethCompileSolidity.BuildRequest(contractCode);
            yield return SendRequest(request);
        }
    }


    public class EthGetCompilersUnityRequest : UnityRpcClient<System.String[]>
    {
        private readonly Nethereum.RPC.Eth.Compilation.EthGetCompilers _ethGetCompilers;

        public EthGetCompilersUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetCompilers = new Nethereum.RPC.Eth.Compilation.EthGetCompilers(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethGetCompilers.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthBlockNumberUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthBlockNumber _ethBlockNumber;

        public EthBlockNumberUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethBlockNumber = new Nethereum.RPC.Eth.Blocks.EthBlockNumber(null);
        }

        public IEnumerator SendRequest()
        {
            var request = _ethBlockNumber.BuildRequest();
            yield return SendRequest(request);
        }
    }


    public class EthGetBlockWithTransactionsByHashUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.BlockWithTransactions>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsByHash _ethGetBlockWithTransactionsByHash;

        public EthGetBlockWithTransactionsByHashUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBlockWithTransactionsByHash = new Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsByHash(null);
        }

        public IEnumerator SendRequest(System.String blockHash)
        {
            var request = _ethGetBlockWithTransactionsByHash.BuildRequest(blockHash);
            yield return SendRequest(request);
        }
    }


    public class EthGetBlockWithTransactionsHashesByHashUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.BlockWithTransactionHashes>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsHashesByHash _ethGetBlockWithTransactionsHashesByHash;

        public EthGetBlockWithTransactionsHashesByHashUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBlockWithTransactionsHashesByHash = new Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsHashesByHash(null);
        }

        public IEnumerator SendRequest(System.String blockHash)
        {
            var request = _ethGetBlockWithTransactionsHashesByHash.BuildRequest(blockHash);
            yield return SendRequest(request);
        }
    }


    public class EthGetBlockWithTransactionsByNumberUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.BlockWithTransactions>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsByNumber _ethGetBlockWithTransactionsByNumber;

        public EthGetBlockWithTransactionsByNumberUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBlockWithTransactionsByNumber = new Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsByNumber(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger number)
        {
            var request = _ethGetBlockWithTransactionsByNumber.BuildRequest(number);
            yield return SendRequest(request);
        }
    }


    public class EthGetBlockTransactionCountByHashUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthGetBlockTransactionCountByHash _ethGetBlockTransactionCountByHash;

        public EthGetBlockTransactionCountByHashUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBlockTransactionCountByHash = new Nethereum.RPC.Eth.Blocks.EthGetBlockTransactionCountByHash(null);
        }

        public IEnumerator SendRequest(System.String hash)
        {
            var request = _ethGetBlockTransactionCountByHash.BuildRequest(hash);
            yield return SendRequest(request);
        }
    }


    public class EthGetBlockTransactionCountByNumberUnityRequest : UnityRpcClient<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthGetBlockTransactionCountByNumber _ethGetBlockTransactionCountByNumber;

        public EthGetBlockTransactionCountByNumberUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBlockTransactionCountByNumber = new Nethereum.RPC.Eth.Blocks.EthGetBlockTransactionCountByNumber(null);
        }

        public IEnumerator SendRequest(Nethereum.RPC.Eth.DTOs.BlockParameter block)
        {
            var request = _ethGetBlockTransactionCountByNumber.BuildRequest(block);
            yield return SendRequest(request);
        }
    }


    public class EthGetBlockWithTransactionsHashesByNumberUnityRequest : UnityRpcClient<Nethereum.RPC.Eth.DTOs.BlockWithTransactionHashes>
    {
        private readonly Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsHashesByNumber _ethGetBlockWithTransactionsHashesByNumber;

        public EthGetBlockWithTransactionsHashesByNumberUnityRequest(string network = "mainnet", AuthProvider authProvider = null, JsonSerializerSettings jsonSerializerSettings = null) : base(network, authProvider, jsonSerializerSettings)
        {
            _ethGetBlockWithTransactionsHashesByNumber = new Nethereum.RPC.Eth.Blocks.EthGetBlockWithTransactionsHashesByNumber(null);
        }

        public IEnumerator SendRequest(Nethereum.Hex.HexTypes.HexBigInteger number)
        {
            var request = _ethGetBlockWithTransactionsHashesByNumber.BuildRequest(number);
            yield return SendRequest(request);
        }
    }
}