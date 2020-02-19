using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColorChanger : MonoBehaviour
{
    [SerializeField] private bool _logCollisions = true;

    [SerializeField] private float _blinkTime = 0.05f;

    private GameObject io;
    private FPSInteractionManager Int;

    private Renderer _renderer;
    private bool _isBlinking = false;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_logCollisions)
            Debug.Log("OnCollision ENTER");
        StartCoroutine(Blink());
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (_logCollisions)
            //Debug.Log("OnCollision STAY");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_logCollisions)
            Debug.Log("OnCollision EXIT");
    }

    public IEnumerator Blink()
    {
        if (_isBlinking)
            yield return null;

        _isBlinking = true;

        /*fps = GameObject.Find("FPSController");

        fps.GetComponent<FPSInteractionManager>().Collision();*/

        io = GameObject.FindWithTag("Target");
        if (io != null)
        {
            io.SetActive(false);
        }

        yield return new WaitForSeconds(_blinkTime);

        _isBlinking = false;
    }

    public void DestroyInstance()
    {
        Destroy(this);
    }
}
