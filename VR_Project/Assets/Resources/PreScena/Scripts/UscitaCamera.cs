using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Color azzurro;
    private Color colore;
    private float red;
    private float green;
    private float blue;
    private bool transFog = true;
    private bool finish = true;
    private bool titoli = false;
    private float density;

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
        /*if (transFog)
        {
            StartCoroutine(transizioneFog());
        }*/
        if (finish)
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
                    if (!titoli)
                    {
                        titoli = true;
                        StartCoroutine(LoadTitoli());
                        
                    }
                }
                else
                {
                    if (up)
                    {
                        StartCoroutine(SwimUp());
                    }
                    else if (!up)
                    {
                        StartCoroutine(SwimDown());
                    }
                }
            }
        }
    }

    public void SetColor(Color color)
    {
        azzurro = color;
    }

    IEnumerator LoadTitoli()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(9);
    }

    IEnumerator transizioneFog()
    {
        transFog = false;
        colore = RenderSettings.fogColor;
        density = RenderSettings.fogDensity;
        while (RenderSettings.fogColor.r >= azzurro.r || RenderSettings.fogColor.g >= azzurro.g || RenderSettings.fogColor.b >= azzurro.b)
        {
            //SETTA I COLORI
            if (colore.r >= azzurro.r)
            {
                colore.r -= 0.01f;
            }
            if (colore.g >= azzurro.g)
            {
                colore.g -= 0.01f;
            }
            if (colore.b >= azzurro.b)
            {
                colore.b -= 0.01f;
            }
            RenderSettings.fogDensity = density;
            RenderSettings.fogColor = colore;
            yield return new WaitForSeconds(0.01f);
        }
        while(RenderSettings.fogDensity>= 0.1f)
        {
            density -= 0.01f;
            RenderSettings.fogDensity = density;
            yield return new WaitForSeconds(0.01f);
        }
        finish = true;
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
        //Debug.Log("SwimDown");
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
