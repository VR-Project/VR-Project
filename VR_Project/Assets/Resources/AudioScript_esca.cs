using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript_esca : MonoBehaviour
{
    private AudioSource activeSound;
    private AudioSource fluoSound;

    // Start is called before the first frame update
    void Start()
    {
        activeSound = GetComponent<AudioSource>();
        activeSound.clip = Resources.Load<AudioClip>("escaAttivata");
        fluoSound = GetComponent<AudioSource>();
        fluoSound.clip = Resources.Load<AudioClip>("escaFosforescente");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
