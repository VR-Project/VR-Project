using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApriPorta4 : MonoBehaviour
{
    private float pos4 = 0;
    public static bool apri4 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (apri4 == true)
        {
            StartCoroutine(Apri4());
            apri4 = false;
        }
    }

    private IEnumerator Apri4()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.GetComponent<AudioSource>().Play();
        while (pos4 > -100)
        {
            pos4 -= 1f;
            transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
