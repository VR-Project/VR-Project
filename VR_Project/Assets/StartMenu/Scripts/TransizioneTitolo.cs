using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransizioneTitolo : MonoBehaviour
{
    private bool transizione = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transizione)
        {
            transizione = false;
            StartCoroutine(Transizione());
        }
    }

    IEnumerator Transizione()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(9);
    }
}
