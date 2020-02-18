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
    [SerializeField] private Image customImage;

    private bool _pointingInteractable;
    private bool _pointingGrabbable;
    private bool _pointingPick;
    private bool _pointingOpen;
    private bool _pointingOpenCab;
    private bool _pointingRotatable;
    private bool _pointingExamine;
    private bool _pointingLeva;
    private bool _pointingMoveQ;
    private bool _pointingBrick;
    private bool _pointingPremuto;

    /******AUDIO BOOLEANS******/
    private bool bigliettoEsaminato = false;
    private bool chiaveInserita = false;
    private bool disegnoEsaminato = false;


    private bool fluo;
    private bool coltello;
    private bool _pointingCut;
    private bool pickOk;

    private CharacterController fpsController;
    private Vector3 rayOrigin;
    private Grabbable _grabbedObject = null;
    private Rotatable _rotatedObject = null;
    private PickUp _pickedObject = null;
    private Openable _openedObject = null;
    private OpenCabinet _openedCabObject = null;
    private Examine _examinedObject = null;
    private MoveLabirinto _movedObject = null;
    public PremiBottone _premuto = null;
    public GameObject portaCassaforte;
    public CollisionColorChanger colorChanger = null;
    public Rigidbody collisionRigidBody = null;


    private GameObject scrigno;

    private GameObject amo;

    private GameObject esca;
    private GameObject fish;
    private GameObject fish1;
    private GameObject fish2;
    private GameObject fish3;
    private GameObject fish4;
    private GameObject fish5;
    private GameObject fish6;
    private CharacterCollisionDetecter colli1;
    private CharacterCollisionDetecter colli2;
    private CharacterCollisionDetecter colli3;
    private CharacterCollisionDetecter colli4;
    private CharacterCollisionDetecter colli5;
    private CharacterCollisionDetecter colli6;
    private CharacterCollisionDetecter colli7;

    private Material material1;

    int counter0 = 0;
    int counter1 = 0;
    int counter2 = 0;
    int counter3 = 0;
    int counterFish = 0;
    int random;
    int previousRandom;

    public GameObject amoColtello;
    public Vector3 finalPositionColtello;
    private List<string> leve_arrivate = new List<string>();

    bool angolo = false;
    bool angOcc2 = false;
    bool angOcc3 = false;
    bool angOcc4 = false;

    float posScrigno = 0;
    float angScrigno = 0;

    int pos = 0;


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
        customImage.enabled = false;
        fpsController = GetComponent<CharacterController>();
        amoColtello = GameObject.Find("amoColtello");
        finalPositionColtello = new Vector3(6.883f, 0.021f, 1.915f);
        amo = GameObject.Find("amo");
        esca = amo.transform.GetChild(0).Find("esca1").gameObject;
        esca.AddComponent(typeof(EscaScript));
        esca.AddComponent(typeof(PickUp));
        esca.AddComponent(typeof(CollisionColorChanger));
        fluo = true;
        coltello = false;
        pickOk = false;
    }

    void Update()
    {
        rayOrigin = _fpsCameraT.position + fpsController.radius * _fpsCameraT.forward;
        CheckInteraction();

        if (fluo == true)
        {
            StartCoroutine(Fluo());
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_grabbedObject != null)
            {
                if (_grabbedObject.name == ("tazza"))
                {
                    /*ThrowSimulation proiettile = _grabbedObject.gameObject.GetComponent<ThrowSimulation>();
                    StartCoroutine(proiettile.SimulateProjectile());*/

                    Debug.Log("Sono dentro porca troia bastarda");
                    InvisibleGun gun = GetComponent<InvisibleGun>();
                    gun.getObject(_grabbedObject.gameObject);
                    Drop();
                }
                else
                {
                    Drop();
                }
            }
            else
                Push();
        }
        UpdateUITarget();
        if (_debugRay)
            DebugRaycast();
    }

    IEnumerator Fluo()
    {
        fluo = false;
        if (!esca.activeSelf)
        {

            if (pickOk == true)
            {
                counterFish++;
            }
            if (counterFish == 5)
            {
                if (pos <= 50)
                {
                    pos = pos + 1;
                    amoColtello.transform.Translate(0, -0.01f, 0);
                    yield return new WaitForSeconds(.01f);
                    counterFish--;
                    fluo = true;
                }
                else
                {
                    esca = amoColtello.transform.GetChild(0).Find("coltello").gameObject;
                    esca.AddComponent(typeof(PickUp));
                    coltello = true;
                    FindObjectOfType<AudioManager>().Play("InterazioneImportante");
                }
            }
            else
            {
                EscaScript script = esca.GetComponent<EscaScript>();
                script.DestroyInstance();
                PickUp pick = esca.GetComponent<PickUp>();
                pick.DestroyInstance();
                if (counterFish >= 1)
                {
                    CollisionColorChanger color = esca.GetComponent<CollisionColorChanger>();
                    color.DestroyInstance();
                    FindObjectOfType<AudioManager>().StopPlaying("EscaFosforescente");

                }
                material1 = (Material)Resources.Load("Esca", typeof(Material));
                esca.GetComponent<Renderer>().material = material1;
                yield return new WaitForSeconds(3);
                esca.transform.gameObject.SetActive(true);
                esca.tag = "Untagged";
                random = Random.Range(0, 8) + 1;
                if (random == previousRandom)
                {
                    if (random == 9)
                    {
                        random = random - 1;
                    }
                    random = random + 1;
                }
                previousRandom = random;
                //random = 1;
                amo = GameObject.Find("amo (" + random + ")");
                Debug.Log("amo (" + random + ")");
                esca = amo.transform.GetChild(0).Find("esca").gameObject;
                esca.AddComponent(typeof(EscaScript));
                esca.AddComponent(typeof(PickUp));
                esca.AddComponent(typeof(CollisionColorChanger));
                esca.tag = "Target";
                pickOk = false;

            }
        }
        fluo = true;
    }

    IEnumerator openScrigno()
    {
        scrigno = GameObject.Find("labirinto_wayPoints/Scrigno_corpo");
        yield return new WaitForSeconds(6f);
        while (posScrigno <= 0.5f)
        {
            posScrigno = posScrigno + 0.01f;
            scrigno.gameObject.transform.Translate(0, 0, 0.005f);
            yield return new WaitForSeconds(.05f);
        }
        while (angScrigno <= 60)
        {
            angScrigno += 1f;
            scrigno.transform.GetChild(0).gameObject.transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(.01f);
        }
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
                if (Input.GetMouseButtonDown(1))
                    interactable.Interact(gameObject);
            }

            //Check if is grabbable
            Grabbable grabbableObject = hit.transform.GetComponent<Grabbable>();
            _pointingGrabbable = grabbableObject != null ? true : false;
            if (_pointingGrabbable && _grabbedObject == null)
            {
                customImage.enabled = true;
                if (Input.GetKeyDown(KeyCode.E) && _grabbedObject == null)
                {
                    customImage.enabled = false;
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
                    pickOk = true;
                    FindObjectOfType<AudioManager>().Play("Interazione");
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
                    if (chiaveInserita == false)
                    {
                        FindObjectOfType<AudioManager>().Play("ChiaveSerratura");
                        chiaveInserita = true;
                    }
                    FindObjectOfType<AudioManager>().Play("AperturaCassetto");
                }
                else if (Input.GetKeyDown(KeyCode.E) && _openedObject != null)
                {
                    openableObject.Close();
                    Close();
                    FindObjectOfType<AudioManager>().Play("AperturaCassetto");

                }
            }

            //Check if is MoveQuadro
            Translate moveQuadroObject = hit.transform.GetComponent<Translate>();
            _pointingMoveQ = moveQuadroObject != null ? true : false;
            if (_pointingMoveQ)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(moveQuadroObject.MoveQ());
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
                    FindObjectOfType<AudioManager>().Play("AperturaSportello");

                }
                else if (Input.GetKeyDown(KeyCode.E) && _openedCabObject != null)
                {
                    openableCabObject.Close();
                    CloseCab();
                    FindObjectOfType<AudioManager>().Play("AperturaSportello");

                }
            }

            //Check if is rotatable
            Rotatable rotatableObject = hit.transform.GetComponent<Rotatable>();
            _pointingRotatable = rotatableObject != null ? true : false;
            if (_pointingRotatable)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rotatableObject.Rotate();
                    FindObjectOfType<AudioManager>().Play("RotelleCassaforte");
                    Rotate(rotatableObject);

                    if (rotatableObject.name == "Cylinder.000")
                    {
                        if (counter0 < 5) counter0++;
                        else counter0 = 0;
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
                        FindObjectOfType<AudioManager>().Play("InterazioneImportante");
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
                    if (examinableObject.gameObject == GameObject.Find("bigliettoAereo") && bigliettoEsaminato == false)
                    {
                        FindObjectOfType<AudioManager>().Play("voce_biglietto");
                        bigliettoEsaminato = true;
                    }
                    else if (examinableObject.gameObject == GameObject.Find("foto") && disegnoEsaminato == false)
                    {
                        FindObjectOfType<AudioManager>().Play("voce_disegno");
                        disegnoEsaminato = true;
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("Interazione");
                    }
                    examinableObject.ClickObject();
                    Examine(examinableObject);
                }
                if (examinableObject.examineMode == false && _examinedObject == examinableObject)
                {
                    ExitExamine();
                }

            }



            //Check if is BrickInTheWall
            BrickIndietro BrickInTheWall = hit.transform.GetComponent<BrickIndietro>();
            _pointingBrick = BrickInTheWall != null ? true : false;
            if (_pointingBrick)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BrickInTheWall.Torna();
                }
            }

            //Check if is PremiBottone
            PremiBottone premibile = hit.transform.GetComponent<PremiBottone>();
            _pointingPremuto = premibile != null ? true : false;
            if (_pointingPremuto)
            {
                if (Input.GetKeyDown(KeyCode.E) && _premuto == null)
                {
                    premibile.Premi();
                    Premi(premibile);
                }
            }

            //Check if coltello
            if (coltello == true)
            {
                GameObject.Find("reteCentrale").AddComponent(typeof(PickUp));
            }


            // Check if is movelabirinto
            MoveLabirinto movableObject = hit.transform.GetComponent<MoveLabirinto>();
            _pointingLeva = movableObject != null ? true : false;
            if (_pointingLeva)
            {
                // Capisco angolo da considerare
                if (movableObject.name == "1")
                {
                    angolo = false;
                }
                else if (movableObject.name == "2" || movableObject.name == "3" || movableObject.name == "4")
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
                    leve_arrivate.Remove(movableObject.name);
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
                if (leve_arrivate.Contains("1") && leve_arrivate.Contains("4") && leve_arrivate.Contains("6") && leve_arrivate.Contains("9"))

                {
                    leve_arrivate.Clear();
                    StartCoroutine(openScrigno());
                }
            }
            //Check if collision
            //Creare i vari fish che richiamano OnCollisionEnter che ritorna un boolean che chiama ciò che è nell'if
            /*if (colli1.OnCollisionEnter()== true || colli2.OnCollisionEnter() == true || colli3.OnCollisionEnter() == true || colli4.OnCollisionEnter() == true || colli5.OnCollisionEnter() == true || colli6.OnCollisionEnter() == true || colli7.OnCollisionEnter() == true)
            {
                pickOk = false;
                esca.SetActive(false);
                //StartCoroutine(Coroutine);
            }*/
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
            _pointingMoveQ = false;
            _pointingBrick = false;
            _pointingPremuto = false;
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
        else if (_pointingMoveQ)
            _target.color = Color.green;
        else if (_pointingRotatable)
            _target.color = Color.green;
        else if (_pointingExamine)
            _target.color = Color.green;
        else if (_pointingLeva)
            _target.color = Color.green;
        else if (_pointingBrick)
            _target.color = Color.green;
        else if (_pointingPremuto)
            _target.color = Color.green;
        else
        {
            _target.color = Color.white;
            customImage.enabled = false;
        }


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
        //_target.enabled = false;
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
    }

    private void Premi(PremiBottone premibile)
    {
        _premuto = premibile;
    }



    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.white);
    }
}
