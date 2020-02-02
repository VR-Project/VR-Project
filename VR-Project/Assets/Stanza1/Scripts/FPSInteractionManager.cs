using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;

public class FPSInteractionManager : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private float _pushDistance;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _rotateDistance;

    [SerializeField] private Image _target;

    private bool _pointingInteractable;
    private bool _pointingGrabbable;
    private bool _pointingPick;
    private bool _pointingOpen;
    private bool _pointingRotatable;

    private CharacterController fpsController;
    private Vector3 rayOrigin;

    private Grabbable _grabbedObject = null;
    private Rotatable _rotatedObject = null;
    private PickUp _pickedObject = null;
    private Openable _openedObject = null;


    public float InteractionDistance
    {
        get { return _interactionDistance; }
        set { _interactionDistance = value; }
    }

    public Grabbable GrabbedObject
    {
        set { _grabbedObject = value; }
    }
    public Rotatable RotatedObject
    {
        set { _rotatedObject = value; }
    }

    void Start()
    {
        fpsController = GetComponent<CharacterController>();
    }

    void Update()
    {
        rayOrigin = _fpsCameraT.position + fpsController.radius * _fpsCameraT.forward;

        if(_grabbedObject == null)
            CheckInteraction();
        if (_rotatedObject == null)
            CheckInteraction();
        if (_pickedObject == null)
            CheckInteraction();
        if (_openedObject == null)
            CheckInteraction();

        if (Input.GetMouseButtonDown(0))
        {
            if (_grabbedObject != null)
                Drop();
            else
                Push();
        }



        UpdateUITarget();

        if (_debugRay)
            DebugRaycast();
    }

    private void CheckInteraction()
    {
        
        Ray ray = new Ray(rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            //Check if is interactable
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            _pointingInteractable = interactable != null ? true : false;
            if (_pointingInteractable)
            {
               
                if(Input.GetMouseButtonDown(1))
                    interactable.Interact(gameObject);
            }

            //Check if is grabbable
            Grabbable grabbableObject = hit.transform.GetComponent<Grabbable>();
            _pointingGrabbable = grabbableObject != null ? true : false;
            if (_pointingGrabbable && _grabbedObject == null)
            {

                /*if (Input.GetMouseButtonDown(1))
                {
                    grabbableObject.Grab(gameObject);
                    Grab(grabbableObject);
                }*/
                if (Input.GetKeyDown(KeyCode.E) && _grabbedObject == null)
                {
                    grabbableObject.Grab(gameObject);
                    Grab(grabbableObject);
                }                

            }

            //Check if is pickable
            PickUp pickableObject = hit.transform.GetComponent<PickUp>();
            _pointingPick = pickableObject != null ? true : false;
            if (_pointingPick)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickableObject.transform.gameObject.SetActive(false);
                    PickUp(pickableObject);
                }

            }

            //Check if is openable
            Openable openableObject = hit.transform.GetComponent<Openable>();
            _pointingOpen = openableObject != null ? true : false;
            if (_pointingOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && _pickedObject != null)
                {
                    openableObject.Open();
                }

            }

            //Check if is rotatable
            Rotatable rotatableObject = hit.transform.GetComponent<Rotatable>();
            _pointingRotatable = rotatableObject != null ? true : false;
            //_pointingRotatable = true;
            if (_pointingRotatable && _rotatedObject == null)
            {

                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Sono entrato nell'if");

                    rotatableObject.Rotate(gameObject);
                    
                    Rotate(rotatableObject);
                }

            }
        }
        else
        {
            _pointingInteractable = false;
            _pointingGrabbable = false;
            _pointingRotatable = false;
        }

    }

    private void UpdateUITarget()
    {
        if (_pointingInteractable)
            _target.color = Color.green;
        else if (_pointingGrabbable)
            _target.color = Color.yellow;
        else if (_pointingPick)
            _target.color = Color.yellow;
        else if (_pointingOpen)
            _target.color = Color.yellow;
        else if (_pointingRotatable)
            _target.color = Color.yellow;
        else
            _target.color = Color.red;
    }

   private void Push()
    {
        Ray ray = new Ray(rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _pushDistance))
        {
            Rigidbody otherRigidbody = hit.rigidbody;
            if (otherRigidbody != null)
            {
                otherRigidbody.AddForce(-hit.normal * _pushForce);
            }
        }
    }

    private void Drop()
    {
        if (_grabbedObject == null)
            return;

        _grabbedObject.transform.parent = _grabbedObject.OriginalParent;
        _grabbedObject.Drop();

        _target.enabled = true;
        _grabbedObject = null;


    }

    private void Grab(Grabbable grabbable)
    {
        _grabbedObject = grabbable;
        grabbable.transform.SetParent(_fpsCameraT);
        Vector3 grabPosition = _fpsCameraT.position + transform.forward * _grabDistance;

        _target.enabled = false;
    }

    private void PickUp(PickUp pickable)
    {
        _pickedObject = pickable;
        //_target.enabled = false;
    }

    private void Rotate(Rotatable rotatable)
    {
        _rotatedObject = rotatable;
        //rotatable.transform.SetParent(_fpsCameraT);
        //rotatable.transform.Rotate(0,0, _rotateDistance);
        GameObject cil = GameObject.Find("Cylinder");
        Debug.Log("Cylinder e' " + cil);
        cil.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Debug.Log("Dovrei aver ruotato");
        //_target.enabled = false;

    }

    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.red);
    }
}
