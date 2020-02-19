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
    [SerializeField] private Image Esamina;
    [SerializeField] private Image Interagisci;
    [SerializeField] private Image Prendi;
    [SerializeField] private Image Rilascia;
    [SerializeField] private Image Ruota;
    [SerializeField] private Image Striscia;

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
    private bool _pointingInterruttore;
    private bool _pointingTastoCassaforte;
    private bool _pointingThrowable;
    private bool _pointingsedia;

    /******AUDIO BOOLEANS******/
    private bool bigliettoEsaminato = false;
    private bool chiaveInserita = false;
    private bool disegnoEsaminato = false;


    private bool fluo;
    private bool coltello;
    private bool _pointingCut;
    private bool pickOk;
    private int numeroCombinazione = 0;
    private int numeroCorretto = 0;
    private bool combCorretta = false;
    private bool coltelloPreso = false;
    private bool funk = false;

    private CharacterController fpsController;
    private Vector3 rayOrigin;
    private Grabbable _grabbedObject = null;
    private Rotatable _rotatedObject = null;
    private PickUp _pickedObject = null;
    private Openable _openedObject = null;
    private OpenCabinet _openedCabObject = null;
    private Examine _examinedObject = null;
    private PremoTastoCassaforte _tastoPremuto = null;
    private MoveLabirinto _movedObject = null;
    private MoveSedia _movedSedia = null;
    public PremiBottone _premuto = null;
    public SpegniLuce _spenta = null;

    public GameObject portaCassaforte;
    public CollisionColorChanger colorChanger = null;
    public Rigidbody collisionRigidBody = null;
    

    private GameObject scrigno;
    private GameObject cassaforte;
    private Material tastierinoRosso;
    private Material tastierinoVerde;
    private GameObject cassaforteStanza4;
    private GameObject cassaforteStanza1;

    private GameObject amo;
    private GameObject pickedColtello;
    private GameObject Tastierino;

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
    float posCass1 = 0;
    float posCass4 = 0;
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
        Prendi.enabled = false;
        Interagisci.enabled = false;
        Esamina.enabled = false;
        Ruota.enabled = false;
        Rilascia.enabled = false;
        Striscia.enabled = false;
        fpsController = GetComponent<CharacterController>();
        amoColtello = GameObject.Find("amoColtello");
        finalPositionColtello = new Vector3(6.883f, 0.021f, 1.915f);
        pickedColtello = amoColtello.transform.GetChild(0).Find("coltello").gameObject;
        amo = GameObject.Find("amo");
        esca = amo.transform.GetChild(0).Find("esca1").gameObject;
        tastierinoRosso = (Material)Resources.Load("Tastierino_Rosso");
        tastierinoVerde = (Material)Resources.Load("Tastierino_Verde");
        Tastierino = GameObject.Find("cassaforte_stanza4/Anta/Tastierino");
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

        if(coltelloPreso == true)
        {
            coltelloPreso = false;
            esca = GameObject.Find("reteCentrale").gameObject;
            esca.AddComponent(typeof(PickUp));
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_grabbedObject != null)
            {
                if (_grabbedObject.gameObject.tag == "proiettile")
                {
                    //ThrowSimulation proiettile = _grabbedObject.gameObject.GetComponent<ThrowSimulation>();
                    //StartCoroutine(proiettile.SimulateProjectile());
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
            if (counterFish >= 5)
            {
                if (pos <= 50)
                {
                    pos = pos + 1;
                    amoColtello.transform.Translate(0, -0.01f, 0);
                    yield return new WaitForSeconds(.01f);
                    counterFish++;
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
               // Debug.Log("amo (" + random + ")");
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

    public IEnumerator AproCassaforteStanza1()
    {
        cassaforteStanza1 = GameObject.Find("Room_new/cassaforte/PortaCassaforte");
        yield return new WaitForSeconds(1.6f);
        FindObjectOfType<AudioManager>().Play("InterazioneImportante");
        while (posCass1 > -70)
        {
            posCass1 -= 1f;
            cassaforteStanza1.gameObject.transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(.02f);
        }
    }

    public IEnumerator AproCassaforteStanza4()
    {
        cassaforteStanza4 = GameObject.Find("cassaforte_stanza4/Anta");
        yield return new WaitForSeconds(0.3f);
        while (posCass4 < 70)
        {
            posCass4 += 1f;
            cassaforteStanza4.gameObject.transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(.02f);
        }
        
        yield return new WaitForSeconds(.4f);
        GameObject sasso = GameObject.Find("Masso").transform.GetChild(0).gameObject;
        sasso.SetActive(true);
        sasso.GetComponent<Rigidbody>().AddForce(0, 0, -50f);
        ApriPorta.apri = true;

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
                Prendi.enabled = true;
                if (Input.GetKeyDown(KeyCode.E) && _grabbedObject == null)
                {
                    Prendi.enabled = false;
                    Rilascia.enabled = true;
                    grabbableObject.Grab(gameObject);
                    Grab(grabbableObject);
                }
            }

            //Check if is pickable
            PickUp pickableObject = hit.transform.GetComponent<PickUp>();
            _pointingPick = pickableObject != null ? true : false;
            if (_pointingPick)
            {
                Prendi.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Prendi.enabled = false;
                    if (pickableObject.gameObject == pickedColtello && funk == false)
                    {   
                        coltelloPreso = true;
                        funk = true;
                    }
                    if (pickableObject.gameObject.name == "reteCentrale")
                    {
                        pickableObject.GetComponent<AudioSource>().Play();
                        Debug.Log("Sono entrato");
                    }
                    else FindObjectOfType<AudioManager>().Play("Interazione");
                    pickableObject.transform.gameObject.SetActive(false);
                    pickOk = true;

                    PickUp(pickableObject);
                }
            }

            //Check if is pulsanteluce
            SpegniLuce interruttore = hit.transform.GetComponent<SpegniLuce>();
            _pointingInterruttore = interruttore != null ? true : false;
            if (_pointingInterruttore)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interruttore.Spegni();
                    //PickUp(pickableObject);
                }
            }

            //Check if is openable
            Openable openableObject = hit.transform.GetComponent<Openable>();
            _pointingOpen = openableObject != null ? true : false;
            if (_pointingOpen)
            {
                Interagisci.enabled = true;
                if (Input.GetKeyDown(KeyCode.E) && _pickedObject != null && _openedObject == null )
                {
                    Interagisci.enabled = false;
                    StartCoroutine(openableObject.Open()); 
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
                    StartCoroutine(openableObject.Close());
                    Close();
                    FindObjectOfType<AudioManager>().Play("AperturaCassetto");

                }
            }
            if (_pointingOpen && _examinedObject == null){
                Interagisci.enabled = true;
            }
            else Interagisci.enabled = false;
            if (_pointingExamine)
            {
                Interagisci.enabled = false;
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

            //Check if is sedia
            MoveSedia sedia = hit.transform.GetComponent<MoveSedia>();
            _pointingsedia = sedia != null ? true : false;
            if (_pointingsedia && _movedSedia== null)
            {
                Interagisci.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interagisci.enabled = false;
                    StartCoroutine(sedia.MoveS());
                    MoveSedia(sedia);
                }
            }

            //Check if is openable
            OpenCabinet openableCabObject = hit.transform.GetComponent<OpenCabinet>();
            _pointingOpenCab = openableCabObject != null ? true : false;
            if (_pointingOpenCab)
            {
                Interagisci.enabled = true;
                if (Input.GetKeyDown(KeyCode.E) && _openedCabObject == null)
                {
                    Interagisci.enabled = false;
                    StartCoroutine(openableCabObject.Open());
                    OpenCab(openableCabObject);
                    FindObjectOfType<AudioManager>().Play("AperturaSportello");

                }
                else if (Input.GetKeyDown(KeyCode.E) && _openedCabObject != null)
                {
                    StartCoroutine(openableCabObject.Close());
                    CloseCab();
                    FindObjectOfType<AudioManager>().Play("AperturaSportello");

                }
            }

            //Check if is rotatable
            Rotatable rotatableObject = hit.transform.GetComponent<Rotatable>();
            _pointingRotatable = rotatableObject != null ? true : false;
            if (_pointingRotatable)
            {
                Interagisci.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interagisci.enabled = false;
                    StartCoroutine(rotatableObject.Rotate());
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
                        StartCoroutine(AproCassaforteStanza1());
                    }
                }
            }

            //Check if is examinable
            Examine examinableObject = hit.transform.GetComponent<Examine>();
            _pointingExamine = examinableObject != null ? true : false;
            if (_pointingExamine)
            {
                if (Input.GetKeyDown(KeyCode.E) && _examinedObject == null)
                {
                    if (examinableObject.gameObject == GameObject.Find("bigliettoAereo") && bigliettoEsaminato == false)
                    {
                        FindObjectOfType<AudioManager>().Play("voce_biglietto");
                        bigliettoEsaminato = true;
                    }
                    else if (examinableObject.gameObject == GameObject.Find("foto") && disegnoEsaminato == false)
                    {
                        FindObjectOfType<AudioManager>().Play("voce_disegno");
                        ApriPorta.apri = true;
                        disegnoEsaminato = true;
                    }
                    else if (examinableObject.gameObject == GameObject.Find("Room/labirinto_wayPoints/Scrigno_corpo/anello"))
                    {
                        //FindObjectOfType<AudioManager>().Play("voce_disegno");
                        ApriPorta.apri = true;
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
            if (_pointingExamine && _examinedObject == null)
            {
                Esamina.enabled = true;
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

            //Check if is lanciabile
            ThrowObject lanciabile = hit.transform.GetComponent<ThrowObject>();
            _pointingThrowable = lanciabile != null ? true : false;

            //Check if is PremiTastoCassaforte
            PremoTastoCassaforte digitabile = hit.transform.GetComponent<PremoTastoCassaforte>();
            _pointingTastoCassaforte = digitabile != null ? true : false;
            if (_pointingTastoCassaforte)
            {
                if (Input.GetMouseButtonDown(0) && _tastoPremuto == null)
                {
                    numeroCombinazione++;
                    digitabile.PremoTasto();
                    StartCoroutine(digitabile.PremoTasto());

                    if (digitabile.name == "2_numero" && numeroCorretto == 0)
                    {
                        numeroCorretto++;
                    }
                    else if (digitabile.name == "8_numero" && numeroCorretto == 1)
                    {
                        numeroCorretto++;
                    }
                    else if (digitabile.name == "8_numero" && numeroCorretto == 2)
                    {
                        numeroCorretto++;
                    }
                    else if (digitabile.name == "2_numero" && numeroCorretto == 3)
                    {
                        numeroCorretto++;
                        combCorretta = true;
                    }

                    if(numeroCombinazione == numeroCorretto && combCorretta == true)
                    {
                        //Debug.Log("combinazione corretta");
                        //Tastierino.GetComponent<Renderer>().material = tastierinoVerde;
                        numeroCombinazione = 0;
                        numeroCorretto = 0;
                        combCorretta = false;
                        StartCoroutine(AproCassaforteStanza4());
                    }
                    else if (numeroCombinazione == 4 && combCorretta == false)
                    {
                        //Tastierino.GetComponent<Renderer>().material = tastierinoRosso;
                        //Debug.Log("combinazione errata");
                        numeroCombinazione = 0;
                        numeroCorretto = 0;
                    }
                }
            }

            //Check if coltello
            /*if (coltello == true)
            {
                GameObject.Find("reteCentrale").AddComponent(typeof(PickUp));
            }*/

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
            _pointingInterruttore = false;
            _pointingTastoCassaforte = false;
            _pointingThrowable = false;
            _pointingsedia = false;
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
        else if (_pointingInterruttore)
            _target.color = Color.green;
        else if (_pointingTastoCassaforte)
            _target.color = Color.green;
        else if (_pointingThrowable)
            _target.color = Color.green;
        else if (_pointingsedia && _movedSedia==null)
            _target.color = Color.green;
        else
        {
            _target.color = Color.white;
            Prendi.enabled = false;
            Interagisci.enabled = false;
            Esamina.enabled = false;
            Ruota.enabled = false;
            Rilascia.enabled = false;
            Striscia.enabled = false;
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
    private void MoveSedia(MoveSedia sedia)
    {
        _movedSedia = sedia;
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
        Esamina.enabled = false;
        Ruota.enabled = true;
        Interagisci.enabled = false;
        _pointingOpen = false;
    }

    public void ExitExamine()
    {
        _examinedObject = null;
        Ruota.enabled = false;
    }

    private void Rotate(Rotatable rotatable)
    {
        _rotatedObject = rotatable;
    }

    private void Premi(PremiBottone premibile)
    {
        _premuto = premibile;
    }

    private void PremoTasto(PremoTastoCassaforte digitabile)
    {
        _tastoPremuto = digitabile;
    }


    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.white);
    }
}
