namespace Bitski.Unity.Editor
{
    using System;
    using UnityEngine;
    using Bitski.Auth;
    
    public class EditorAuthProvider : MonoBehaviour, AuthProvider {
        public User CurrentUser {
            get {
                return new User
                {
                    UserId = "fake-user",
                    AccessToken = "fake-token",
                    ExpiresAt = DateTime.Now
                };
            }
        }

        public EditorAuthProvider(String clientId) {
            
        }

        public void SignIn(Action<User> callback)
        {
            callback(CurrentUser);
        }

        public void GetUser(Action<User> callback)
        {
            callback(CurrentUser);
        }

        public void SendTransaction(String network, String method, String request, Action<String> callback)
        {
            callback(null);
        }
    }
}
