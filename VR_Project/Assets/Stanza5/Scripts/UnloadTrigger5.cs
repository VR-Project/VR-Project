using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadTrigger5 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject persona = GameObject.Find("Fps/FPSController");
        if (other.gameObject.name == persona.name)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("Stanza4");
            Stanza4Trigger.floor4 = false;
            GameObject.Find("stanza5/Pavimento").GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<AudioManager>().StopPlaying("transizione_1");
            FindObjectOfType<AudioManager>().Play("sottofondoGenerale");
        }
    }

}