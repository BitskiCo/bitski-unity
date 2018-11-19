namespace Bitski.Auth
{
    using System;

    public interface AuthProvider
    {
        void Initialize(String clientId);
        void SignIn(Action<User> callback);
        void GetUser(Action<User> callback);

    
        void SendTransaction(String network, String method, String request, Action<String> callback);
        User CurrentUser { get; }

        String ClientId { get; }
    }
}