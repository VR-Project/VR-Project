using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fine : MonoBehaviour
{
    private bool fine = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fine)
        {
            fine = false;
            StartCoroutine(FineT());
        }
    }

    IEnumerator FineT()
    {
        yield return new WaitForSeconds(8f);
        Application.Quit();
    }
}
