namespace Bitski.Unity.WebGL
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using Newtonsoft.Json;
    using Bitski.Auth;

    [Serializable]
    class WebGLProfileResponse
    {
        public int auth_time;
        public int rat;
        public string sub;
    }

    [Serializable]
    class WebGLUserResponse
    {
        public string access_token;
        public int expires_at;
        public string id_token;
        public WebGLProfileResponse profile;
        public string scope;
        public string state;
        public string token_type;
    }

    public class WebGLAuthProvider : ScriptableObject, AuthProvider
    {
        [DllImport("__Internal")]
        private static extern void BitskiWebGLInit(string clientId);

        [DllImport("__Internal")]
        private static extern void BitskiWebGLSignIn();

        [DllImport("__Internal")]
        private static extern void BitskiWebGLGetUser();

        [DllImport("__Internal")]
        private static extern void BitskiWebGLSendTransaction(string network, string method, string request);

        public User CurrentUser {
            get {
                return WebGLAuthProviderCallbackHandler._lastKnownUser;
            }
        }

        private static GameObject Bridge;

        static void Setup() {
            if (Bridge == null) {
                var bridge = new GameObject("WebGLAuthProvider");
                bridge.AddComponent<WebGLAuthProviderCallbackHandler>();

                DontDestroyOnLoad(bridge);

                Bridge = bridge;
            }
        }

        public String ClientId { get; private set; }

        public void Initialize(String clientId)
        {
            ClientId = clientId;
            BitskiWebGLInit(clientId);
            Setup();
        }

        public void SignIn(Action<User> callback)
        {
            WebGLAuthProviderCallbackHandler.SignInCallback = callback;
            BitskiWebGLSignIn();
        }

        public void GetUser(Action<User> callback)
        {
            WebGLAuthProviderCallbackHandler.GetUserCallback = callback;
            BitskiWebGLGetUser();
        }

        public void SendTransaction(String network, String method, String request, Action<String> callback) 
        {
            WebGLAuthProviderCallbackHandler.SendTransactionCallback = callback;
            BitskiWebGLSendTransaction(network, method, request);
        }
    }

    public class WebGLAuthProviderCallbackHandler: MonoBehaviour {
        internal static User _lastKnownUser;

        internal static Action<User> SignInCallback;

        public void DidSignIn(String jsonObject)
        {
            if (jsonObject.Contains("error"))
            {
                Debug.Log("Got an error signing in: " + jsonObject);
            }
            else
            {
                var userResponse = JsonUtility.FromJson<WebGLUserResponse>(jsonObject);
                var user = new User
                {
                    AccessToken = userResponse.access_token,
                    ExpiresAt = new DateTime(userResponse.expires_at),
                    UserId = userResponse.profile.sub,
                };

                _lastKnownUser = user;

                SignInCallback(user);
            }
        }


        internal static Action<User> GetUserCallback;

        public void DidGetUser(String jsonObject)
        {
            if (GetUserCallback == null) {
                return;
            }

            if (jsonObject == "null") {
                GetUserCallback(null);
            }
            else if (jsonObject.Contains("error"))
            {
                Debug.Log("Got an error signing in: " + jsonObject);
                GetUserCallback(null);
            }
            else
            {
                var userResponse = JsonUtility.FromJson<WebGLUserResponse>(jsonObject);
                var user = new User
                {
                    AccessToken = userResponse.access_token,
                    ExpiresAt = new DateTime(userResponse.expires_at),
                    UserId = userResponse.profile.sub,
                };

                _lastKnownUser = user;

                GetUserCallback(user);
            }
        }

        internal static Action<String> SendTransactionCallback;

        public void DidSendTransaction(String jsonObject) {
            SendTransactionCallback(jsonObject);
        }
    }
}
