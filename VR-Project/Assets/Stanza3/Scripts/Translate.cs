using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using UnityEngine;

public class Translate : MonoBehaviour
{
    float pos = 0;
    float maxSpost = 0.8f;
    float dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveQ()
    {
        if (this.name == "LibriMove" || this.name == "cestino") maxSpost = 0.2f;
        if (this.name == "cestino"|| this.name == "Scrivania") dir = -1;
        while (pos < maxSpost)
        {
            pos +=  0.01f;
            gameObject.transform.Translate(0, -0.01f*dir, 0);
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(0.3f);
        while (pos > 0)
        {
            pos -= 0.01f;
            gameObject.transform.Translate(0, +0.01f*dir, 0);
            yield return new WaitForSeconds(.007f);
        }

    }
}
