using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

    public GameObject pauseMenu;
    private GameObject fps;
    public GameObject opzioni;
    // Update is called once per frame
    void Update()
    {
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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                }
				if (thisIndex == 1)
				{
                    SceneManager.LoadScene(8);
				}
				if(thisIndex == 2)
				{
					Application.Quit();
				}
                if (thisIndex == 3)
                {
                    pauseMenu.SetActive(false);
                    GameObject.Find("Fps/FPSController").GetComponent<FirstPersonController>().enabled = true;
                }
                if (thisIndex == 4)
                {
                    pauseMenu.SetActive(false);
                    GameObject.Find("Fps/FPSController").GetComponent<FirstPersonController>().enabled = true;
                    opzioni.SetActive(true);
                }
                if (thisIndex == 5)
                {
                    Application.Quit();
                }
            }
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
