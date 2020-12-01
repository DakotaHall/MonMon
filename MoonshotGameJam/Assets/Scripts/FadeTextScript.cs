using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeTextScript : MonoBehaviour
{
    public bool beginFade;
    public TextMeshPro textMesh;
    public float fadeTime;

    void OnEnable(){
        fadeTime = Time.time + 5f;
        textMesh.color = new Color(1,1,1,1);
    }
    void Update()
    {
        if(Time.time > fadeTime ){
            beginFade = true;
        }
        if(beginFade){
            textMesh.color = new Color(1,1,1,textMesh.color.a - 1*Time.deltaTime);
            if(textMesh.color.a <= 0){
                textMesh.text = "";
                gameObject.SetActive(false);
                beginFade = false;
            }
        }

    }
}
