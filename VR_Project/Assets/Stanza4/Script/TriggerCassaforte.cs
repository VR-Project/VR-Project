using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCassaforte : MonoBehaviour
{
    // Start is called before the first frame update
    //public static bool floor4 = false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject masso = GameObject.Find("Masso/Stone");
        if (other.gameObject.name == masso.name)
        {
            //floor4 = true;
            //AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("Stanza3");
            //GameObject.Find("room_4/Room.001/Floor.001").GetComponent<BoxCollider>().enabled = false;
            masso.GetComponent<AudioSource>().Stop();
            this.gameObject.GetComponent<AudioSource>().Play();

            while (this.gameObject.GetComponent<AudioSource>().isPlaying)
            {

            }
            FindObjectOfType<AudioManager>().Play("voce_scoglio");

        }
    }

}
