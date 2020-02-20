using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpegniLuce : MonoBehaviour
{
    Material material1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spegni()
    {
        GameObject.Find("lampadario/SpotLight").SetActive(false);
        this.gameObject.GetComponent<AudioSource>().Play();
        material1 = (Material)Resources.Load("wall_conScritta", typeof(Material));
        GameObject.Find("room_4/Room.001").GetComponent<Renderer>().material = material1;
    }
}
