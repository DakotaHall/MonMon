using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealthScript : MonoBehaviour
{
    public float originalHealth;
    public float health = 100f;
    public bool damaged;
    public SpriteMask damageMaskSprite;
    public GameObject damageMask;
    public float damageTime;

    void Update()
    {
        
        if(damaged){
            damageMaskSprite.sprite = GetComponent<SpriteRenderer>().sprite;
            if (Time.time > damageTime)
                {
                    damaged = false;
                    damageMask.SetActive(false);
                    damageMaskSprite.enabled = false;
                }
        }
         
    }

    public void Damage(){
        if(!damageMask.activeSelf){
            damageMask.SetActive(true);
            damaged = true;
            damageMaskSprite.enabled = true;
        } 

            if(health > 0){
                damageTime = Time.time + .2f;
            } else{
                damageTime = Time.time + .1f;
            }
            
            
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack" && !damaged)
        {
            health -= 34f;
            Damage();
        }
    }
}
