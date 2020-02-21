﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSedia : MonoBehaviour
{
    private float posX = 0;
    private float posZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator MoveS()
    {
        yield return new WaitForSeconds(.03f);
        FindObjectOfType<AudioManager>().Play("SediaTranslate");

        while (posX < 0.5 && posZ<1.5)
        {
            posX += 0.01f;
            posZ += 0.03f;
            gameObject.transform.Translate(-0.01f,  0, 0.03f);
            yield return new WaitForSeconds(.02f);
        }
    }
}
