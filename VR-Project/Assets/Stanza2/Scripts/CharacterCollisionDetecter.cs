using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionDetecter : MonoBehaviour
{

    public GameObject io;
    public bool ok;

    public void OnCollisionEnter(Collision collision)
    {
        CollisionColorChanger colorChanger = collision.gameObject.GetComponent<CollisionColorChanger>();
        Rigidbody collisionRigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (colorChanger != null && collisionRigidBody == null)
        {
            //colorChanger.blink();
            ok = false;
            //StartCoroutine(Coroutine);
        }

    }

    public bool getOk()
    {
        return ok;
    }



    /*IEnumerator Coroutine()
    {
        EscaScript script = esca.GetComponent<EscaScript>();
        script.DestroyInstance();
        PickUp pick = esca.GetComponent<PickUp>();
        pick.DestroyInstance();
        CollisionColorChanger color = esca.GetComponent<CollisionColorChanger>();
        color.DestroyInstance();
        material1 = (Material)Resources.Load("Esca", typeof(Material));
        io.GetComponent<Renderer>().material = material1;
        yield return new WaitForSeconds(3);
        io.transform.gameObject.SetActive(true);
        io.tag = "Untagged";
        random = Random.Range(0, 8) + 1;
        amo = GameObject.Find("amo (" + random + ")");
        Debug.Log("amo (" + random + ")");
        esca = amo.transform.GetChild(0).Find("esca").gameObject;
        esca.AddComponent(typeof(EscaScript));
        esca.AddComponent(typeof(PickUp));
        esca.AddComponent(typeof(CollisionColorChanger));
        esca.tag = "Target";
    }*/

}
