using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField]
    public SC_FPSController PlayerController;


    [SerializeField]
    private MinigameTrigger PomPomTrigger;
    [SerializeField]
    private MinigamePomPom PomPomScript;
    private bool PomPomActive;

    private GameObject PomPomUI;


    private void Update()
    {
        if(PomPomTrigger.PlayerInRange && PomPomActive == false)//Press E to start minigame while in trigger
        {
            //PomPomUI.SetActive(true);

            if (Input.GetKey(KeyCode.E))//Minigame Start
            {
                PlayerController.enabled = false;
                PomPomActive = true;
                PomPomScript.enabled = true;
                PomPomScript.MinigameStart();
            }
        }

        if(PomPomActive == true && Input.GetKey(KeyCode.Escape))//Exit during minigame
        {
            PlayerController.enabled = true;
            PomPomActive = false;
            PomPomScript.MinigameEnd(false);
            PomPomScript.enabled = false;
        }

        else if(!PomPomTrigger.PlayerInRange && PomPomActive == true)//Deactive UI when leaving trigger
        {
            PomPomActive = false;
            //PomPomUI.SetActive(false);

        }
    }
}
