using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelLightScript : MonoBehaviour
{
    public FadeScreenScript fadeScreen;
    private bool levelEnd;
    void Update()
    {
        if( fadeScreen.fadeOut && fadeScreen.fadeScreen.color.a >= 1 && levelEnd){
            SceneManager.LoadScene("FinalBossLevel",LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && !levelEnd){
            fadeScreen.fadeOut = true;
            levelEnd = true;
        }
    }
}
