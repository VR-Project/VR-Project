﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotate()
    {
        transform.Rotate(0.0f, 0.0f, 60.0f, Space.Self);
    }

}