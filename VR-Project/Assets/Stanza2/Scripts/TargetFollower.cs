using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float movSpeed = 1f;

    private bool tar= true;
    private bool colli = true;
    private bool change = true;
    private int random;
    private double rot;
    private Collision col;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tar == true)
        {
            StartCoroutine(ChangeFilo());
        }

        if (change == true)
        {
            //Compute target direction
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.y = 0f;
            targetDirection.Normalize();

            //Rotate toward target direction
            float rotationStep = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

            //Move object along its forward axis
            transform.Translate(Vector3.forward * movSpeed * Time.deltaTime);
            //IS EQUIVALENT TO 
            //transform.Translate(transform.forward * movSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            //Compute target direction
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.y = 0f;
            targetDirection.Normalize();

            //Rotate toward target direction
            float rotationStep = (rotationSpeed + 2f) * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

            //Move object along its forward axis
            transform.Translate(Vector3.forward * (movSpeed) * Time.deltaTime);
            //IS EQUIVALENT TO 
            //transform.Translate(transform.forward * movSpeed * Time.deltaTime, Space.World);
        }
    }


    IEnumerator ChangeFilo()
    {
        tar = false;
        target = GameObject.FindWithTag("Target");

        if (target == null)
        {
            random = Random.Range(1, 9);
            target = GameObject.Find("amo (" + random + ")");
            change = false;

            random = Random.Range(1, 3);
            yield return new WaitForSeconds(random);
        }
        tar = true;
    }


}
