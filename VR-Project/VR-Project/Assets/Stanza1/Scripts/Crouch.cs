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

    void Start ()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.height = 0.5f;
        }
        else
        {
            characterController.height = 1.8f;
        }
    }
}
