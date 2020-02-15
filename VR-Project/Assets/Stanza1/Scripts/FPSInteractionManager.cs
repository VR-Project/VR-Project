using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;
using System.Collections.Generic;

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
    private bool _pointingOpenCab;
    private bool _pointingRotatable;
    private bool _pointingExamine;
    private bool _pointingLeva;

    private CharacterController fpsController;
    private Vector3 rayOrigin;

    private Grabbable _grabbedObject = null;
    private Rotatable _rotatedObject = null;
    private PickUp _pickedObject = null;
    private Openable _openedObject = null;
    private OpenCabinet _openedCabObject = null;
    private Examine _examinedObject = null;
    private MoveLabirinto _movedObject = null;
    public GameObject portaCassaforte;
    int counter0 = 0;
    int counter1 = 0;
    int counter2 = 0;
    int counter3 = 0;
    private List<string> leve_arrivate= new List<string>();
    bool angolo = false;
    bool angOcc2 = false;
    bool angOcc3 = false;
    bool angOcc4 = false;



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
                if (Input.GetKeyDown(KeyCode.E) && _pickedObject != null && _openedObject == null)
                {
                    openableObject.Open();
                    Open(openableObject);
                }
                else if (Input.GetKeyDown(KeyCode.E) && _openedObject != null)
                {
                        openableObject.Close();
                        Close();
                }

            }

            //Check if is openable
            OpenCabinet openableCabObject = hit.transform.GetComponent<OpenCabinet>();
            _pointingOpenCab = openableCabObject != null ? true : false;
            if (_pointingOpenCab)
            {
                if (Input.GetKeyDown(KeyCode.E) && _openedCabObject == null)
                {
                    openableCabObject.Open();
                    OpenCab(openableCabObject);
                }
                else if (Input.GetKeyDown(KeyCode.E) && _openedCabObject != null)
                {
                    openableCabObject.Close();
                    CloseCab();
                }

            }

            //Check if is rotatable
            Rotatable rotatableObject = hit.transform.GetComponent<Rotatable>();
            _pointingRotatable = rotatableObject != null ? true : false;
            //_pointingRotatable = true;
            if (_pointingRotatable)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    rotatableObject.Rotate();
                    
                    Rotate(rotatableObject);

                    if (rotatableObject.name == "Cylinder.000")
                    {
                        if (counter0 < 5) counter0++;
                        else
                        {
                            counter0 = 0;
                            Debug.Log("azzera");
                        }
                        
                    }

                    else if (rotatableObject.name == "Cylinder.001")
                    {
                        if (counter1 < 5) counter1++;
                        else counter1 = 0;
                    }

                    else if (rotatableObject.name == "Cylinder.002")
                    {
                        if (counter2 < 5) counter2++;
                        else counter2 = 0;
                    }

                    else if (rotatableObject.name == "Cylinder.003")
                    {
                        if (counter3 < 5) counter3++;
                        else counter3 = 0;
                    }

                    if (counter0 == 4 && counter1 == 1 && counter2 == 5 && counter3 == 0)
                    {
                        portaCassaforte = GameObject.Find("Room_new/cassaforte/PortaCassaforte");
                        portaCassaforte.gameObject.transform.Rotate(0, -70, 0);
                    }
                }

            }

            //Check if is examinable
            Examine examinableObject = hit.transform.GetComponent<Examine>();
            _pointingExamine = examinableObject != null ? true : false;
            if (_pointingExamine)
            {

                if (Input.GetMouseButtonDown(0) && _examinedObject == null)
                {
                    examinableObject.ClickObject();  
                    Examine(examinableObject);
                }
                if (examinableObject.examineMode == false && _examinedObject == examinableObject )
                {
                    ExitExamine();
                }

            }

            //Check if is movelabirinto
            MoveLabirinto movableObject = hit.transform.GetComponent<MoveLabirinto>();
            _pointingLeva = movableObject != null ? true : false;

            if (_pointingLeva)
            {
                // Capisco angolo da considerare
                if (movableObject.name == "2" || movableObject.name == "3" || movableObject.name == "4")
                {
                    angolo = angOcc2;
                }
                else if (movableObject.name == "5" || movableObject.name == "6")
                {
                    angolo = angOcc3;
                }
                else if (movableObject.name == "7" || movableObject.name == "8" || movableObject.name == "9" || movableObject.name == "10")
                {
                    angolo = angOcc4;
                }


                if (Input.GetKeyDown(KeyCode.E) && !leve_arrivate.Contains(movableObject.name) && !angolo)
                {
                    StartCoroutine(movableObject.MoveAlongWaipointsCoroutine());
                    leve_arrivate.Add(movableObject.name);
                    
                    // Setto flag se angolo già occupato
                    if (movableObject.name == "2" || movableObject.name == "3" || movableObject.name == "4")
                    {
                        angOcc2 = true;
                    }
                    else if (movableObject.name == "5" || movableObject.name == "6")
                    {
                        angOcc3 = true;
                    }
                    else if (movableObject.name == "7" || movableObject.name == "8" || movableObject.name == "9" || movableObject.name == "10")
                    {
                        angOcc4 = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E) && leve_arrivate.Contains(movableObject.name))
                {
                    movableObject.tornaIndietro();
                    
                    // Setto flag false se angolo si libera
                    if (movableObject.name == "2" || movableObject.name == "3" || movableObject.name == "4")
                    {
                        angOcc2 = false;
                    }
                    else if (movableObject.name == "5" || movableObject.name == "6")
                    {
                        angOcc3 = false;
                    }
                    else if (movableObject.name == "7" || movableObject.name == "8" || movableObject.name == "9" || movableObject.name == "10")
                    {
                        angOcc4 = false;
                    }
                }

            }
        }
        else
        {
            _pointingInteractable = false;
            _pointingGrabbable = false;
            _pointingRotatable = false;
            _pointingExamine = false;
            _pointingOpen = false;
            _pointingOpenCab = false;
            _pointingPick = false;
            _pointingLeva = false;
        }

    }

    private void UpdateUITarget()
    {
        if (_pointingInteractable)
            _target.color = Color.green;
        else if (_pointingGrabbable)
            _target.color = Color.green;
        else if (_pointingPick)
            _target.color = Color.green;
        else if (_pointingOpen)
            _target.color = Color.green;
        else if (_pointingOpenCab)
            _target.color = Color.green;
        else if (_pointingRotatable)
            _target.color = Color.green;
        else if (_pointingExamine)
            _target.color = Color.green;
        else if (_pointingLeva)
            _target.color = Color.green;
        else
            _target.color = Color.white;
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
    }

    private void Open(Openable openable)
    {
        _openedObject = openable;

    }

    private void Close()
    {
        _openedObject = null;
    }

    private void OpenCab(OpenCabinet openCab)
    {
        _openedCabObject = openCab;
    }
    private void CloseCab()
    {
        _openedCabObject = null;
    }

    private void Examine(Examine examinable)
    {
        _examinedObject = examinable;
    }

    public void ExitExamine()
    {
       _examinedObject = null;
    }


    private void Rotate(Rotatable rotatable)
    {
        _rotatedObject = rotatable;
        //rotatable.transform.SetParent(_fpsCameraT);
        //rotatable.transform.Rotate(0,0, _rotateDistance);
        //GameObject cil = GameObject.Find("Cylinder");
        //Debug.Log("Cylinder e' " + cil);
        //cil.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        //Debug.Log("Dovrei aver ruotato");
        ////_target.enabled = false;

    }

    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.red);
    }
}
