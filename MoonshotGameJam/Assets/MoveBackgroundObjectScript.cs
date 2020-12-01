using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundObjectScript : MonoBehaviour
{
    public Vector3 startPos;
    public float moveSpeed;
    public float minXPos;
    void OnEnable()
    {
        startPos = transform.localPosition;   
    }

    void Update()
    {
        if(minXPos > transform.localPosition.x){
            gameObject.SetActive(false);
        } else{
            transform.Translate(Vector3.left*moveSpeed*Time.deltaTime);
        }
        
    }

    public void Reset(){
        if(startPos != Vector3.zero){
            transform.position = startPos;
        }
        
        gameObject.SetActive(true);
    }
}
