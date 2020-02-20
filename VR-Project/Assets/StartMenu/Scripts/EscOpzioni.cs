using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscOpzioni : MonoBehaviour
{
    private GameObject cameraMenu;
    private GameObject cameraOpzioni;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraMenu = GameObject.Find("EmptyMenu");
        cameraOpzioni = GameObject.Find("EmptyOpzioni");

        if (Input.GetKeyDown(KeyCode.Escape) && cameraOpzioni.transform.GetChild(0).gameObject.active==true)
        {
            cameraMenu.transform.GetChild(0).gameObject.SetActive(true);
            cameraOpzioni.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && cameraOpzioni.transform.GetChild(0).gameObject.active == false)
        {
            cameraOpzioni.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}
