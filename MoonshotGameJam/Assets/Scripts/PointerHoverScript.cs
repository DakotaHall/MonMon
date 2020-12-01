using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerHoverScript : MonoBehaviour
{
    public Vector3 initialPos;
    public bool hover;
    public Vector3 upPos;
    public bool goingUp;
    // Start is called before the first frame update
    void OnEnable()
    {
        initialPos = transform.position;
        upPos = transform.position + Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if(hover){
            if(goingUp){
                transform.position = Vector3.MoveTowards(transform.position,upPos,1*Time.deltaTime);
                if(transform.position == upPos){
                    goingUp = false;
                }
            } else{
                transform.position = Vector3.MoveTowards(transform.position,initialPos,1*Time.deltaTime);
                if(transform.position == initialPos){
                    goingUp = true;
                }
            }
        }   
    }
}
