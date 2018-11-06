mergeInto(LibraryManager.library, {

    BitskiWebGLSignIn: function () {
      console.log("BitskiWebGLSignIn called");
      window.unityBitskiInstance.signIn(2).then((user) => {
        console.log("Signed in as " + JSON.stringify(user));
        SendMessage('WebGLAuthProvider', 'DidSignIn', JSON.stringify(user));
      }).catch((error) => {
        console.log("Error signing in " + JSON.stringify(error));
        SendMessage('WebGLAuthProvider', 'DidSignIn', JSON.stringify(error));
      });
    },
  
    BitskiWebGLInit: function (clientId) {
      if (window.Bitski === undefined) {
        window.alert("Not running in the Bitski HTML template, canot start Bitski");
      } else {
        window.unityBitskiInstance = new window.Bitski.Bitski(clientId);
      }
    }
  });
  