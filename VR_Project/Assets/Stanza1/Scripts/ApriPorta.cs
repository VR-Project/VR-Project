using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApriPorta : MonoBehaviour
{
    private float pos = 0;
    public static bool apri = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (apri == true)
        {
            StartCoroutine(Apri());
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Stanza2", LoadSceneMode.Additive);
            apri = false;
        }
    }

    private IEnumerator Apri()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.GetComponent<AudioSource>().Play();
        while (pos > -100)
        {
            pos -= 1f;
            transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
