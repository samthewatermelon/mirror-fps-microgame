using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class steamLobby : MonoBehaviour
{
    [SerializeField] private GameObject buttons = null;
    
    private NetworkManager networkManager;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

    private const string HostAddressKey = "HostAddress";


    // Start is called before the first frame update
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized)
            return;

        lobbyCreated = Callback<LobbyCreated_t>.Create(onLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(onGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(onLobbyEntered);
    }

    public void hostLobby()
    {
        buttons.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
    }

    private void onLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            buttons.SetActive(true);
            return;
        }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby), 
            HostAddressKey, 
            SteamUser.GetSteamID().ToString());

    }

    private void onGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void onLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active)
            return;

        string hostAddress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            HostAddressKey);

        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        buttons.SetActive(false);
    }
}
