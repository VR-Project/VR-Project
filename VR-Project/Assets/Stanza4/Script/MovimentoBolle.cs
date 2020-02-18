using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBolle : MonoBehaviour
{
    public static bool bolle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bolle == true) this.GetComponent<Rigidbody>().AddForce(0, 0.01f, 0);
    }

    public void MuoviBolle()
    {
        this.GetComponent<Rigidbody>().AddForce(Random.Range(-10, 2) * 2, Random.Range(-10, 2) * 2, Random.Range(-10, 2) * 2);
        bolle = true;
    }

}
