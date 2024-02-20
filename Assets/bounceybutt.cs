using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceybutt : MonoBehaviour
{
    public Vector3 startPos;
    public bool dirUp;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dirUp)
        {
            transform.localPosition = new Vector3(startPos.x, Mathf.Lerp(transform.localPosition.y, startPos.y + 0.1f, 0.03f), startPos.z);
            if ((startPos.y + 0.1f) - transform.localPosition.y < 0.06f)
                dirUp = false;
        }
        else
        {
            transform.localPosition = new Vector3(startPos.x, Mathf.Lerp(transform.localPosition.y, startPos.y - 0.1f, 0.03f), startPos.z);
            if ((startPos.y - 0.1f) - transform.localPosition.y > -0.06f)
                dirUp = true;
        }
            
    }
}
