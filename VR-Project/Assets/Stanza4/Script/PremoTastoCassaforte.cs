using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremoTastoCassaforte : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PremoTasto()
    {
        FindObjectOfType<AudioManager>().Play("SuonoPulsanteCassaforte");
        transform.Translate(0.005f, 0, 0);
        yield return new WaitForSeconds(0.3f);
        transform.Translate(-0.005f, 0, 0);
    }
}
