using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBolle : MonoBehaviour
{
    public static bool bolle;
    float pos = 0;
    bool muovi = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bolle == true)
        {
            StartCoroutine(Muovi());
        }

    }

    public static void MuoviBolle()
    {
        bolle = true;
    }

    public IEnumerator Muovi()
    {
        
        while (muovi==true)
        {
            while (pos < 0.5f)
            {
                pos += 0.01f;
                gameObject.transform.Translate(0, 0.01f, 0);
                yield return new WaitForSeconds(.02f);
            }
            yield return new WaitForSeconds(0.3f);
            while (pos > -1.2f)
            {
                pos -= 0.01f;
                gameObject.transform.Translate(0, -0.01f, 0);
                yield return new WaitForSeconds(.02f);
            }
        }
        
    }
}
