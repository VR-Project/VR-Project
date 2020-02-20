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
    public GameObject FPS;
    private GameObject light;
    public Camera Camera;
    public RenderTexture nero;
    private float red;
    private float blue;
    private float green;
    private Color color;
    public Material nerog;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Stone5ok17");
        CameraG = GameObject.Find("Camera");
        Camera = CameraG.GetComponent<Camera>();
        FPS = GameObject.Find("Fps");
        nerog = (Material)Resources.Load("Nero", typeof(Material));
        light = GameObject.Find("Directional Light (2)");
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
            transform.Translate(Vector3.forward * 4f * Time.deltaTime);

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
            if (transform.rotation.y < 0.001f && finish == true)
            {
                finish = false;
                StartCoroutine(Nero());
            }
        }else if(finishTot)
        {
            Debug.Log("Transizione");
            CameraG.SetActive(false);
            light.GetComponent<Light>().enabled = false;
            RenderSettings.skybox = nerog;
            FPS.transform.GetChild(0).gameObject.SetActive(true);
            RenderSettings.fog = false;
        }
    }

    IEnumerator Nero()
    {
        while(RenderSettings.fogDensity < 0.5f)
        {

            color.r = RenderSettings.fogColor.r;
            color.g = RenderSettings.fogColor.g;
            color.b = RenderSettings.fogColor.b;
            color.r = color.r - 0.01f;
            color.b = color.b - 0.01f;
            color.g = color.g - 0.01f;

            RenderSettings.fogColor = color;

            RenderSettings.fogDensity = RenderSettings.fogDensity + 0.001f;
            yield return new WaitForSeconds(.01f);
        }
        finishTot = true;
    }

}
