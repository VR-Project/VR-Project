using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using UnityEngine;

public class MoveQuadro : MonoBehaviour
{
    float pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerable MoveQ()
    {
        while(pos < 1f)
        {
            pos +=  0.01f;
            gameObject.transform.Translate(0, 0, -0.005f);
            yield return new WaitForSeconds(.05f);
        }
        while (pos > 0)
        {
            pos -= 0.01f;
            gameObject.transform.Translate(0, 0, +0.005f);
            yield return new WaitForSeconds(.05f);
        }

    }
}
