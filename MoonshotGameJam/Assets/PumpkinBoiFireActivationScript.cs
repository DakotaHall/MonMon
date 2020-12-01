using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBoiFireActivationScript : MonoBehaviour
{
    public GameObject nextFire;
    public BoxCollider2D boxCollider;
    public LayerMask groundMask;
    public Transform baseTransform;
    public Vector3 originalScale;
    public Vector3 originalLossyScale;
    public AudioSource fireSound;
    

    void OnDisable(){
        transform.localScale = originalScale;
    }

    void OnEnable(){
        boxCollider.enabled = true;
        if(originalScale == Vector3.zero){
            originalScale = transform.localScale;
            originalLossyScale = transform.lossyScale;
        }
        
         RaycastHit2D fireRay = Physics2D.Raycast(baseTransform.position,(transform.parent.localScale.x/Mathf.Abs(transform.parent.localScale.x))*baseTransform.right*(originalScale.x/Mathf.Abs(originalScale.x)),boxCollider.size.x*Mathf.Abs(originalLossyScale.y),groundMask);
             if(fireRay.collider != null){
                 float vectorDifMag = ((Vector3)fireRay.point - baseTransform.position).magnitude;
                 float scalar = boxCollider.size.x * Mathf.Abs(originalLossyScale.y) - vectorDifMag;
                 if(scalar < 1){
                     scalar = 1f;
                 }
                 transform.localScale = new Vector3(originalScale.x/scalar,originalScale.y/scalar,1);
             } else{
                 transform.localScale = originalScale;
             }
    }

    void FixedUpdate()
    {
        if(baseTransform != null){
            Debug.DrawRay(baseTransform.position,(transform.parent.localScale.x/Mathf.Abs(transform.parent.localScale.x))*baseTransform.right*(originalScale.x/Mathf.Abs(originalScale.x))*boxCollider.size.x*Mathf.Abs(originalLossyScale.y),Color.red);
             RaycastHit2D fireRay = Physics2D.Raycast(baseTransform.position,(transform.parent.localScale.x/Mathf.Abs(transform.parent.localScale.x))*baseTransform.right*(originalScale.x/Mathf.Abs(originalScale.x)),boxCollider.size.x*Mathf.Abs(originalLossyScale.y),groundMask);
             if(fireRay.collider != null){
                 float vectorDifMag = ((Vector3)fireRay.point - baseTransform.position).magnitude;
                 float scalar = boxCollider.size.x * Mathf.Abs(originalLossyScale.y) - vectorDifMag;
                 if(scalar < 1){
                     scalar = 1f;
                 }
                 transform.localScale = new Vector3(originalScale.x/scalar,originalScale.y/scalar,1);
             } else{
                 transform.localScale = originalScale;
             }
        }
        
    }

    public void ActivateFire(){
        nextFire.SetActive(true);
    }

    public void DeActivateFire(){
        gameObject.SetActive(false);
    }
    public void DisableCollider(){
        boxCollider.enabled = false;
    }
    // public void PlayFireSound(){
    //     fireSound.Play();
    // }
}
