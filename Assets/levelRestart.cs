using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class levelRestart : NetworkBehaviour
{    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && isServer)
            NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}
