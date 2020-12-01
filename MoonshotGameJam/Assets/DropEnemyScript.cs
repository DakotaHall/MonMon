using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnemyScript : MonoBehaviour
{
    public Vector3 leftRaycastPos;
    public Vector3 rightRaycastPos;
    public bool dropping;
    public Rigidbody2D myRigidbody;
    public LayerMask playerMask;
    public BoxCollider2D boxCollider;
    public BoxCollider2D dropCollider;
    public bool lifting;
    public Vector3 startPos;
    public float liftSpeed;
    public float waitTime;
    public bool waiting;
    public Animator myAnim;
    public float dropSpeed;
    public PlayerScript player;
    public float resetTime;
    public bool reset;
    public float dropLength;
    public float waitForDropTime;
    public bool waitingForDrop;
    public AudioSource dropSound;
    void Start()
    {
        startPos = transform.position;
    }



    void FixedUpdate()
    {
        if(reset){
            if(Time.time > resetTime){
                reset = false;
            }
        }
        if(!dropping){
             myRigidbody.velocity = Vector3.zero;
            if(lifting){
                if(transform.position.y > startPos.y){
                    lifting = false;
                    myAnim.SetBool("Lifting",false);
                } else{
                    if(waiting){
                        if(Time.time > waitTime){
                            waiting = false;
                            dropCollider.enabled = false;
                            boxCollider.enabled = true;
                            myAnim.SetTrigger("Lift");
                            myAnim.SetBool("Lifting",true);
                        } 
                    } else{
                         transform.Translate(Vector3.up*liftSpeed*Time.deltaTime);
                    }
                   
                }
            } else{
                RaycastHit2D leftRay = Physics2D.Raycast(new Vector3(boxCollider.bounds.min.x,transform.position.y,0),Vector3.down,dropLength,playerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector3(boxCollider.bounds.max.x,transform.position.y,0),Vector3.down,dropLength,playerMask);
        RaycastHit2D midRay = Physics2D.Raycast(transform.position,Vector3.down,dropLength,playerMask);
                //  Debug.DrawRay(transform.position,Vector3.down*dropLength,Color.red);
                //  Debug.DrawRay(new Vector3(boxCollider.bounds.min.x,transform.position.y,0),Vector3.down*dropLength,Color.red);
                //  Debug.DrawRay(new Vector3(boxCollider.bounds.max.x,transform.position.y,0),Vector3.down*dropLength,Color.red);
                transform.position = startPos;
                if((leftRay || rightRay || midRay) && !reset && !waitingForDrop){
                    waitingForDrop = true;
                    waitForDropTime = Time.time + .1f;
                   
        } else{
            if(waitingForDrop){
                if(Time.time > waitForDropTime){
                 dropping = true;
                    waitingForDrop = false;
                    boxCollider.enabled = false;
                    dropCollider.enabled = true;
                    
                    lifting = false;
                    waiting = false;
                    myAnim.SetTrigger("Drop");
            }
            }
            
        }
            } 
            
        } else{
            dropSpeed += 5*Time.deltaTime;
            myRigidbody.velocity = new Vector3(0,-dropSpeed,0);

        }
        
        
    }

    void OnCollisionEnter2D(Collision2D other){
        if((other.gameObject.tag == "Ground" || other.gameObject.tag == "Boat" || other.gameObject.tag == "Death") && dropping){
            dropping = false;
            lifting = true;
            waiting = true;
            waitTime = Time.time + 2f;
            myRigidbody.velocity = Vector3.zero;
            dropSpeed = 20;
            myAnim.SetTrigger("GroundTrigger");
            dropSound.Play();
            
        } else if(other.gameObject.tag == "Player"){
            if(dropping){
                other.collider.gameObject.GetComponent<PlayerScript>().health = 0f;
            }
        }
    }

    public void Reset(){
        reset = true;
        resetTime = Time.time + 1f;
        dropping = false;
        lifting = false;
        waiting = false;
        myRigidbody.velocity = Vector3.zero;
        dropCollider.enabled = false;
        boxCollider.enabled = true;
        myAnim.Rebind();
        transform.position = startPos;
    }
}
