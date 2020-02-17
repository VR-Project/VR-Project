using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolle : MonoBehaviour
{
    static bool bolle = false;
    private float pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bolle == true) 
        {
            StartCoroutine(MuovoBolle());
            bolle = false;
        }
        
    }

    public static void MostraBolle()
    {
        bolle = true;
    }

    public IEnumerator MuovoBolle()
    {
        Debug.Log("muovi bolle");
        this.gameObject.SetActive(true);
        //while (pos < 0.1f)
        //{
        //    pos += 0.01f;
        //    gameObject.transform.Translate(0, -0.01f , 0);
        //    yield return new WaitForSeconds(.02f);
        //}
        yield return new WaitForSeconds(0.3f);
        //while (pos > 0)
        //{
        //    pos -= 0.01f;
        //    gameObject.transform.Translate(0, +0.01f, 0);
        //    yield return new WaitForSeconds(.007f);
        //}

    }
}
