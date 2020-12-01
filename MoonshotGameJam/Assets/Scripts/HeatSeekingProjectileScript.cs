using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeekingProjectileScript : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D myRigidbody;
    public float movementSpeed = 20f;
    public float rotateSpeed = 200f;
    public Animator myAnim;
    public bool dissipating;
    public float deathTime;
    public BoxCollider2D boxCollider;
    public AudioSource impactSound;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        deathTime = Time.time + 5f;
        
    }

    void FixedUpdate()
    {

        if(!dissipating){
            if(Time.time <= deathTime){
           
            Vector2 direction = (Vector2)target.position - myRigidbody.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction,transform.right).z;
        myRigidbody.angularVelocity = -rotateAmount * rotateSpeed;
        myRigidbody.velocity = transform.right*movementSpeed;
        
        } else{
            myRigidbody.angularVelocity = 0f;
        }
        } else{
            if(!impactSound.isPlaying){
                gameObject.SetActive(false);
            }
        }
        
        
        
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag == "Player" && !dissipating){
            impactSound.Play();
            other.GetComponent<PlayerScript>().takeDamage();
            dissipating = true;
            myAnim.SetTrigger("Dissipate");
        }
    }

    public void Dissipate(){
         GetComponent<SpriteRenderer>().enabled = false;
    }
}
