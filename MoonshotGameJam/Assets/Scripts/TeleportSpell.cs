using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpell : MonoBehaviour
{
    public FinalWitchScript finalWitchScript;
    public bool hasTeleported;
    public AudioSource exitSound;
    public void Teleport(){
        finalWitchScript.Teleport();
        hasTeleported = true;
    }

    public void HasTeleportedCheck(){
        if(hasTeleported){
            exitSound.Play();
            if(finalWitchScript.activateFire || finalWitchScript.activateLightning){
                finalWitchScript.enemyState = "casting";
                finalWitchScript.teleportToCenter = false;
            } else{
                finalWitchScript.enemyState = "basic";
               
            }
             finalWitchScript.CalculateNewPos();
            
            gameObject.SetActive(false);
            
        }
    }
}
