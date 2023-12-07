using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Steamworks;


public class CustomNetworkManager : NetworkManager
{
    //[SerializeField] private PlayerObjectController GamePlayerPrefab;
    public static CustomNetworkManager singleton { get; internal set; }

    public List<PlayerObjectController> GamePlayers;

    public override void Start()
    {
        base.Start();
        singleton = this;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        
        if (SceneManager.GetActiveScene().name == "Lobby") 
        {
            PlayerObjectController GamePlayerInstance = conn.identity.gameObject.GetComponent<PlayerObjectController>();
            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);
        }
    }
}
