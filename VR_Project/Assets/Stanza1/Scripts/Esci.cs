using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esci : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject opzioni;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && opzioni.activeSelf)
        {
            opzioni.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
