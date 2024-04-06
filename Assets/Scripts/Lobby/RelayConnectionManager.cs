using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayConnectionManager : Singleton<RelayConnectionManager>
{
    [SerializeField] private TextMeshProUGUI joinCodeTmp;
    [SerializeField] private TMP_InputField joinInputTmp;
    private const int MaxConnections = 2;

    void Start()
    {
        DisablePasswordUi();
    }

    protected override async void Awake()
    {
        // if (_instance != null && _instance != this)
        // {
        //     RelayConnectionManager[] existingInstances = FindObjectsOfType<RelayConnectionManager>();
        //     foreach (RelayConnectionManager instance in existingInstances)
        //     {
        //         if (instance != this)
        //         {
        //             Destroy(instance.gameObject);
        //         }
        //     }
        // return;
        // }
        // _instance = this;
        DontDestroyOnLoad(this);
        base.Awake();
        await Authenticate();
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        DisconnectFromServer(true);
    }

    private static async Task Authenticate()
    {
        try
        {
            // Initialise l'API de UGS
            await UnityServices.InitializeAsync();


            if (AuthenticationService.Instance.IsSignedIn) return;
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log($"Signed in {AuthenticationService.Instance.PlayerId}");
            };

            // On �vite de cr�er des comptes. Pour ce prototype, on s'authentifie au serveur anonymement
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        } catch (AuthenticationException e)
        {
            Debug.LogError(e);
        }
    }

    public async void CreateGame()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        try
        {
            var allocationDetails = await RelayService.Instance.CreateAllocationAsync(MaxConnections);
            joinCodeTmp.text = await RelayService.Instance.GetJoinCodeAsync(allocationDetails.AllocationId);
            
            // Applique les d�tails de la connexion au UnityTransport
            var relayServerData = new RelayServerData(allocationDetails, "dtls");
            transport.SetRelayServerData(relayServerData);


            // D�bute la connexion du host
            Debug.Log("Create game and start host");
            NetworkManager.Singleton.StartHost();

            joinCodeTmp.transform.parent.gameObject.SetActive(true);

            NetworkLoader.LoadNetwork(NetworkLoader.Scene.CharacterSelection); 
        }
        catch (RelayServiceException rse)
        {
            Debug.LogError(rse);
        }
    }

    public async void JoinGame()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        try
        {
            JoinAllocation clientAllocationDetails = 
                await RelayService.Instance.JoinAllocationAsync(joinInputTmp.text);

            // Applique les details de la connexion du client au UnityTransport
            var relayServerData = new RelayServerData(clientAllocationDetails, "dtls");
            transport.SetRelayServerData(relayServerData);

            // D�bute la connexion du client
            NetworkManager.Singleton.StartClient();
            DisablePasswordUi();
        }
        catch (RelayServiceException rse)
        {
            Debug.LogError(rse);
        }
    }

    public void DisconnectFromServer(bool hostDisconnected = false)
    {
        GameManager.Instance.LoadLevelAsync(NetworkLoader.Scene.MainMenu.ToString());
        Destroy(GameManager.Instance.gameObject);
        Destroy(SoundManager.Instance.gameObject);
        Destroy(NetworkManager.Singleton.gameObject);

        if (!hostDisconnected)
        {
            NetworkManager.Singleton.Shutdown();
        }
        
        Destroy(gameObject);
    }

    public void DisablePasswordUi()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}