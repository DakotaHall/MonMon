using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;
 using UnityEngine.EventSystems;
public class ButtonSelectedScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject star;
    public AudioSource soundEffect;
    public MenuNavigationScript menuNavigationScript;
    public bool backButton;
    public void OnSelect(BaseEventData eventData)
     {

        if(Time.time > .02){
            soundEffect.Play();
        }
         
         if(!backButton){
              star.SetActive(true);
             menuNavigationScript.lastSelectedButton = this.gameObject;
         }
         
     }
     public void OnDeselect(BaseEventData eventData)
     {
                if(!backButton){
                    star.SetActive(false);
                }
             
         
         
     }
}
