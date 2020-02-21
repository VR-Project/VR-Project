﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Grabbable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Transform _originalParent;

    public Transform OriginalParent
    {
        get { return _originalParent; }
        protected set { _originalParent = value; }
    }    
    // Use this for initialization
    void Start ()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _originalParent = transform.parent;
		
	}
	
    public void Grab(GameObject grabber)
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddTorque(5, 5, 5);

    }
}
