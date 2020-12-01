using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBoiScript : MonoBehaviour
{
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject fire4;
    public GameObject fire5;
    public GameObject[] fire;
    public int fireNum = 0;
    public float fireStartSpacing;
    public float rotateSpeed;
    public Vector3 minYPosition;
    public Rigidbody2D myRigidbody;
    public Vector3 originalPos;
    public bool goingDown;
    public float bounceSpeed;
    void Start()
    {
        minYPosition = transform.position - Vector3.up*.1f;
        originalPos = transform.position;
    }

    void Update()
    {

        
        transform.localEulerAngles += Vector3.forward*Time.deltaTime*rotateSpeed;
    }

    void OnCollisionEnter2D(Collision2D other){
    }
}
