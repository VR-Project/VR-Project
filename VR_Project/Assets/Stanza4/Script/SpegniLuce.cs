using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpegniLuce : MonoBehaviour
{
    private int pos = 0;
    Material material1;
    Material emissivo;
    Material noEmissivo;

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
        GameObject.Find("lampadario/Point Light (2)").SetActive(false);
        material1 = (Material)Resources.Load("wall_conScritta", typeof(Material));
        noEmissivo = (Material)Resources.Load("vetroLampadario", typeof(Material));
        GameObject.Find("lampadario/vetroLampadario").GetComponent<Renderer>().material = noEmissivo;
        GameObject.Find("room_4/Room.001").GetComponent<Renderer>().material = material1;
    }

    public void Accendi()
    {
        StartCoroutine(PulsanteSu());
        GameObject.Find("room_4/lampadario/SpotLight").SetActive(true);
        GameObject.Find("room_4/lampadario/Point Light (2)").SetActive(true);
        material1 = (Material)Resources.Load("wall", typeof(Material));
        GameObject.Find("room_4/Room.001").GetComponent<Renderer>().material = material1;
        emissivo = (Material)Resources.Load("vetroLampadarioEmission", typeof(Material));
        GameObject.Find("lampadario/vetroLampadario").GetComponent<Renderer>().material = emissivo;
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

    private IEnumerator PulsanteSu()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        while (pos > 0)
        {
            pos -= 4;
            transform.Rotate(0, 0, -4, Space.Self);
            yield return new WaitForSeconds(0f);
        }
    }
}
