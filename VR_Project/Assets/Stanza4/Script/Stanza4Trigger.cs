using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stanza4Trigger : MonoBehaviour
{
    public static bool floor4 = false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject persona = GameObject.Find("Fps/FPSController");
        if (other.gameObject.name == persona.name)
        {
            floor4 = true;
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("Stanza3");
            GameObject.Find("room_4/Room.001/Floor.001").GetComponent<BoxCollider>().enabled = false;
        }
    }

}