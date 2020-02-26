using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTrigger : MonoBehaviour
{
    public bool bucoCorretto = false;
    static int counter = 0;
    private void OnTriggerEnter(Collider other)
    {
        GameObject originalParent = GameObject.Find("Mattoni");
        Vector3 othersPositionRelativeTo = (other.transform.position - transform.position).normalized;

        //the result of the dot product returns > 0 if relative position 
        float dotResult = Vector3.Dot(othersPositionRelativeTo, transform.forward);
        FindObjectOfType<AudioManager>().Play("MattoneInserito");


        GameObject brick = other.gameObject;
        brick.transform.parent = originalParent.transform;        
        brick.transform.rotation = Quaternion.Euler(0,180,0);
        Vector3 initRot = brick.transform.rotation.eulerAngles;
        brick.transform.position = transform.position;
        Destroy(brick.GetComponent<Grabbable>());
        brick.AddComponent<BrickIndietro>();
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        //Debug.Log(counter);
        if (brick.name == this.gameObject.name)
        {
            counter++;
            aperturaVarco.contaMattoniCorretti();
        }
        
    }

    public static int getCounterMattoni()
    {
        return counter;
    }

}