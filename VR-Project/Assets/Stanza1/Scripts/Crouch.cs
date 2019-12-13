using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {

    //=================================================================================
    // © 2016 Scott Durkin, All rights reserved.
    // By Downloading and using this script credit must 
    // be given to the creator know as "Unity3D With Scott".
    // YouTube Channel: https://www.youtube.com/channel/UC9hfBvn17qSIrdFwAk56oZg
    //=================================================================================

    public CharacterController characterController;
    public bool isCrouched = false;
    void Start ()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched == false)
        {
            characterController.height = 0.5f;
            isCrouched = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched==true)
        {
            characterController.height = 1.8f;
            isCrouched = false;
        }

    }
}
