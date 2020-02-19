using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolle : MonoBehaviour
{
    static bool bolle = false;
    private float pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).transform.GetChild(3).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bolle == true) 
        {
            AttivoBolle();
            bolle = false;
        }
        
    }

    public static void MostraBolle()
    {
        bolle = true;
    }

    public void AttivoBolle()
    {
        GameObject.Find("room_4/Room.001/tetto_s4").GetComponent<BoxCollider>().gameObject.SetActive(true);
        for(int i = 0; i < 4; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            MovimentoBolle bolla = transform.GetChild(i).transform.GetComponent<MovimentoBolle>();
            bolla.MuoviBolle();
        }
       
    }
}
