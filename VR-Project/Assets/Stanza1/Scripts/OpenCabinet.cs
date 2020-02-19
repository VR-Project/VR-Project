using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCabinet : MonoBehaviour
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

    public void Open()
    {
        transform.Rotate(0, -70, 0);
    }
    public void Close()
    {
        transform.Rotate(0, 70, 0);
    }
}
