using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public static bool esca0 = false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject persona = GameObject.Find("Fps/FPSController");
        if(other.gameObject.name == persona.name)
        {
            esca0 = true;
        }
    }

}