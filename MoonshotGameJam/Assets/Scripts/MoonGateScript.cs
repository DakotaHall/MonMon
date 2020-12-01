using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoonGateScript : MonoBehaviour
{
    public GameObject pointer;
    public FadeScreenScript fadeScreen;
    void Update()
    {
        if(pointer.activeSelf){
            if(fadeScreen.fadeOut && fadeScreen.fadeScreen.color.a >= 1){
                SceneManager.LoadScene("TreeLevel",LoadSceneMode.Single);
            }
            if(Input.GetKeyDown(KeyCode.E)){
                fadeScreen.fadeOut = true;
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            pointer.SetActive(true);
        }
    }

 void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            pointer.SetActive(false);
        }
    }
    
}
