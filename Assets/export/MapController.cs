using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using TMPro;


public class MapController : NetworkBehaviour
{
    //Variables
    public string[] mapNames;
    public int currentMapIndex;
    public TMP_Text currentMapText;
    [SyncVar(hook = nameof(UpdateMapName))] public string MapNameSynced = "Scene1";

    private void Start()
    {
        currentMapIndex = 0;
        UpdateMapVariables();
    }

    public void NextMap()
    {
        if (currentMapIndex < mapNames.Length - 1)
        {
            currentMapIndex++;
            UpdateMapVariables();
            cmdSendMessageToPlayers(mapNames[currentMapIndex]);
        }
    }

    public void PreviousMap()
    {
        if (currentMapIndex > 0)
        {
            currentMapIndex--;
            UpdateMapVariables();
            cmdSendMessageToPlayers(mapNames[currentMapIndex]);
        }
    }

    public void UpdateMapVariables()
    {
        currentMapText.text = mapNames[currentMapIndex];
        LobbyController.Instance.MapName = mapNames[currentMapIndex];
    }

    public void UpdateMapName(string oldValue, string newValue)
    {
        if (isServer)
        {
            MapNameSynced = newValue;
        }
        if (isClient && (oldValue != newValue))
        {
            currentMapText.text = newValue;
        }
    }

    [Command(requiresAuthority = false)]
    void cmdSendMessageToPlayers(string newMessage)
    {
        UpdateMapName(MapNameSynced, newMessage);
    }
}


