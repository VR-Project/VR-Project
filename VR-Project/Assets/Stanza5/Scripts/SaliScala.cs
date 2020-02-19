using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaliScala : MonoBehaviour
{
    public GameObject fps;
    private float counter;
    private int angle;
    public GameObject uscita;
    private GameObject sinapsi;
    private GameObject room1;
    private GameObject room2;
    private GameObject room3;
    private GameObject room4;
    private GameObject prescena;
    private Material bianco;
    private GameObject cameraPost;

    private Color azzurro;
    private Vector3 positionFPS;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaliERuota()
    {
        StartCoroutine(SeR());
    }

    public IEnumerator SeR()
    {
        bianco = (Material)Resources.Load("Bianco", typeof(Material));
        RenderSettings.skybox = bianco;
        sinapsi = GameObject.Find("Sinapsi");
        sinapsi.SetActive(false);
        room1 = GameObject.Find("Room_new");
        room2 = GameObject.Find("room_2");
        room3 = GameObject.Find("Room");
        room4 = GameObject.Find("room_4");
        prescena = GameObject.Find("PreScena");

        room1.SetActive(false);
        room2.SetActive(false);
        room3.SetActive(false);
        room4.SetActive(false);
        prescena.SetActive(false);

        fps = GameObject.Find("FPSController");
        uscita = GameObject.Find("EmptyCamera");
        azzurro = RenderSettings.fogColor;
        positionFPS = fps.transform.position;
        uscita.transform.GetChild(0).gameObject.SetActive(true);
        uscita.transform.GetChild(0).transform.position = positionFPS;
        fps.SetActive(false);

        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.white;
        RenderSettings.fogDensity = 0f;

        while(uscita.transform.GetChild(0).transform.position.z < 7.2)
        {
            uscita.transform.GetChild(0).transform.Translate(0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        while (counter < 2f)
        {
            if (angle < 70)
            {
                angle += 2;
                uscita.transform.GetChild(0).transform.Rotate(-2, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
            else{ 
            RenderSettings.fogDensity += 0.001f;
            uscita.transform.GetChild(0).transform.Translate(0, 0.005f, 0.01f);
            yield return new WaitForSeconds(0.01f);
            counter += 0.01f;
            }
        }
        prescena.SetActive(true);
        uscita.transform.GetChild(0).gameObject.SetActive(false);
        cameraPost = GameObject.Find("EmptyPost");
        cameraPost.transform.GetChild(0).gameObject.SetActive(true);
        cameraPost.transform.GetChild(0).gameObject.GetComponent<UscitaCamera>().SetColor(azzurro);


    }
}
