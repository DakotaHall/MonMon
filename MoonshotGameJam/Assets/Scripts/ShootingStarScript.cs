using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarScript : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float arcHeight;
    public Vector3 midPoint;
    public float count = 0;
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;
    void OnEnable()
    {
        spriteRenderer.color = Color.white;
        circleCollider.enabled = true;
        transform.localPosition =  new Vector3(-30f,Random.Range(8f,10f),10);
        startPos = transform.localPosition;
        endPos = transform.localPosition + new Vector3(60,Random.Range(-5f,5f),0);
        arcHeight = Random.Range(5f,7f);
        midPoint =  startPos +(endPos -startPos)/2 +Vector3.up*arcHeight;
    }
    void Update()
    {
        if(spriteRenderer.color.a <= 0 && circleCollider.enabled){
            circleCollider.enabled = false;
        }
        if(Vector3.Distance(transform.localPosition,endPos) < .1f){
            count = 0;
            gameObject.SetActive(false);
            
        } else{
            count += .2f *Time.deltaTime;
            Vector3 m1 = Vector3.Lerp( startPos, midPoint, count );
            Vector3 m2 = Vector3.Lerp( midPoint, endPos, count );
            transform.right = (m2-m1).normalized;
            transform.localPosition = Vector3.Lerp(m1, m2, count);
        }
    }
}
