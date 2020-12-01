using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class UrchinBoiScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform target;
    //public float enemyHealth = 50f;
    public EnemyHealthScript enemyHealth;
    public bool exploding;
    public Animator myAnim;
    public GameObject explosion;
    public bool canMove;
    public CircleCollider2D circleCollider;
    public Vector3 startPos;
    public AudioSource explodeSound;
    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
       
        if(!exploding){
            if(canMove){
                if(target.position.x > transform.position.x){
            transform.localScale = Vector3.one;
            transform.Translate(Vector3.right*moveSpeed*Time.deltaTime);
        } else{
            transform.localScale = new Vector3(-1,1,1);
            transform.Translate(Vector3.left*moveSpeed*Time.deltaTime);
        }
            }
             
            if(enemyHealth.health <= 0 && !exploding){
                exploding = true;
                myAnim.SetTrigger("Explode");
               explodeSound.Play();
            }
        } else{
            if(transform.localScale.x < 0){
                transform.localScale = transform.localScale + new Vector3(-1,1,0)*Time.deltaTime;
            } else{
                transform.localScale = transform.localScale + new Vector3(1,1,0)*Time.deltaTime;
            }

        }
    }

    public void Explode(){
        explosion.SetActive(true);
        
    }

    public void DestroyThis(){
        gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            exploding = true;
            circleCollider.enabled = false;
            myAnim.SetTrigger("Explode");
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Boat"){
            exploding = true;
            circleCollider.enabled = false;
            myAnim.SetTrigger("Explode");
            explodeSound.Play();
        } else if(other.gameObject.tag == "Player"){
            
        }
    }

    public void Reset(){
       
        explosion.SetActive(false);
        circleCollider.enabled = true;
        exploding = false;
        enemyHealth.health = enemyHealth.originalHealth;
        myAnim.Rebind();
       if(startPos != Vector3.zero){
            transform.position = startPos;
        }
        gameObject.SetActive(false);
       
    }
}
