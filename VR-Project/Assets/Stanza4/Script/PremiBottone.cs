using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiBottone : MonoBehaviour
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

    public void Premi()
    {
        transform.Translate(0, 0.02f, 0);
        StartCoroutine(Apri());
    }

    public IEnumerator Apri()
    {
        GameObject cassetto = GameObject.Find("scrivania/Scrivania/cassetto_buono");
        while (pos < 0.25)
        {
            pos += 0.01f;
            cassetto.transform.Translate(0, 0, 0.01f);
            yield return new WaitForSeconds(.05f);
        }
    }
}
