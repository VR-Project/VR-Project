using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAperturaVarco : MonoBehaviour
{
    private int c = 0;
    // Update is called once per frame
    void Update()
    {
        c = BrickTrigger.getCounterMattoni();
        if (c == 5)
        {
            FindObjectOfType<AudioManager>().Play("SpostamentoMuro");
        }
    }
}
