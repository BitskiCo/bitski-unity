namespace Bitski.Unity.Editor
{
    using System;
    using UnityEngine;

    class EditorAuthProvider : MonoBehaviour, AuthProvider {
        public EditorAuthProvider(String clientId) {
            
        }

        public void SignIn(Action<User> callback)
        {
            throw new NotSupportedException("The Bitski sdk does not support signing in in the editor");
        }


        public void GetUser(Action<User> callback)
        {
            throw new NotSupportedException("The Bitski sdk does not support getting a user in the editor");
        }
    }
}
