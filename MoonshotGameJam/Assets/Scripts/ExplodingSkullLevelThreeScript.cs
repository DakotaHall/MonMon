using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class ExplodingSkullLevelThreeScript : MonoBehaviour
{
    public EnemyHealthScript enemyHealth;
    public float attackCooldown;
    public float attackCooldownTime = 3f;
    public Transform target;
    public float moveSpeed;
    public Vector3 wiggleUpPos;
    public Vector3 wiggleDownPos;
    public bool movingUp;
    public bool canAttack;
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
    public float cameraRatio;
    public float camOrthoSize;
    public Camera mainCam;
    public bool chilling;
    public Rigidbody2D myRigidbody;
    public float movementSpeed = 20f;
    public float rotateSpeed = 200f;
    public float chaseTime;
    public AudioSource explodingSound;

    void Awake()
    {
        initialPos = transform.position;

        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        camOrthoSize = Camera.main.orthographicSize;
        cameraRatio = camOrthoSize * Camera.main.aspect;
        wiggleUpPos = transform.position + Vector3.up * .5f;
        wiggleDownPos = transform.position + Vector3.down * .5f;
        rotatePoint = transform.position + Vector3.down;
        chilling = true;
    }

    void Update()
    {
        if (!exploding)
        {

            if (chilling)
            {
                if (target.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                }
                else
                {
                    transform.localScale = Vector3.one*1.5f;
                }
                rotationAngle += 5 * Time.deltaTime;
                Vector3 offset = new Vector3(Mathf.Sin(rotationAngle), Mathf.Cos(rotationAngle), 0) * 1f;
                transform.position = rotatePoint + offset;
                if ((transform.position.x > mainCam.transform.position.x - cameraRatio && transform.position.x < mainCam.transform.position.x + cameraRatio) &&
                (transform.position.y > mainCam.transform.position.y - camOrthoSize && transform.position.y < mainCam.transform.position.y + camOrthoSize))
                {
                    canAttack = true;
                    chilling = false;
                    attackCooldown = Time.time + 3f;
                    chaseTime = attackCooldown + 5f;
                }

            }
            else if (canAttack)
            {
                if (Time.time > attackCooldown)
                {
                    if(Time.time > chaseTime){
                        
                    } else{
                         Vector2 direction = (Vector2)target.position - myRigidbody.position;
                    direction.Normalize();
                    float rotateAmount = Vector3.Cross(direction, transform.right*transform.localScale.x).z;
                    myRigidbody.angularVelocity = -rotateAmount * rotateSpeed;
                    myRigidbody.velocity = transform.right*transform.localScale.x * movementSpeed;
                    }

                }
                else
                {
                    if (target.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                    }
                    else
                    {
                        transform.localScale = Vector3.one*1.5f;
                    }
                    rotationAngle += 5 * Time.deltaTime;
                    Vector3 offset = new Vector3(Mathf.Sin(rotationAngle), Mathf.Cos(rotationAngle), 0) * 1f;
                    transform.position = rotatePoint + offset;
                }
            }


            if (enemyHealth.health <= 0)
            {
                myAnim.SetTrigger("ExplosionTrigger");
                exploding = true;
            }
            else
            {
                if (damaged)
                {
                    if (Time.time > damageTime)
                    {
                        sprite.color = Color.white;
                    }
                }
            }
        }
        else
        {
            if (rising)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale += new Vector3(-1f, 1f, 0) * Time.deltaTime;
                }
                else
                {
                    transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime;
                }
                enemyLight.intensity += 2 * Time.deltaTime;
            }
            if (Time.time > damageTime)
            {
                sprite.color = Color.white;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            enemyHealth.health -= 34f;
        }
        if (other.gameObject.tag != "Enemy")
        {
            sprite.color = new Color(0, 255, 255);
            damaged = true;
            damageTime = Time.time + .1f;
            canAttack = false;
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = 0;
            exploding = true;
            myAnim.SetTrigger("ExplosionTrigger");
        }
    }
    public void Reset()
    {
        if (initialPos != Vector3.zero)
        {
            transform.position = initialPos;
        }
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = 0;
        transform.localEulerAngles = Vector3.zero;
        rotationAngle = 0;
        enemyHealth.health = enemyHealth.originalHealth;
        myAnim.Rebind();
        gameObject.SetActive(true);
        exploding = false;
        explosion.SetActive(false);
        canAttack = false;
        chilling = true;
        enemyLight.intensity = 1;
        transform.localScale = Vector3.one*1.5f;
    }
    public void Explode()
    {
        explosion.SetActive(true);
        explodingSound.Play();
        rising = false;
    }
    public void Donezo()
    {
        gameObject.SetActive(false);
    }
    public void Rising()
    {
        rising = true;
    }

}
