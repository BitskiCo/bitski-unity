using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections;
using AOT;

public class BitskiBehavior : MonoBehaviour
{
    private static BitskiBehavior _instance;
    private System.String[] _accounts;

    public BitskiBehavior()
    {
        if (_instance != null && _instance != this)
        {
            Debug.Log("Only one intsance of Bitski should exist");
        }

        _instance = this;

        Debug.Log("Init");
    }


    public Button signInButton;

    public SpriteRenderer[] characters;

    void Start()
    {
        signInButton.GetComponent<Button>().onClick.AddListener(SignInButtonClicked);
    }

    void SignInButtonClicked()
    {
        Debug.Log("Signing in");
        SignIn();
    }

    private delegate void BitskiSignInCallback(string token, string error);

    #if UNITY_IPHONE
    [DllImport("__Internal")]
    #endif
    private static extern void BitskiSignIn(string ClientID, string CallbackUri, BitskiSignInCallback callback);

    [MonoPInvokeCallback(typeof(BitskiSignInCallback))]
    static void SignInCallback(string token, string error)
    {
        Debug.Log("Got a response:");

        if (token != null)
        {
            Debug.Log("Got a token " + token);
            _instance.StartCoroutine(_instance.GetAccountBalance(token));
        }

        if (error != null)
        {
            Debug.Log("Got an error" + error);
        }
    }

    public IEnumerator GetAccountBalance(string accessToken) {

        var nft = new NFT(accessToken);
        yield return nft.GetAccountBalance();

        Debug.Log("Displaying character images for tokens " + nft.tokens.tokenIDs);

        for (int i = 0; i < characters.Length; i++) {
            if (i > nft.tokens.tokenIDs.Count) {
                Debug.Log("Hiding token #" + i);
                characters[i].enabled = false;
                break;
            }

            Debug.Log("Showing token #" + i);
            characters[i].enabled = true;
            var tokenID = nft.tokens.tokenIDs[i];
        }

        signInButton.enabled = false;
    }

    void SignIn()
    {
        BitskiSignIn("35a7e890-2f64-4332-b5bc-ee556bde5cf1", "bitskiexampledapp://application/callback", SignInCallback);
    }
}
