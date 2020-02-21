using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithWall : MonoBehaviour
{
    [SerializeField] private bool _logCollisions = true;

    [SerializeField] private float _blinkTime = 0.05f;
    [SerializeField] private Color _blinkColor;

    private GameObject target;
    private FPSInteractionManager Int;

    private Color _originalColor;
    private Renderer _renderer;
    private bool _isBlinking = false;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
            _originalColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (_logCollisions)
        //    Debug.Log("OnCollision ENTERFish");
        StartCoroutine(Blink());
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (_logCollisions)
        //    Debug.Log("OnCollision STAYFish");
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (_logCollisions)
        //    Debug.Log("OnCollision EXITFish");
    }

    public IEnumerator Blink()
    {
        if (_isBlinking)
            yield return null;

        _isBlinking = true;

        /*fps = GameObject.Find("FPSController");

        fps.GetComponent<FPSInteractionManager>().Collision();*/

        //Con rigidBody usare rigidBody.move da applicare nel FixedUpdate

        target = GameObject.FindWithTag("Fish");
        Vector3 targetDirection = target.transform.position + transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        //Rotate toward target direction
        float rotationStep = (6f) * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

        //Move object along its forward axis
        transform.Translate(Vector3.forward * (1f) * Time.deltaTime);
        //IS EQUIVALENT TO 
        //transform.Translate(transform.forward * movSpeed * Time.deltaTime, Space.World);

        yield return new WaitForSeconds(_blinkTime);

        _isBlinking = false;
    }

    public void DestroyInstance()
    {
        Destroy(this);
    }
}
