using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiBottone : MonoBehaviour
{
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
    }
}
