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
        if (NetworkManager.singleton.transport.GetType().Name == "KcpTransport")
            steamUi.SetActive(false);
        else
            turnOffUi();
    }

    public void setServerConnectAddress()
    {
        NetworkManager.singleton.networkAddress = ipAddressField.GetComponent<TMPro.TMP_InputField>().text;
    }

    public void turnOffUi()
    {
        foreach (GameObject go in regularUi)
            go.SetActive(false);
    }
}
