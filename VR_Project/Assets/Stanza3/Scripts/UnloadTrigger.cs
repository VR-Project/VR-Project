using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject persona = GameObject.Find("Fps/FPSController");
        if (other.gameObject.name == persona.name)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("Stanza2");
            FindObjectOfType<AudioManager>().StopPlaying("transizione_1");
            //FindObjectOfType<AudioManager>().Play("transizione_2_finale");
            FindObjectOfType<AudioManager>().Play("sottofondoGenerale");

        }
    }

}
