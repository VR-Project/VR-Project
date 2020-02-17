using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionDetecter : MonoBehaviour
{

    public GameObject target;
    public bool ok;
    private double rot;
    private bool colli = true;

    public void OnCollisionEnter(Collision collision)
    {
        CollisionColorChanger colorChanger = collision.gameObject.GetComponent<CollisionColorChanger>();
        Rigidbody collisionRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        CollisionWithFish collisionWall = collision.gameObject.GetComponent<CollisionWithFish>();


        if (collisionWall != null && colli == true)
        {
            target = GameObject.Find("amoColtello");
            StartCoroutine(ChangeDirection());
        }

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

    IEnumerator ChangeDirection()
    {
        colli = false;
        while (rot < 1)
        {
            Debug.Log("changeDirection");
            rot = rot + 0.001;
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.y = 0f;
            targetDirection.Normalize();

            //Rotate toward target direction
            float rotationStep = 5f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

            //Move object along its forward axis
            transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
            yield return new WaitForSeconds(.05f);
        }
        rot = 0;
        colli = true;
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
