namespace Bitski.Unity.WebGL
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    class WebGLAuthProvider : MonoBehaviour, AuthProvider
    {
        [DllImport("__Internal")]
        private static extern void BitskiWebGLInit(string clientId);

        public WebGLAuthProvider(String clientId) {
            BitskiWebGLInit(clientId);

            var bridge = new GameObject("WebGLAuthProvider");
            bridge.AddComponent<WebGLAuthProvider>();
            bridge.transform.parent = gameObject.transform;

            Debug.Log("Set up WebGLAuthProvider");

            DontDestroyOnLoad(bridge);
        }

        [DllImport("__Internal")]
        private static extern void BitskiWebGLSignIn();

        private Action<User> SignInCallback;

        public void SignIn(Action<User> callback) {
            SignInCallback = callback;
            BitskiWebGLSignIn();
        }

        public void DidSignIn(String jsonObject) {
            if (jsonObject.Contains("error")) {
                Debug.Log("Got an error signing in: " + jsonObject);
            } else {
                Debug.Log("Signed in as: " + jsonObject);
                var user = JsonUtility.FromJson<User>(jsonObject);
                SignInCallback(user);
            }
        }

        [DllImport("__Internal")]
        private static extern void BitskiWebGLGetUser();

        private Action<User> GetUserCallback;

        public void GetUser(Action<User> callback)
        {
            GetUserCallback = callback;
            BitskiWebGLGetUser();
        }

        public void DidGetUser(String userJSON, String errorJSON)
        {   
            if (userJSON != null)
            {
                Debug.Log("Got a user: " + userJSON);
                var user = JsonUtility.FromJson<User>(userJSON);
                GetUserCallback(user);
            }

            if (userJSON != null)
            {
                Debug.Log("Got an error getting user: " + errorJSON);
            }
        }
    }
}
