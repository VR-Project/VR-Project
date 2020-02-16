using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject originalParent = GameObject.Find("stanza5");
        Vector3 othersPositionRelativeTo = (other.transform.position - transform.position).normalized;

        //the result of the dot product returns > 0 if relative position 
        float dotResult = Vector3.Dot(othersPositionRelativeTo, transform.forward);
        
        GameObject brick = other.gameObject;
        brick.transform.parent = originalParent.transform;        
        brick.transform.rotation = Quaternion.Euler(0,180,0);
        Vector3 initRot = brick.transform.rotation.eulerAngles;
        brick.transform.position = transform.position;
        brick.GetComponent<Grabbable>().enabled = false;
    }

}