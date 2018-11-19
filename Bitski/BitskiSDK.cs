namespace Bitski
{
    using System;
    using UnityEngine;
    using Bitski.Unity.WebGL;
    using Bitski.Unity.Editor;
    using Bitski.Auth;

    /// <summary>
    /// The Bitski SDK.
    /// </summary>
    public sealed class BitskiSDK : ScriptableObject
    {
        private static AuthProvider authProvider;

        /// <summary>
        /// Get the current auth provider.
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
        public static void Init(string clientId)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WebGLPlayer:
                    authProvider = WebGLAuthProvider.CreateInstance<WebGLAuthProvider>();
                    authProvider.Initialize(clientId);
                    break;
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.LinuxEditor:
                    authProvider = EditorAuthProvider.CreateInstance<EditorAuthProvider>();
                    authProvider.Initialize(clientId);                
                    break;
                default:
                    throw new NotSupportedException("The Bitski sdk does not support this platform");
            }
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
        public static void GetUser(Action<User> callback = null)
        {
            AuthProviderImpl.GetUser(callback);
        }

        /// <summary>
        /// Logs in and returns the current user.
        /// </summary>
        /// <param name="callback">Callback to be called when request completes.</param>
        public static void SignIn(Action<User> callback = null)
        {
            AuthProviderImpl.SignIn(callback);
        }

        public static User CurrentUser {
            get {
                return AuthProviderImpl.CurrentUser;
            }
        }
    }
}