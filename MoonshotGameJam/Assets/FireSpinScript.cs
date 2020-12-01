using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpinScript : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject[] fires;
    public bool finalBoss;
    public float radius;
    public float rotationAngle;
    public float[] rotateAngles;
    void OnEnable()
    {
        if(finalBoss){
            for(int i = 0; i < fires.Length;i++){
                float xPos = radius * Mathf.Cos(Mathf.Deg2Rad*(360 - (360/16)*i));
                
                float yPos = radius * Mathf.Sin(Mathf.Deg2Rad*(360 - (360/16)*i));
                fires[i].transform.localPosition = new Vector3(xPos,yPos,0);
                if(i < 5){
                    fires[i].transform.localEulerAngles = new Vector3(0,0,90f-(22.5f*i));
                } else{
                    fires[i].transform.localEulerAngles = new Vector3(0,0,360-22.5f*(i-5));
                }
                fires[i].GetComponent<FinalBossFireProjectileScript>().rotationAngle =6f/(360f/(22.5f*(i+1)));
            }
        }
    }

    void OnDisable(){
        radius = 5f;
        if(finalBoss){
            for(int i = 0; i < fires.Length;i++){
                float xPos = radius * Mathf.Cos(Mathf.Deg2Rad*(360 - (360/16)*i));
                
                float yPos = radius * Mathf.Sin(Mathf.Deg2Rad*(360 - (360/16)*i));
                fires[i].transform.localPosition = new Vector3(xPos,yPos,0);
                if(i < 5){
                    fires[i].transform.localEulerAngles = new Vector3(0,0,90f-(22.5f*i));
                } else{
                    fires[i].transform.localEulerAngles = new Vector3(0,0,360-22.5f*(i-5));
                }
                fires[i].GetComponent<FinalBossFireProjectileScript>().rotationAngle =6f/(360f/(22.5f*(i+1)));
            }
        }
    }

    void Update()
    {
       
        for(int i = 0; i < fires.Length;i++){
            if(fires[i] != null){
                if(finalBoss){
                } else{
                    fires[i].transform.RotateAround(parentObject.transform.position+Vector3.up,Vector3.forward,90*Time.deltaTime);
                }
                
            }
            
        }
    }
}
