using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;


public class LobbiesListManager : MonoBehaviour
{
    public static LobbiesListManager instance;

    //Lobbies List Variables
    public GameObject lobbiesMenu;
    public GameObject lobbyDataItemPrefab;
    public GameObject lobbiesListContent;

    public GameObject lobbiesButton, hostButton, ExitGameButton;

    public List<GameObject> listOfLobbies = new List<GameObject>();



    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public void GetListOfLobbies()
    {
        lobbiesButton.SetActive(false);
        hostButton.SetActive(false);
        ExitGameButton.SetActive(false);

        lobbiesMenu.SetActive(true);

        SteamLobby.Instance.GetLobbiesList();

    }


    public void DisplayLobbies(List<CSteamID> lobbyIDs, LobbyDataUpdate_t result)
    {
        for (int i = 0; i < lobbyIDs.Count; i++)
        {
            if (lobbyIDs[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                GameObject createdItem = Instantiate(lobbyDataItemPrefab);

                createdItem.GetComponent<LobbyDataEntry>().lobbyID = (CSteamID)lobbyIDs[i].m_SteamID;

                createdItem.GetComponent<LobbyDataEntry>().lobbyName =
                    SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "Name");

                createdItem.GetComponent<LobbyDataEntry>().SetLobbyData();

                createdItem.transform.SetParent(lobbiesListContent.transform);
                createdItem.transform.localScale = Vector3.one;

                listOfLobbies.Add(createdItem);
            }
        }
    }


    public void DestroyLobbies()
    {
        foreach (GameObject lobbyItem in listOfLobbies)
        {
            Destroy(lobbyItem);
        }
        listOfLobbies.Clear();

    }

    public void BacktoMainMenu()
    {
        lobbiesButton.SetActive(true);
        hostButton.SetActive(true);
        ExitGameButton.SetActive(true);
        DestroyLobbies();

        lobbiesMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }


}
