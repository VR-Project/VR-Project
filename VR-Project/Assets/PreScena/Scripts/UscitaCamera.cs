using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UscitaCamera : MonoBehaviour
{

    private bool collision = true;
    private GameObject target;
    private int counterUscita;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("TargetUscita");
        counterUscita = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (collision)
        {
            if (transform.position.y > 12)
            {
                target = GameObject.Find("TargetDirezione");
                StartCoroutine(Rotate());
            }
            else
            {
                StartCoroutine(Swim());
            }
        }
    }

    IEnumerator Swim()
    {
        collision = false;
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.Normalize();

        //Rotate toward target direction
        float rotationStep = 0.5f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

        //Move object along its forward axis
        transform.Translate(Vector3.forward * 4f * Time.deltaTime);

        yield return new WaitForSeconds(.01f);
        collision = true;
    }

    IEnumerator Rotate()
    {
        collision = false;
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.z = 0f;
        targetDirection.Normalize();

        //Rotate toward target direction
        float rotationStep = 0.5f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

        //Move object along its forward axis
        transform.Translate(Vector3.forward * 4f * Time.deltaTime);

        yield return new WaitForSeconds(.01f);
        collision = true;
    }
}
