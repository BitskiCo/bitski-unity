namespace Bitski.Unity {
    using System;
    using UnityEngine;
    using WebGL;
    using Editor;

    /// <summary>
    /// A Bitski user.
    /// </summary>
    [Serializable]
    public class User {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public string UserId { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public bool IsExpired {
            get {
                return ExpiresAt > DateTime.Now;
            }
        }
    }

    /// <summary>
    /// The Bitski SDK.
    /// </summary>
    public sealed class BitskiSDK : ScriptableObject {
        private static AuthProvider authProvider;

        /// <summary>
        /// Get the current auth provider.
        /// </summary>
        public static AuthProvider AuthProviderImpl
        {
  
          get
            {
                if (authProvider == null)
                {
                    throw new NullReferenceException("The Bitski SDK is not yet initialized.  Did you call BitskiSDK.Init()?");
                }

                return authProvider;
            }
        }


        /// <summary>
        /// Initialize the Bitski SDK
        /// </summary>
        public static void Init(string clientId) {
            Debug.Log("Initializing Bitski SDK");
            switch (Application.platform)
            {
                case RuntimePlatform.WebGLPlayer:
                    authProvider = new WebGLAuthProvider(clientId);
                    Debug.Log("Set up WebGLAuthProider");
                    break;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.LinuxEditor:
                    authProvider = new EditorAuthProvider(clientId);
                    Debug.Log("Set up EditorAuthProvider");
                    break;
                default:
                    throw new NotSupportedException("The Bitski sdk does not support this platform");
            }

            Debug.Log("Finished initializing Bitski SDK");
        }

        /// <summary>
        /// Gets a value indicating whether is the SDK is initialized.
        /// </summary>
        /// <value><c>true</c> if is initialized; otherwise, <c>false</c>.</value>
        public static bool IsInitialized
        {
            get
            {
                return (authProvider != null);
            }
        }

        /// <summary>
        /// Gets the current user. Returns null if the user is not signed in.
        /// </summary>
        /// <param name="callback">Callback to be called when request completes.</param>
        public static void GetUser(Action<User> callback)
        {
            AuthProviderImpl.GetUser(callback);
        }

        /// <summary>
        /// Logs in and returns the current user.
        /// </summary>
        /// <param name="callback">Callback to be called when request completes.</param>
        public static void SignIn(Action<User> callback = null) {
            AuthProviderImpl.SignIn(callback);
        }
    }

    public interface AuthProvider {
        void SignIn(Action<User> callback);
        void GetUser(Action<User> callback);
    }
}