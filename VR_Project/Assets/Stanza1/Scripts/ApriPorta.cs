using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            apri = false;
        }
    }

    private IEnumerator Apri()
    {
        yield return new WaitForSeconds(0.3f);
        while (pos > -100)
        {
            pos -= 1f;
            transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
