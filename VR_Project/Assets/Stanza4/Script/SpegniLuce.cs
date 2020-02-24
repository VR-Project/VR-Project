using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpegniLuce : MonoBehaviour
{
    private int pos = 0;
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
        StartCoroutine(Pulsante());
        GameObject.Find("lampadario/SpotLight").SetActive(false);
        material1 = (Material)Resources.Load("wall_conScritta", typeof(Material));
        GameObject.Find("room_4/Room.001").GetComponent<Renderer>().material = material1;
    }

    private IEnumerator Pulsante()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        while (pos < 80)
        {
            pos += 4;
            transform.Rotate(0, 0, 4, Space.Self);
            yield return new WaitForSeconds(0f);
        }
    }
}
