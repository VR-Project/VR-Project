using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Crouch : MonoBehaviour {

    public CharacterController characterController;
    public bool isCrouched = false;
    private bool guardaSotto = false;
    [SerializeField] private Image Striscia;

    void Start ()
    {
        characterController = gameObject.GetComponent<CharacterController>();        
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched == false)
        {
            Striscia.enabled = true;
            characterController.height = 0.3f;
            isCrouched = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched==true)
        {
            if(guardaSotto == true) transform.localScale += new Vector3(0.0f, 0.8f, 0);
            
            characterController.height = 1.8f;
            isCrouched = false;
            guardaSotto = false;
            Striscia.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.RightControl) && isCrouched == true && guardaSotto == false)
        {
            guardaSotto = true;
            transform.localScale += new Vector3(0.0f, -0.8f, 0);
            Striscia.enabled = false;
        }
    }
}
