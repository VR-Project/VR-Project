using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;   

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject fps;
    public GameObject opzioni;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        fps = GameObject.Find("Fps");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !opzioni.activeSelf)
        {
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                GameObject.Find("Fps/FPSController").GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                GameObject.Find("Fps/FPSController").GetComponent<FirstPersonController>().enabled = false;
            }
        }
    }
}
