using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UscitaCamera : MonoBehaviour
{

    private bool collision = true;
    private GameObject target;
    private int counterUscita;
    private float movZ;
    public float smooth= 2.0f;
    public float tiltAngle= 90.0f;
    private bool up = true;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("TargetUscita");
        counterUscita = 0;
        movZ = 0;
        moveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        smooth = 2.0f;
        tiltAngle = 90.0f;

        if (collision)
        {
            if (transform.position.y > 12)
            {
                RenderSettings.fog = false;
                target = GameObject.Find("TargetDirezione");
                StartCoroutine(Rotate());
            }
            else
            {
                if (up)
                {
                    StartCoroutine(SwimUp());
                } else if (!up)
                {
                    StartCoroutine(SwimDown());
                }
            }
        }
    }

    IEnumerator SwimUp()
    {
        collision = false;
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.Normalize();

        //Rotate toward target direction
        float rotationStep = 0.5f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

        //Move object along its forward axis
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        moveSpeed += 0.25f;

        if(moveSpeed == 4f)
        {
            up = false;
        }

        yield return new WaitForSeconds(.01f);
        collision = true;
    }

    IEnumerator SwimDown()
    {
        Debug.Log("SwimDown");
        collision = false;
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.Normalize();

        //Rotate toward target direction
        float rotationStep = 0.5f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

        //Move object along its forward axis
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        moveSpeed -= 0.25f;

        if(moveSpeed == 1f)
        {
            up = true;
        }

        yield return new WaitForSeconds(.01f);
        collision = true;
    }

    IEnumerator Rotate()
    {
        if (movZ < 3f)
        {
            movZ += .05f;
            transform.Translate(0f, 0f, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            Vector3 targetDirection = transform.position;
            targetDirection.Normalize();

            float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion targetR = Quaternion.Euler(tiltAroundX, 0, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetR, Time.deltaTime * smooth);

            //Move object along its forward axis
            //transform.Translate(-Vector3.forward * 0.5f * Time.deltaTime);
        }
    }
}
