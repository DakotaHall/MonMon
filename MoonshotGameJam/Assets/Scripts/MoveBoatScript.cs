using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBoatScript : MonoBehaviour
{
    public float boatHealth = 100;
    public Rigidbody2D myRigidBody;
    public SpriteRenderer bottom;
    public SpriteRenderer middle;
    public SpriteRenderer top;
    public Sprite healthyTop;
    public Sprite healthyMid;
    public Sprite healthyBottom;
    public Sprite damagedTop;
    public Sprite damagedMid;
    public Sprite damagedBottom;
    public Sprite rektTop;
    public Sprite rektMid;
    public Sprite rektBottom; 
    public float damageTime;
    public bool damaged;
    public BoatEnemySpawner boatEnemySpawner;
    public bool done;
    public PlayerScript player;
    public SpriteRenderer fadeSprite;
    public FadeScreenScript fadeScreenScript;
    public bool startMovingAgain;
    void Update()
    {
        if(boatHealth <= 0){
            gameObject.SetActive(false);
        }
        if(damaged){
            if(Time.time > damageTime){
                damaged = false;
                top.color = Color.white;
                middle.color = Color.white;
                bottom.color = Color.white;
            }
        }
        if(fadeScreenScript.fadeOut && transform.position.x > 38){
            if(fadeScreenScript.fadeScreen.color.a >= 1){
                    SceneManager.LoadScene("ForestScene", LoadSceneMode.Single);
                }
        }
    }

    void FixedUpdate(){
        if(!done){
            if(transform.position.x < 0 && boatEnemySpawner.waveCleared){
            myRigidBody.MovePosition(transform.position + Vector3.right * Time.fixedDeltaTime/6);
        } else{
            if(transform.position.x > 0){
                transform.position = new Vector3(0,transform.position.y,0);
            }
        }
        } else{
            if(player.CheckGrounded() && player.transform.parent == null && !startMovingAgain){
                player.transform.SetParent(this.transform);
                player.SetState("talking");
                player.myRigidbody.velocity = Vector3.zero;
                startMovingAgain = true;
            }
            if(startMovingAgain){
                myRigidBody.MovePosition(transform.position + Vector3.right * Time.fixedDeltaTime*5);
            }
            
            if(transform.position.x > 38){
                fadeScreenScript.fadeOut = true;
                
            }
        }
        
        
    }

    public void TakeDamage(){
        if(!damaged){
             damaged = true;
            damageTime = Time.time + .1f;
            top.color =  new Color(1,0,0,1);
            middle.color = new Color(1,0,0,1);
            bottom.color = new Color(1,0,0,1);
        boatHealth -= 20;
        if(boatHealth == 20){
            top.sprite = rektTop;
            middle.sprite = rektMid;
            bottom.sprite = rektBottom;
        } else if(boatHealth == 60){
            top.sprite = damagedTop;
            middle.sprite = damagedMid;
            bottom.sprite = damagedBottom;
        }
        }
       
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Explosion"){
            TakeDamage();
        
        }
    }
}
