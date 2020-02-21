using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCabinet : MonoBehaviour
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
        while (pos > -70)
        {
            pos -= 1f;
            transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(.01f);
        }
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(0.3f);
        while (pos < 0)
        {
            pos += 1f;
            transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(.01f);
        }
    }

}
