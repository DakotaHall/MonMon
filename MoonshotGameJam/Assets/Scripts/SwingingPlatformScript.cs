using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatformScript : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 midPoint;
    public Vector3 endPoint;
    public Transform platformTransform;
    public Transform leftTransform;
    public Transform rightTransform;
    public bool goingRight;
    public float swingHeight;
    public float count = 0;
    public bool waiting;
    public float waitTime;
    void Start()
    {
        if(goingRight){
            startPos = leftTransform.position;
        endPoint = rightTransform.position;
        } else{
            startPos = rightTransform.position;
        endPoint = leftTransform.position;
        }
        
        midPoint = startPos + (endPoint - startPos)/2 + Vector3.up*swingHeight;

    }

    void FixedUpdate()
    {
        if(waiting){
            if(Time.time > waitTime){
                waiting = false;
            }
        } else{
            count += .5f * Time.deltaTime;
        Vector3 m1 = Vector3.Lerp( startPos, midPoint, count );
        Vector3 m2 = Vector3.Lerp( midPoint, endPoint, count );
        platformTransform.position = Vector3.Lerp(m1, m2, count);
        if(Vector3.Distance(platformTransform.position,endPoint) < .01f){
            waiting = true;
            waitTime = Time.time + 2f;
            if(goingRight){
                startPos = rightTransform.position;
                endPoint = leftTransform.position;
                midPoint = startPos + (endPoint - startPos)/2 + Vector3.up*swingHeight;
                goingRight = false;
                count = 0;
        } else{
            startPos = leftTransform.position;
            endPoint = rightTransform.position;
            midPoint = startPos + (endPoint - startPos)/2 + Vector3.up*swingHeight;
                goingRight = true;
                count = 0;
        }
        }
        }
        
        
    }
}
