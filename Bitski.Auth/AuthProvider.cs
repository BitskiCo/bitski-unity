namespace Bitski.Auth
{
    using System;

    public interface AuthProvider
    {
        void SignIn(Action<User> callback);
        void GetUser(Action<User> callback);
        User CurrentUser { get; }
    }
}