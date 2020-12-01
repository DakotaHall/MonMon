using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerScript : MonoBehaviour
{
    public GameObject platform;
    public Transform platformStart;
    public Transform platformEnd;
    public bool activated;
    public float moveSpeed;
    public LightAbilityScript lightAbility;
    void Update()
    {
        // if(platform != null){
        //     if(activated){
        //     if(lightAbility.ray1Object != this.gameObject && lightAbility.ray2Object != this.gameObject && lightAbility.ray3Object != this.gameObject){
        //         activated = false;
        //         return;
        //     }
        //     if(Vector3.Distance(platform.transform.position,platformEnd.position) > .1f){
        //         platform.transform.position = Vector2.MoveTowards(platform.transform.position,platformEnd.position,moveSpeed*Time.deltaTime);
        //     }
        // } else{
        //     if(Vector3.Distance(platform.transform.position,platformStart.position) > .1f){
        //         platform.transform.position = Vector2.MoveTowards(platform.transform.position,platformStart.position,moveSpeed/5*Time.deltaTime);
        //     }
            
        // }
        // }
        
    }
}
