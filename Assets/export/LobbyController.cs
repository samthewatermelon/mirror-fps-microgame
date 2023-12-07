using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using System.Linq;


public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    //UI Elements
    public Text LobbyNameText;

    //Player Data
    public GameObject PlayerListViewContent;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;

    //Other Data
    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();

    //Manager
    private CustomNetworkManager manager = CustomNetworkManager.singleton;

    //Maps
    public string MapName;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    public void UpdateLobbyName()
    {
        CurrentLobbyID = manager.GetComponent<SteamLobby>().CurrentLobbyID;
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");
    }

    public void UpdatePlayerList()
    {
        if (!PlayerItemCreated) 
        { CreateHostPlayerItem(); } //Host
        if (PlayerListItems.Count < manager.GamePlayers.Count) { CreateClientPlayerItem(); }
        if (PlayerListItems.Count > manager.GamePlayers.Count) { RemovePlayerItem(); }
        if (PlayerListItems.Count == manager.GamePlayers.Count) { UpdatPlayerItem(); }
    }

    public void CreateHostPlayerItem()
    {
        foreach (PlayerObjectController player in manager.GamePlayers)
        {
            Debug.Log("spawning player list item");
            
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab);
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
        }
        PlayerItemCreated = true;
    }

    public void CreateClientPlayerItem()
    {
        foreach (PlayerObjectController player in manager.GamePlayers)
        {
            if (!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatPlayerItem()
    {
        foreach (PlayerObjectController player in manager.GamePlayers)
        {
            foreach (PlayerListItem PlayerListItemScript in PlayerListItems)
            {
                if (PlayerListItemScript.ConnectionID == player.ConnectionID)
                {
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.SetPlayerValues();
                }
            }
        }
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach (PlayerListItem playerListItem in PlayerListItems)
        {
            if (!manager.GamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerListItem);
            }
        }
        if (playerListItemToRemove.Count > 0)
        {
            foreach (PlayerListItem playerlistItemToRemove in playerListItemToRemove)
            {
                GameObject ObjectToRemove = playerlistItemToRemove.gameObject;
                PlayerListItems.Remove(playerlistItemToRemove);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }

    public void StartGame()
    {
        CustomNetworkManager.singleton.ServerChangeScene(MapName);
    }

    public void QuitLobby()
    {
        Exit();        
    }

    public void Exit()
	{  
		SteamMatchmaking.LeaveLobby((CSteamID)(CurrentLobbyID));
		// stops host 
		if (NetworkServer.active && NetworkClient.isConnected)
		{				
			CustomNetworkManager.singleton.StopHost();
			
		}	
		// stop client if client-only
		else if (NetworkClient.isConnected)
		{	
			CustomNetworkManager.singleton.StopClient();
			
		}
		// stop server if server-only
		else if (NetworkServer.active)
		{	
			CustomNetworkManager.singleton.StopServer();
			
		}		
	}
}
