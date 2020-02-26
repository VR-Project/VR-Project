using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCassaforte : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject masso = GameObject.Find("Masso/Stone");
        Debug.Log("Dentro la funzione");
        if (other.gameObject.name == masso.name)
        {
            Debug.Log("Masso ha colliso");
            masso.GetComponent<AudioSource>().Stop();
            this.gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Audio ok: " + this.gameObject.GetComponent<AudioSource>().isPlaying);

            //FindObjectOfType<AudioManager>().Play("voce_scoglio");

        }
    }

}
