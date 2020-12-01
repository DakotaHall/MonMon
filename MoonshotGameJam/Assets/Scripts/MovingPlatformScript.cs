using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float moveSpeed;
    public bool goingToEnd = true;
    public float waitTime;
    public bool waiting;
    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if(goingToEnd){
             if(Vector2.Distance(transform.position,endPos) < .01f){
                waiting = true;
                waitTime = Time.time + 1f;
                goingToEnd = false;
            } else{
                if(waiting){
                    if(Time.time > waitTime){
                        waiting = false;
                    }
                } else{
                    transform.position = Vector2.MoveTowards(transform.position,endPos,moveSpeed*Time.deltaTime);
                }

            }
            
           
        } else{
             if(Vector2.Distance(transform.position,startPos) < .01f){
                waiting = true;
                waitTime = Time.time + 1f;
                goingToEnd = true;
            } else{
                if(waiting){
                    if(Time.time > waitTime){
                        waiting = false;
                    }
                } else{
                    transform.position = Vector2.MoveTowards(transform.position,startPos,moveSpeed*Time.deltaTime);
                }
            }
            
            
        }
    }
}
