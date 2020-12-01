using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenScript : MonoBehaviour
{
    public SpriteRenderer fadeScreen;
    public bool fadeIn;
    public bool fadeOut;
    public float fadeTime;
    
    void Start()
    {
        fadeIn = true;
        fadeTime = Time.time + 5f;
    }

    void Update()
    {
    
         if(fadeIn){
             fadeScreen.color = new Color(0,0,0,fadeScreen.color.a-1*Time.deltaTime);
            if(fadeScreen.color.a <= 0){
                fadeIn = false;
            }
         } 
         else if(fadeOut){
            fadeScreen.color = new Color(0,0,0,fadeScreen.color.a+1*Time.deltaTime);
            if(fadeScreen.color.a <= 0){
               // fadeOut = false;
            }
        }
    }
}
