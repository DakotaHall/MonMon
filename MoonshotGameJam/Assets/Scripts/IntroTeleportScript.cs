using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTeleportScript : MonoBehaviour
{
    public AudioSource exitSound;
    public WitchDialogueScript witchDialogueScript;


    public void ActivateWitch(){
        witchDialogueScript.ActivateWitch();
    }

    public void ActivateDialogue(){
        witchDialogueScript.ActivateDialogue();
        gameObject.SetActive(false);
    }

    public void PlayExitSound(){
        exitSound.Play();
    }
}
