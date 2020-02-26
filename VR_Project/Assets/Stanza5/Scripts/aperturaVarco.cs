using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class aperturaVarco : MonoBehaviour
{
    static int counter = 0;
    private float pos = 0;
    static int flag = 0;
    private bool varcoOk = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 5 && varcoOk==false)
        {
            StartCoroutine(ApriVarco());
            varcoOk = true;
            
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
        //FindObjectOfType<AudioManager>().Play("SpostamentoMuro");
        //this.gameObject.GetComponent<AudioSource>().Play();
        //Debug.Log("Apertura");
        yield return new WaitForSeconds(0.7f);
        FindObjectOfType<AudioManager>().Play("SpostamentoMuro");
        while (pos < 1.44f)
        {
            pos += 0.008f;
            gameObject.transform.Translate(0, 0.008f, 0);
            yield return new WaitForSeconds(0.00002f);
        }
        FindObjectOfType<AudioManager>().StopPlaying("SpostamentoMuro");

    }

    private void playAudioMuro()
    {
        FindObjectOfType<AudioManager>().Play("SpostamentoMuro");
    }
}
