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
    public CameraGiu camera;
    //private GameObject cam = GameObject.Find("FPSController/FirstPersonCharacter");
    void Start ()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        //camera = gameObject.GetComponent<Camera>();
        
    }

    void Update ()
    {

        //Vector3 originalPosition = characterController.transform.position;
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched == false)
        {
            characterController.height = 0.3f;
            isCrouched = true;
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                camera.Giu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched==true)
        {
            GameObject.Find("FPSController/FirstPersonCharacter").transform.position= new Vector3(0, 0.8f, 0);
            characterController.height = 1.8f;
            isCrouched = false;
            //characterController.transform.position = originalPosition;
        }

    }
}
