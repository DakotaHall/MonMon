using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonlightScript : MonoBehaviour
{
    public float moonLight = 100f;
    public bool depleting = false;
    public GameObject moonInside;
    void Update()
    {
        if(depleting){
            if(moonLight > 0){
                moonLight -= 4*Time.deltaTime;
                if(moonLight < 0){
                    moonLight = 0;
                }
            }
            
        } else{
            if(moonLight < 100){
                moonLight += 4*Time.deltaTime;
                if(moonLight > 100){
                    moonLight = 100;
                }
            }
            

        }
        moonInside.transform.localEulerAngles = new Vector3(0,-180 + moonLight*180/100,0);
        if(moonLight < 50f){
            moonInside.GetComponent<SpriteRenderer>().color = Color.black;
        } else{
            moonInside.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
