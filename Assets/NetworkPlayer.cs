using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour
{
    public Camera fpsCamera;
    public Camera weaponCamera;
    public AudioListener audioListener;
    public GameObject hud;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            fpsCamera.targetDisplay = 2;
            weaponCamera.enabled = false;
            audioListener.enabled = false;
            hud.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            fpsCamera.targetDisplay = 2;
            weaponCamera.enabled = false;
            audioListener.enabled = false;
            hud.SetActive(false);
        }
        else
        {
            GetComponent<Unity.FPS.Gameplay.PlayerInputHandler>().enabled = true;
        }

    }
}
