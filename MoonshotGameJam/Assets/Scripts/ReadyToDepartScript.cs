using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyToDepartScript : MonoBehaviour
{
    public GameObject pointer;
    public bool fading;
    public float fadeTime;
    public SpriteRenderer fadeScreen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && pointer.activeSelf){
            fading = true;
            fadeTime = Time.time + 2.5f;
        }
        if(fading){
            fadeScreen.color = new Color(0,0,0,fadeScreen.color.a+1*Time.deltaTime);
            if(Time.time > fadeTime){
                  SceneManager.LoadScene("BoatPractice", LoadSceneMode.Single);
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
