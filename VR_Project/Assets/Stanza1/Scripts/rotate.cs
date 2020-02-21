using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotationSpeed = 8;  //This will determine max rotation speed, you can adjust in the inspector

    public Camera cam;  //Drag the camera object here

    void Update()
    {
        //If you want to prevent rotation, just don't call this method
        if (Input.GetMouseButton(1))
        {
            RotateObject();
        }
        
    }

    void RotateObject()
    {
        ////Get mouse position
        //Vector3 mousePos = Input.mousePosition;

        ////Adjust mouse z position
        //mousePos.z = cam.transform.position.y - transform.position.y;

        ////Get a world position for the mouse
        //Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mousePos);

        ////Get the angle to rotate and rotate
        //float angle = -Mathf.Atan2(transform.position.z - mouseWorldPos.z, transform.position.x - mouseWorldPos.x) * Mathf.Rad2Deg;

        ////transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
        ////float angle2 = -Mathf.Atan2(transform.position.y - mouseWorldPos.y, transform.position.x - mouseWorldPos.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    
        transform.Rotate(0, 0, 90);
    }
}
