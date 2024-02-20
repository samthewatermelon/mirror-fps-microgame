using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class serverFunctions : MonoBehaviour
{
    public GameObject ipAddressField;
    public GameObject steamUi;
    public GameObject[] regularUi;

    private void Start()
    {
        Debug.Log(GetComponent<CustomNetworkManager>().transport.GetType().Name);

        if (GetComponent<CustomNetworkManager>().transport.GetType().Name != "FizzySteamworks")
            steamUi.SetActive(false);
        else
            turnOffUi();
    }

    public void setServerConnectAddress()
    {
        CustomNetworkManager.singleton.networkAddress = ipAddressField.GetComponent<TMPro.TMP_InputField>().text;
        CustomNetworkManager.singleton.StartClient();
    }

    public void turnOffUi()
    {
        foreach (GameObject go in regularUi)
            go.SetActive(false);
    }
}
