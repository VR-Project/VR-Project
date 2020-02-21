using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    private float pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Open()
    {
        yield return new WaitForSeconds(0.3f);
        while (pos > -0.3)
        {
            pos -= 0.005f;
            transform.Translate(-0.005f, 0.0f, 0.0f);
            yield return new WaitForSeconds(.001f);
        }
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(0.3f);
        while (pos < 0)
        {
            pos += 0.004f;
            transform.Translate(0.004f, 0.0f, 0.0f);
            yield return new WaitForSeconds(.001f);
        }
    }

}
