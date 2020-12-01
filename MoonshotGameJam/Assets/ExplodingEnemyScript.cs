using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ExplodingEnemyScript : MonoBehaviour
{
    public EnemyHealthScript enemyHealth;
    public float attackCooldown;
    public float attackCooldownTime = 3f;
    public bool movingOntoScreen = true;
    public Vector3 goToPosition;
    public Transform boatTransform;
    public Transform target;
    public float moveSpeed;
    public Vector3 wiggleUpPos;
    public Vector3 wiggleDownPos;
    public bool movingUp;
    public bool canAttack;
    public GameObject projectile;
    public GameObject player;
    public Animator myAnim;
    public SpriteRenderer sprite;
    public bool damaged;
    public float damageTime;
    public Vector3 rotatePoint;
    public float rotationAngle;
    public float descentSpeed = 5f;
    public GameObject explosion;
    public bool exploding = false;
    public bool rising = false;
    public Light2D enemyLight;
    public Vector3 initialPos;
    public bool JUSTWAIT;
    public Vector3 tutorialPos;
    public bool tutorial;
    public AudioSource explosionSound;

    void Awake()
    {
        initialPos = transform.position;
        goToPosition = transform.position;
        if(tutorial){
            goToPosition.y = -3f;
        } else{
            goToPosition.y = Random.Range(10f,12f);
        }
        
        
       myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!exploding){
            if(target.position.x < transform.position.x){
            transform.localScale = new Vector3(-1,1,1);
        }
         if(movingOntoScreen){
                if(!JUSTWAIT){
                    transform.Translate(Vector3.down*descentSpeed*Time.deltaTime);
                }
                
                if(transform.position.y <= goToPosition.y){
                    wiggleUpPos = transform.position + Vector3.up*.5f;
                    wiggleDownPos = transform.position + Vector3.down*.5f;
                    movingOntoScreen = false;
                    canAttack = true;
                    attackCooldown = Time.time + attackCooldownTime;
                    rotatePoint = transform.position + Vector3.down;
                }
            
           
        } else{
            if(canAttack && !tutorial){
                if(Time.time > attackCooldown){
                    moveSpeed += 1*Time.deltaTime;
                   transform.Translate((target.position - transform.position).normalized*moveSpeed*Time.deltaTime);
                } else{
                    rotationAngle += 5 * Time.deltaTime;
                    Vector3 offset = new Vector3(Mathf.Sin(rotationAngle),Mathf.Cos(rotationAngle),0)*1f;
                    transform.position = rotatePoint + offset;
                }
            } else{
                
                if(movingUp){
                transform.Translate(Vector3.up*Time.deltaTime);
                if(transform.position.y >= wiggleUpPos.y){
                    movingUp = false;
                }
            } else{
                transform.Translate(Vector3.down*Time.deltaTime);
                if(transform.position.y <= wiggleDownPos.y){
                    movingUp = true;
                }
            }
            }
            
        }
        if(enemyHealth.health <= 0){
           myAnim.SetTrigger("ExplosionTrigger");
           exploding = true;
        } else{
            if(damaged){
                if(Time.time > damageTime){
                    sprite.color = Color.white;
                }
            }
        }
        } else{
            if(rising){
                if(transform.localScale.x < 0){
                    transform.localScale += new Vector3(-1f,1f,0)*Time.deltaTime;
                } else{
                    transform.localScale += new Vector3(1f,1f,0)*Time.deltaTime;
                }
                enemyLight.intensity += 2*Time.deltaTime;
            }
            if(Time.time > damageTime){
                sprite.color = Color.white;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Attack"){
            enemyHealth.health -= 34f;
        }
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Boat"){
            damaged = true;
            damageTime = Time.time + .1f;
            canAttack = false;
            exploding = true;
             myAnim.SetTrigger("ExplosionTrigger");
        }
    }
    public void Reset(){
        if(initialPos != Vector3.zero){
            transform.position = initialPos;
        }
        rotationAngle = 0;
        enemyHealth.health = enemyHealth.originalHealth;
        myAnim.Rebind();
        gameObject.SetActive(true);
        exploding = false;
        explosion.SetActive(false);
        canAttack = false;
        enemyLight.intensity = 1;
        movingOntoScreen = true;
        transform.localScale = Vector3.one;
    }
    public void Explode(){
        explosion.SetActive(true);
        explosionSound.Play();
        rising = false;
    }
    public void Donezo(){
        gameObject.SetActive(false);
    }
    public void Rising(){
        rising = true;
    }


}
