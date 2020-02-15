using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaScript : MonoBehaviour
{
    private bool fluo;
    Material material1;

    // Start is called before the first frame update
    void Start()
    {
        fluo = false;
        ChangeColorToFluo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeColorToFluo()
    {
        material1 = (Material)Resources.Load("EscaFluo", typeof(Material));
        this.GetComponent<Renderer>().material = material1;
        fluo = true;
    }

    public void DestroyInstance()
    {
        Destroy(this);
    }
}
