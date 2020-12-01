using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyScript : MonoBehaviour
{
    //public float enemyHealth = 100;
    public float attackCooldown;
    public float attackCooldownTime = 3f;
    public bool movingOntoScreen = true;
    public Vector3 goToPosition;
    public Transform boatTransform;
    public float moveSpeed;
    public Vector3 wiggleUpPos;
    public Vector3 wiggleDownPos;
    public bool movingUp;
    public bool canAttack;
    public GameObject projectile;
    public GameObject player;
    public Vector3 rotatePoint;
    public float rotationAngle;
    public bool chilling = true;
    public Vector3 midPoint;
    public float count = 0.0f;
    public Vector3 startPos;
    public int flipVal = 1;
    public Animator myAnim;
    public bool casting = false;
    public Transform castTransform;
    public bool dying;
    public bool tripleShot;
    public bool teleporting;
    public Vector3 initialPos;
    public float movementBoundsLeft;
    public float movementBoundsRight;
    public float movementBoundsUp;
    public float movementBoundsDown;
    public Transform target;
    public EnemyHealthScript enemyHealth;
    public bool tutorial;
    public AudioSource castSound;
    public AudioSource deathSound;
    
    void Awake(){
        initialPos = transform.position;
        if(target.position.x < transform.position.x){
            goToPosition = transform.position + Vector3.left*100;
        } else{
            goToPosition = transform.position + Vector3.right*100;
        }

        
    }

    void Update()
    {
        if(!dying){
            if(target.transform.position.x < transform.position.x){
            transform.localScale = new Vector3(-1,1,1);
        } else{
            transform.localScale = Vector3.one;
        }
        if(!casting){
            if(chilling){
            if((transform.position.x < target.transform.position.x && transform.position.x > -24) || (transform.position.x > target.transform.position.x && transform.position.x < 24) ){
                myAnim.SetBool("Aggro",true);
                chilling = false;
                canAttack = true;
                attackCooldown = Time.time + attackCooldownTime;
                 wiggleUpPos = transform.position + new Vector3(Random.Range(-3,3),Random.Range(-3,3));
                    while(wiggleUpPos.x < movementBoundsLeft || wiggleUpPos.x > movementBoundsRight){
                        wiggleUpPos.x = transform.position.x + Random.Range(-3,3);
                    }
                    while(wiggleUpPos.y < movementBoundsDown || wiggleUpPos.y > movementBoundsUp){
                        wiggleUpPos.y = transform.position.y + Random.Range(-3,3);
                    }
                    startPos = transform.position;
                    
                    midPoint =  startPos +(wiggleUpPos -startPos)/2 +Vector3.up  *Random.Range(1f,2f)*flipVal;
            } else{
                if(transform.position.x > target.position.x){
                    transform.Translate(Vector3.left*moveSpeed*Time.deltaTime);
                } else{
                    transform.Translate(Vector3.right*moveSpeed*Time.deltaTime);
                }
            } 
        }
        else{
            if(canAttack){
            
                if(Time.time > attackCooldown && !tutorial){
                    casting = true;
                    myAnim.SetBool("Casting",true);
                    
                }
            }

             count += 1.0f *Time.deltaTime;

            Vector3 m1 = Vector3.Lerp( startPos, midPoint, count );
            Vector3 m2 = Vector3.Lerp( midPoint, wiggleUpPos, count );
            transform.position = Vector3.Lerp(m1, m2, count);
            if(Vector3.Distance(transform.position,wiggleUpPos) < .01f){
                wiggleUpPos = transform.position + new Vector3(Random.Range(-3,3),Random.Range(-3,3));
                 
                    while(wiggleUpPos.x < movementBoundsLeft || wiggleUpPos.x > movementBoundsRight ){
                        wiggleUpPos.x = transform.position.x + Random.Range(-3,3);
                    }
                    while(wiggleUpPos.y < movementBoundsDown || wiggleUpPos.y > movementBoundsUp){
                        wiggleUpPos.y = transform.position.y + Random.Range(-3,3);
                    }
                    rotatePoint = transform.position + (wiggleUpPos-transform.position)/2;
                    count = 0;
                    startPos = transform.position;
                    flipVal *= -1;
                    midPoint = startPos +(wiggleUpPos -startPos)/2 +Vector3.up *Random.Range(1f,2f)*flipVal;
            }
        }
        }
        
        if(enemyHealth.health <= 0 && !dying){
            dying = true;
            myAnim.SetTrigger("Die");
            deathSound.Play();
        }
        }
        
    }

    public void Reset(){
       if(initialPos != Vector3.zero){
            transform.position = initialPos;
        }
        count = 0f;
        myAnim.Rebind();
        enemyHealth.health = enemyHealth.originalHealth;
        gameObject.SetActive(true);
        dying = false;
        casting = false;
        chilling = true;
        canAttack = false;
    }

    void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.tag == "Attack"){
            enemyHealth.health -= 34f;
        }
    }

    public void Die(){
        gameObject.SetActive(false);
    }
    public void Fire(){
        castSound.Play();
        attackCooldown = Time.time + attackCooldownTime;
        GameObject newProjectile = Instantiate(projectile,castTransform.position,Quaternion.identity);
        newProjectile.transform.localScale = transform.localScale;
        Vector3 projectileDirection = target.transform.position - (castTransform.position - target.transform.position);
        newProjectile.GetComponent<ProjectileScript>().targetDirection = projectileDirection;
        if(tripleShot){
            GameObject secondProjectile = Instantiate(projectile,castTransform.position,Quaternion.identity);
            GameObject thirdProjectile = Instantiate(projectile,castTransform.position,Quaternion.identity);
            secondProjectile.transform.localScale = transform.localScale;
            thirdProjectile.transform.localScale = transform.localScale;
            Vector3 upPos = new Vector3((projectileDirection.x-castTransform.position.x)*Mathf.Cos(Mathf.PI/180*10f) - (projectileDirection.y-castTransform.position.y)*Mathf.Sin(Mathf.PI/180*10f),(projectileDirection.y-castTransform.position.y)*Mathf.Cos(Mathf.PI/180*10f) + (projectileDirection.x-castTransform.position.x)*Mathf.Sin(Mathf.PI/180*10f),0);
            Vector3 downPos = new Vector3((projectileDirection.x-castTransform.position.x)*Mathf.Cos(Mathf.PI/180*350f) - (projectileDirection.y-castTransform.position.y)*Mathf.Sin(Mathf.PI/180*350f),(projectileDirection.y-castTransform.position.y)*Mathf.Cos(Mathf.PI/180*350f) + (projectileDirection.x-castTransform.position.x)*Mathf.Sin(Mathf.PI/180*350f),0);
        
            upPos += castTransform.position;
            downPos += castTransform.position;
            secondProjectile.GetComponent<ProjectileScript>().targetDirection = upPos;
            thirdProjectile.GetComponent<ProjectileScript>().targetDirection = downPos;


        }
        casting = false;
        myAnim.SetBool("Casting",false);
    }
}
