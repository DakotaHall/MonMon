using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGateRingLightScript : MonoBehaviour
{
    public float liveTime;
    public GameObject nextLight;

    void OnEnable(){
        liveTime = Time.time + .2f;
    }
    void Update()
    {
        if(Time.time > liveTime){
            nextLight.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
