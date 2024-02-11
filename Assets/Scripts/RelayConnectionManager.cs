using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayConnectionManager : MonoBehaviour
{
    [SerializeField] private string joinCodeText;
    [SerializeField] private string joinInput;

    private UnityTransport transport;
    private const int MaxConnections = 1;

    private async void Awake()
    {
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        await Authenticate();
    }

    private static async Task Authenticate()
    {
        // Initialise l'API de UGS
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Signed in {AuthenticationService.Instance.PlayerId}");
        };

        // On évite de créer des comptes. Pour ce prototype, on s'authentifie au serveur anonymement
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateGame()
    {
        try
        {
            var allocationDetails = await RelayService.Instance.CreateAllocationAsync(MaxConnections);
            joinCodeText = await RelayService.Instance.GetJoinCodeAsync(allocationDetails.AllocationId);

            // Applique les détails de la connexion au UnityTransport
            var relayServerData = new RelayServerData(allocationDetails, "dtls");
            transport.SetRelayServerData(relayServerData);

            // Débute la connexion du host
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
                await RelayService.Instance.JoinAllocationAsync(joinInput);

            // Applique les détails de la connexion du client au UnityTransport
            var relayServerData = new RelayServerData(clientAllocationDetails, "dtls");
            transport.SetRelayServerData(relayServerData);

            // Débute la connexion du client
            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException rse)
        {
            Debug.LogError(rse);
        }
    }
}