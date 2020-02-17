using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject target;
    private bool collision = true;
    private bool finish = true;
    private bool finishTot = false;

    public float nearClipPlane = 0;
    public GameObject CameraG;
    public Camera Camera;
    public RenderTexture nero;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Stone5ok17");
        CameraG = GameObject.Find("Camera");
        Camera = CameraG.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float smooth = 1.0f;
        float tiltAngle = 270.0f;

        if (collision)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.Normalize();

            //Rotate toward target direction
            float rotationStep = 2f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

            //Move object along its forward axis
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);

            if (transform.position.y < 3.11f)
            {
                collision = false;
            }

        }else if(!collision && !finishTot)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.Normalize();

            float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion targetR = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetR, Time.deltaTime * smooth);

            //Move object along its forward axis
            transform.Translate(-Vector3.forward * 0.5f * Time.deltaTime);
            if (transform.rotation.y < 0.02f && finish == true)
            {
                finish = false;
                CameraG.GetComponent<Camera>().cullingMask = 1 << LayerMask.NameToLayer("Nothing");
                RenderSettings.fog = false;
            }
        }else if(finishTot)
        {
            CameraG.SetActive(false);
        }
    }

}
