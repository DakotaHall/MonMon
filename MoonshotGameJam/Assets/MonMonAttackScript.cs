using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonMonAttackScript : MonoBehaviour
{
    public PlayerScript player;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Heavy"){
            player.SetState("knockback");
            player.knockbackTime = Time.time + .1f;
            if(other.gameObject.transform.position.x > player.transform.position.x){
                player.myRigidbody.AddForce(Vector3.left*5f,ForceMode2D.Impulse);
                
            } else{
                player.myRigidbody.AddForce(Vector3.right*5f,ForceMode2D.Impulse);
            }
            
        }
    }
}
