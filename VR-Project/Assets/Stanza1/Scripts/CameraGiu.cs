using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGiu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Giu()
    {
        GameObject.Find("FPSController/FirstPersonCharacter").transform.Translate(0, -0.65f, 0);
    }
}
