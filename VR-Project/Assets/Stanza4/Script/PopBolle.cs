using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBolle : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameObject pezzoFoto = GameObject.Find("Bolle/PezziFoto/" + this.name); 
        if (other.gameObject.tag == "proiettile")
        {
            Vector3 posBolla = gameObject.transform.position;
            gameObject.SetActive(false);
            GameObject.Find("Bolle/PezziFoto/" + this.name).transform.position = posBolla;
            pezzoFoto.SetActive(true);
        }
        
    }

}