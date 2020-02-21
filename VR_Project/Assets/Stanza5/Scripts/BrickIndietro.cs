using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickIndietro : MonoBehaviour
{
    private Vector3[] BuchiPos = new Vector3[5];
    string nomeBuco;
    private bool tolto = true;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=1;  i < 6; i++)
        {
            GameObject curBuco = GameObject.Find("muro/Buchi/" + i.ToString());
            BuchiPos[i - 1] = curBuco.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Esci()
    {
        float posZ = 0;
        //float posY = 0;
        float posX = 0;
        FindObjectOfType<AudioManager>().Play("MattoneTolto");

        while (posZ < 0.2f)
        {
            posZ += 0.01f;
            gameObject.transform.Translate(0, 0, 0.01f);
            yield return new WaitForSeconds(.06f);
        }
        yield return new WaitForSeconds(0.3f);
        while ( posX<1.1)
        {
            //posY += 0.01f;
            posX += 0.01f;
            gameObject.transform.Translate(0.01f, 0, 0);
            yield return new WaitForSeconds(.03f);
        }
        Destroy(this.GetComponent<BrickIndietro>());
        this.gameObject.AddComponent<Grabbable>();
        GameObject.Find("muro/Buchi/" + nomeBuco).GetComponent<BoxCollider>().enabled = true;
        if (this.name == GameObject.Find("muro/Buchi/" + nomeBuco).name)
        {
            aperturaVarco.decrMattoniCorretti();
        }
        tolto = true;
    }

    public void Torna()
    {
        for (int i = 0; i < 5; i++)
        {
            if (this.transform.position == BuchiPos[i])
            {
                nomeBuco = (i + 1).ToString();
            }
        }
        if (tolto == true)
        {
            StartCoroutine(Esci());
            tolto = false;
        }
        
    }
}
