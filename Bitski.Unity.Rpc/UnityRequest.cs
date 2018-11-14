using System;


namespace Bitski.Unity.Rpc
{
    public class UnityRequest<TResult>
    {
        public TResult Result { get; set; }
        public Exception Exception { get; set; }
    }
}  