# Bitski Unity SDK

This project is still under development and is not ready to test yet.

## Getting Started

### Installing

Download the latest BitskiSDK.unitypackage from https://github.com/BitskiCo/bitski-unity/releases. Double click the downloaded file and follow the instructions to add it to your current unity project.

### Setup

In your `Awake` method or somewhere similar, initialise the Bitski SDK:
```C#
void Awake() {
    if (!BitskiSDK.IsInitialized)
    {
        try
        {
            BitskiSDK.Init("YOUR CLIENT ID");
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);  
        }
    } else {
        Debug.Log("Already initialized");
    }
}
```

### Signing In

You can sign in to Bitski by calling `BitskiSDK.SignIn()` and passing in a callback function.

_NOTE:_ In WebGL we need to launh a modal from the SignIn function. Unfortunately this is difficult due to the way Unity handles user input. To get around this we need to register with the PointerDown event, not the regular click event:

```C#
void Start()
{    
    EventTrigger trigger = signInButton.gameObject.AddComponent<EventTrigger>();
    var pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((e) => SignInButtonClicked());
    trigger.triggers.Add(pointerDown);

    mintButton.onClick.AddListener(MintButtonClicked);
}

void SignInButtonClicked()
{
    BitskiSDK.SignIn(SignInCallback);
}

void SignInCallback(User user)
{
    if (user == null) {
      Debug.Log("Error signing in...");
    } else {
      Debug.Log("Signed in!");
    }
}
```

### Listing Accounts

To list accounts use the `EthAccountsUnityRequest` api:

```C#
var accounts = new EthAccountsUnityRequest('mainnet');
yield return accounts.SendRequest();

if (accounts.Exception != null) {
    Debug.Log("Got an exception: " + accounts.Exception.Message);
}

if (accounts.Result != null) {
    Debug.Log("Got accounts: " + accounts.Result);
}
```

### Interacting with Contracts

To read a value from a contract, first we need to define our API. For the stanard ERC721 contract, to check the balance of an address, we define an API like this:

```C#
[Function("balanceOf", typeof(BalanceOfDTO))]
public class BalanceOfFunctionMessage : FunctionMessage
{
  [Parameter("address", "owner", 1)]
  public string Owner { get; set; }
}

[FunctionOutput]
public class BalanceOfDTO: IFunctionOutputDTO {
    [Parameter("uint256", "", 1)]
    public BigInteger Balance { get; set; }
}
```

Then we can call the function like this:

```C#
internal IEnumerator GetBalance(string account)
{
    var functionMessage = new BalanceOfFunctionMessage
    {
        Owner = account,
    };

    var request = new QueryUnityRequest<BalanceOfFunctionMessage, BalanceOfDTO>(defaultAccount, networkName);
    yield return request.Query(functionMessage, contractAddress);

    if (request.Exception != null) {
        Console.Log("Got an error" + request.Exception);
    } else {
        Console.Log("The balance is " + request.Result.Balance);
    }
}
```

# Building

To build, run this:
```bash
dotnet build --configuration Release --output ../build/netstandard20
```

Then copy the .dll and .jslib files from build/netstandard20 into Unity.