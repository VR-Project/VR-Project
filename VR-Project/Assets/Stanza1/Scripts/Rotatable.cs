using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Rotatable : MonoBehaviour
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
    void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _originalParent = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotate(GameObject rotater)
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }
}