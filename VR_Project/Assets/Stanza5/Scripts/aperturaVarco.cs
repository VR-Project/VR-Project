using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class aperturaVarco : MonoBehaviour
{
    static int counter = 0;
    private float pos = 0;
    static int flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 5)
        {
            StartCoroutine(ApriVarco());
            FindObjectOfType<AudioManager>().Play("SpostamentoMuro");
        }
    }

    public static void contaMattoniCorretti()
    {
        counter++;
    }
    public static void decrMattoniCorretti()
    {
        counter--;
    }

    public IEnumerator ApriVarco()
    {
        //AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("Stanza4");
        while (pos < 1.44f)
        {
            pos += 0.001f;
            gameObject.transform.Translate(0, 0.001f, 0);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
