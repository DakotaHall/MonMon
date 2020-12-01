using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossFireProjectileScript : MonoBehaviour
{
    public float rotationAngle;
    public Vector3 rotatePoint;
    public FireSpinScript fireSpin;
    public float rotateSpeed;
    public Animator myAnim;
    public bool dissipating;
    public BoxCollider2D boxCollider;
    public AudioSource dissipateSound;
    
    void OnEnable(){
        boxCollider.enabled = true;
    }
    void Update()
    {
         
        rotationAngle += rotateSpeed * Time.deltaTime;
        rotateSpeed = Mathf.Clamp(5/fireSpin.radius,.8f,1);
        
                    Vector3 offset = new Vector3(Mathf.Sin(rotationAngle),Mathf.Cos(rotationAngle),0)*fireSpin.radius;
                    transform.localPosition =   offset;
                    
                   
                   
                    if(rotationAngle >= 6){
                         int roundFactor = Mathf.RoundToInt(rotationAngle/2*Mathf.PI);
                        transform.localEulerAngles = new Vector3(0,0,-rotationAngle*360/(2*Mathf.PI));
                    } else{
                         transform.localEulerAngles = new Vector3(0,0,-rotationAngle*360/(2*Mathf.PI));
                    }
    }
     void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag == "Player" && !dissipating){
            dissipateSound.Play();
            other.GetComponent<PlayerScript>().takeDamage();
            boxCollider.enabled = false;
            dissipating = true;
            myAnim.SetTrigger("Dissipate");
        }
    }

    public void Dissipate(){
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
