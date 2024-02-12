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

    private UnityTransport transport;
    private const int MaxConnections = 1;

    async void Awake()
    {
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        await Authenticate();
    }

    private static async Task Authenticate()
    {
        try
        {
            // Initialise l'API de UGS
            await UnityServices.InitializeAsync();

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
        try
        {
            var allocationDetails = await RelayService.Instance.CreateAllocationAsync(MaxConnections);
            joinCodeTmp.text = await RelayService.Instance.GetJoinCodeAsync(allocationDetails.AllocationId);

            // Applique les d�tails de la connexion au UnityTransport
            var relayServerData = new RelayServerData(allocationDetails, "dtls");
            transport.SetRelayServerData(relayServerData);

            
            // D�bute la connexion du host
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException rse)
        {
            Debug.LogError(rse);
        }
    }

    public async void JoinGame()
    {
        try
        {
            JoinAllocation clientAllocationDetails = 
                await RelayService.Instance.JoinAllocationAsync(joinInputTmp.text);

            // Applique les d�tails de la connexion du client au UnityTransport
            var relayServerData = new RelayServerData(clientAllocationDetails, "dtls");
            transport.SetRelayServerData(relayServerData);

            // Need to load character choice scene and then start host and characters on that scene
            // D�bute la connexion du client
            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException rse)
        {
            Debug.LogError(rse);
        }
    }
}