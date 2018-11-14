mergeInto(LibraryManager.library, {
    BitskiWebGLInit: function (clientId) {
        if (window.Bitski === undefined) {
            window.alert("Not running in the Bitski HTML template, canot start Bitski");
        } else {
            let callbackURL = window.location.protocol + "//" + window.location.host + '/callback.html';
            window.unityBitskiInstance = new window.Bitski.Bitski(Pointer_stringify(clientId), callbackURL, callbackURL);
        }
    },

    BitskiWebGLSignIn: function () {
        var SignIn = function () {
            window.unityBitskiInstance.signIn(2).then( user => {
                SendMessage('WebGLAuthProvider', 'DidSignIn', JSON.stringify(user));
            }).catch( error => {
                SendMessage('WebGLAuthProvider', 'DidSignIn', JSON.stringify(error));
            });
            document.getElementById('gameContainer').removeEventListener('click', SignIn);
        };

        document.getElementById('gameContainer').addEventListener('click', SignIn, false);
    },

    BitskiWebGLGetUser: function () {
        window.unityBitskiInstance.getUser().then( user => {
            SendMessage('WebGLAuthProvider', 'DidGetUser', JSON.stringify(user));
        }).catch( error => {
            SendMessage('WebGLAuthProvider', 'DidGetUser', JSON.stringify(error));
        });
    },
});