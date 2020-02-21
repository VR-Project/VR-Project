using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{

    Camera mainCam;//Camera Object Will Be Placed In Front Of
    GameObject clickedObject;//Currently Clicked Object

    //Holds Original Postion And Rotation So The Object Can Be Replaced Correctly
    Vector3 originaPosition;
    Vector3 originalRotation;

    //If True Allow Rotation Of Object
    public bool examineMode;
    private float coeff = 0.7f;


    void Start()
    {
        /*mainCam = Camera.main;
        examineMode = false;*/
    }

    private void Update()
    {

        //Decide What Object To Examine

        TurnObject();//Allows Object To Be Rotated

        //ExitExamineMode();//Returns Object To Original Postion
    }


    public void ClickObject()
    {
        mainCam = Camera.main;
        examineMode = false;
        if (examineMode == false)
        {      
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //ClickedObject Will Be The Object Hit By The Raycast
                clickedObject = hit.transform.gameObject;
                if (clickedObject.name == "bigliettoAereo" || clickedObject.name == "foto" || clickedObject.name == "fotoStrappata" || clickedObject.name == "2")coeff = 0.4f;
                else if (clickedObject.name == "anello") coeff = 0.35f;

                //Save The Original Postion And Rotation
                originaPosition = clickedObject.transform.position;
                originalRotation = clickedObject.transform.rotation.eulerAngles;

                Vector3 newPosition = mainCam.transform.position;
                //Vector3 newPosition2 = newPosition + transform.forward * 0.7f;
                //Now Move Object In Front Of Camera
                
                //clickedObject.transform.position = newPosition;
                clickedObject.transform.rotation = mainCam.transform.rotation;
                clickedObject.transform.position = newPosition + transform.forward * coeff;
                //Pause The Game
                Time.timeScale = 0;

                //Turn Examine Mode To True
                examineMode = true;
                clickedObject.transform.eulerAngles = originalRotation;
            }
        }
    }

    void TurnObject()
    {
        if (Input.GetMouseButton(0) && examineMode)
        {
            float rotationSpeed = 15;

            float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;

            clickedObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
            clickedObject.transform.Rotate(Vector3.right, yAxis, Space.World);
        }
        if (Input.GetMouseButtonDown(1) && examineMode)
        {
            ExitExamineMode();
        }
    }

    public void ExitExamineMode()
    {
        //Reset Object To Original Position
        clickedObject.transform.position = originaPosition;
        clickedObject.transform.eulerAngles = originalRotation;

        //Unpause Game
        Time.timeScale = 1;

        //Return To Normal State
        examineMode = false;

        if (clickedObject == GameObject.Find("scrivania/Scrivania/cassetto_buono/fotoStrappata"))
        {
            GameObject.Find("room_4/Room.001/tetto_s4").GetComponent<BoxCollider>().enabled = true;
            Bolle.MostraBolle();
        }
   
    }
}
