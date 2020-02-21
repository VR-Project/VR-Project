using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApriPorta3 : MonoBehaviour
{
    private float pos3 = 0;
    public static bool apri3 = false;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (apri3 == true)
        {
            StartCoroutine(Apri3());
            apri3 = false;
        }
    }

    private IEnumerator Apri3()
    {
        yield return new WaitForSeconds(0.3f);
        FindObjectOfType<AudioManager>().Play("FineLivello");

        while (pos3 > -100)
        {
            pos3 -= 1f;
            transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
