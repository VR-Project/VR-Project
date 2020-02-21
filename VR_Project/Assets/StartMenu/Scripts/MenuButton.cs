using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

	private GameObject cameraInizio;
	private GameObject miaCamera;
	private GameObject cameraOpzioni;
	private GameObject Canvas;

    // Update is called once per frame
    void Update()
    {
		miaCamera = GameObject.Find("CameraMenu");
		cameraInizio = GameObject.Find("EmptyPre");
		cameraOpzioni = GameObject.Find("EmptyOpzioni");
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1){
				animator.SetBool ("pressed", true);

			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
				if (thisIndex == 0)
				{
					miaCamera.SetActive(false);
					RenderSettings.fog = true;
					cameraInizio.transform.GetChild(0).gameObject.SetActive(true);
				}
				if (thisIndex == 1)
				{
					miaCamera.SetActive(false);
					cameraOpzioni.transform.GetChild(0).gameObject.SetActive(true);
				}
				if(thisIndex == 2)
				{
					Application.Quit();
				}
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
