using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePrefs : MonoBehaviour
{
    public int targetFps;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;   
    }
}
