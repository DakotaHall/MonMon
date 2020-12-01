using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughScript : MonoBehaviour
{
    public Collider2D passThroughCollider;
    public float disableTime;
    public bool disabled = false;
    void Start()
    {
        passThroughCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(disabled && Time.time > disableTime){
            disabled = false;
            passThroughCollider.enabled = true;
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.S)){
            passThroughCollider.enabled = false;
            disableTime = Time.time + .5f;
            disabled = true;
        }
    }
}
