using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuScript1 : MonoBehaviour
{

    public static AudioMenuScript1 instance1;

    void Start()
    {
        if (instance1 != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance1 = this;
            gameObject.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
        }
    }

}
