﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public GameObject obj;
    public float throwForce = 800;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    private AudioSource audio;
    public int dmg;
    private bool touched = false;
    private bool hasObjectAlready;
    public Thrower thrower;
    private bool isPointing;
    float maxDist;
    [SerializeField] public Image Lancia;
    [SerializeField] public Image Prendimi;
    //[SerializeField] public Image Target1;


    void Start()
    {
        //Target1.color = Color.green;
        audio = GetComponent<AudioSource>();
        obj = GameObject.Find("FPSController");
        player = obj.transform;
        playerCam = GameObject.Find("FirstPersonCharacter").transform;
        thrower = obj.GetComponent<Thrower>();
        isPointing = false;
        maxDist = 2f;
    }

    void Update()
    {
        hasObjectAlready = thrower.hasObject;
        //float dist = Vector3.Distance(gameObject.transform.position, player.position);

        RaycastHit hit;
        Physics.Raycast(playerCam.position, playerCam.transform.forward, out hit, maxDist);

        /* if (hit.transform.GetComponent<ThrowObject>()!=null)
         {
             isPointing = true;
         }*/
        if (hit.transform != null)
        {
            if (this.name == hit.transform.name)
            {
                Prendimi.enabled = true;
                Target1.enabled = true;
                hasPlayer = true;
                if (hasPlayer && Input.GetKeyDown(KeyCode.E) && !hasObjectAlready)
                {
                    Prendimi.enabled = false;
                    Lancia.enabled = true;
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.parent = playerCam;
                    beingCarried = true;
                    thrower.hasObject = true;
                }
            }
            else
            {
                Target1.enabled = false;
                Prendimi.enabled = false;
                hasPlayer = false;
            }   
        }
        if (beingCarried)
        {
            Prendimi.enabled = false;
            if (touched)
            {
                Prendimi.enabled = false;
                Lancia.enabled = false;
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
                thrower.hasObject = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Prendimi.enabled = false;
                Lancia.enabled = false;
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                thrower.hasObject = false;
                //RandomAudio();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Prendimi.enabled = false;
                Lancia.enabled = false;
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                thrower.hasObject = false;
            }
        }
    }

    void RandomAudio()
    {
        if (audio.isPlaying)
        {
            return;
        }
        audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        audio.Play();

    }
    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }

    public bool IsPointingThrowable()
    {
        return isPointing;
    }
}
