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

    public class WebGLAuthProvider : MonoBehaviour, AuthProvider
    {
        [DllImport("__Internal")]
        private static extern void BitskiWebGLInit(string clientId);

        [DllImport("__Internal")]
        private static extern void BitskiWebGLSignIn();

        [DllImport("__Internal")]
        private static extern void BitskiWebGLGetUser();

        private static User _currentUser;

        public User CurrentUser {
            get {
                return _currentUser;
            }
        }

        public WebGLAuthProvider(String clientId)
        {
            BitskiWebGLInit(clientId);

            var bridge = new GameObject("WebGLAuthProvider");
            bridge.AddComponent<WebGLAuthProvider>();

            DontDestroyOnLoad(bridge);

            //    bridge.transform.parent = gameObject.transform;
        }

        private static Action<User> SignInCallback;

        public void SignIn(Action<User> callback)
        {
            SignInCallback = callback;
            BitskiWebGLSignIn();
        }

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

                _currentUser = user;

                SignInCallback(user);
            }
        }


        private static Action<User> GetUserCallback;

        public void GetUser(Action<User> callback)
        {
            GetUserCallback = callback;
            BitskiWebGLGetUser();
        }

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

                _currentUser = user;

                GetUserCallback(user);
            }
        }
    }
}
