mergeInto(LibraryManager.library, {
    BitskiWebGLInit: function (clientIdPointer) {
        if (window.Bitski === undefined) {
            window.alert("Not running in the Bitski HTML template, canot start Bitski");
        } else {
            var callbackURL = window.location.protocol + "//" + window.location.host + '/callback.html';
            window.unityBitskiInstance = new window.Bitski.Bitski(Pointer_stringify(clientIdPointer), callbackURL, callbackURL);
        }
    },

    BitskiWebGLSignIn: function () {
        var SignIn = function () {
            window.unityBitskiInstance.signIn(2).then(function(user) {
                SendMessage('WebGLAuthProvider', 'DidSignIn', JSON.stringify(user));
            }).catch(function(error) {
                SendMessage('WebGLAuthProvider', 'DidSignIn', JSON.stringify(error));
            });
            document.getElementById('gameContainer').removeEventListener('click', SignIn);
        };

        document.getElementById('gameContainer').addEventListener('click', SignIn, false);
    },

    BitskiWebGLGetUser: function () {
        window.unityBitskiInstance.getUser().then(function(user) {
            SendMessage('WebGLAuthProvider', 'DidGetUser', JSON.stringify(user));
        }).catch(function(error) {
            SendMessage('WebGLAuthProvider', 'DidGetUser', JSON.stringify(error));
        });
    },

    BitskiWebGLSendTransaction: function (networkPointer, methodPointer, requestPointer) {
        var network = Pointer_stringify(networkPointer);
        var request = Pointer_stringify(requestPointer);
        var provider = window.unityBitskiInstance.getProvider(network);
        console.log("Sending: " + request);

        provider.sendAsync(JSON.parse(request), function(error, response){
            console.log("Got callback");

            if (error) {
                console.error(error);
                SendMessage('WebGLAuthProvider', 'DidSendTransaction', JSON.stringify(error));
            }

            if (response) {
                console.log(response);
                SendMessage('WebGLAuthProvider', 'DidSendTransaction', JSON.stringify(response));
            }
        });
    },
});