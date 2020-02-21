using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    private float pos = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator Rotate()
    {
        yield return new WaitForSeconds(0.3f);
        FindObjectOfType<AudioManager>().Play("RotelleCassaforte");
        while (pos < 60)
        {
            pos += 1f;
            transform.Rotate(1f, 0.0f, 0.0f, Space.Self);
            yield return new WaitForSeconds(.005f);
        }
        pos = 0;
    }

}