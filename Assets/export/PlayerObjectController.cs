using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

public class PlayerObjectController : NetworkBehaviour
{
    //Player Data
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;

    private CustomNetworkManager manager = CustomNetworkManager.singleton;

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        LobbyController.Instance.UpdateLobbyName();
    }


    public override void OnStartClient()
    {
        manager.GamePlayers.Add(this);
        Debug.Log("attempting to add player to GamePlayers");
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }


    public override void OnStopClient()
    {
        manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }


    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        this.PlayerNameUpdate(this.PlayerName, playerName);
    }


    public void PlayerNameUpdate(string OldValue, string NewValue)
    {
        if (isServer) //Host
        {
            this.PlayerName = NewValue;
        }
        if (isClient) //Client
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    public void Quit()
    {

        //Set the offline scene to null
        manager.offlineScene = "";

        //Make the active scene the offline scene
        SceneManager.LoadScene("MainMenu");

        //Leave Steam Lobby
        SteamLobby.Instance.LeaveLobby();

        if (isOwned)
        {
            if (isServer)
            {
                manager.StopHost();
            }
            else
            {
                manager.StopClient();
            }
        }
    }
}
