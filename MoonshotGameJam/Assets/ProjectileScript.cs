using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileScript : MonoBehaviour
{
    public Vector3 targetDirection;
    public Animator myAnim;
    public bool dissipating;
    public BoxCollider2D boxCollider;
    public AudioSource fireSound;
    public AudioSource impactSound;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        targetDirection -= transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!dissipating){
            if(Vector3.Distance(transform.position,targetDirection) < .1f){
                gameObject.SetActive(false);
            } else{
                transform.right = (targetDirection)*transform.localScale.x;
            transform.Translate((targetDirection).normalized*20f*Time.deltaTime,Space.World);
            }
            
        } else{
            if(!impactSound.isPlaying){
                gameObject.SetActive(false);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag == "Player" && !dissipating){
            other.GetComponent<PlayerScript>().takeDamage();
            boxCollider.enabled = false;
            dissipating = true;
            myAnim.SetTrigger("Dissipate");
            fireSound.Stop();
            impactSound.Play();
        }
    }

    public void Dissipate(){
        GetComponent<SpriteRenderer>().enabled = false;
     //   gameObject.SetActive(false);
    }
}
