using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorTrigger : MonoBehaviour
{
    private bool enterStanza2 = false;
    public static bool esca0 = false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject persona = GameObject.Find("Fps/FPSController");
        if(other.gameObject.name == persona.name)
        {
            esca0 = true;
            Destroy(GameObject.Find("Room_new"));
            if (enterStanza2 == false)
            {
                FindObjectOfType<AudioManager>().StopPlaying("transizione_1");
                FindObjectOfType<AudioManager>().Play("sottofondo_stanza2");
                FindObjectOfType<AudioManager>().Play("voce_pescare");

                enterStanza2 = true;
            }
        }
    }

}