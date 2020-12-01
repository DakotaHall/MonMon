using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LevelTwoWitchEnemyScript : MonoBehaviour
{
    //public float enemyHealth = 100;
    public float attackCooldown;
    public float attackCooldownTime = 3f;
    public bool movingOntoScreen = true;
    public Vector3 goToPosition;
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
    public float originalMovementBoundsLeft;
    public float movementBoundsRight;
    public float originalMovementBoundsRight;
    public float movementBoundsUp;
    public float movementBoundsDown;
    public Transform target;
    public bool JUSTWAIT;
    public EnemyHealthScript enemyHealth;
    public SpriteMask damageMaskSprite;
    public GameObject damageMask;
    public float damageTime;
    public bool damaged;
    public Rigidbody2D myRigidbody;
    public string enemyState = "chilling";
    public GameObject rotatingFireball1;
    public GameObject rotatingFireball2;
    public float cameraRatio;
    public float camOrthoSize;
    public Camera mainCam;
    public AudioSource castSound;
    public AudioSource deathSound;
    
    // Start is called before the first frame update
    void Awake()
    {
        initialPos = transform.position;
    }
    void Start()
    {
        mainCam = Camera.main;
        originalMovementBoundsLeft = movementBoundsLeft;
        originalMovementBoundsRight = movementBoundsRight;
        camOrthoSize = Camera.main.orthographicSize;
        cameraRatio = camOrthoSize*Camera.main.aspect;
        
        
    }

    void Update()
    {
        switch (enemyState)
        {
            case "chilling":
             movementBoundsLeft = mainCam.transform.position.x - cameraRatio + 1;
             movementBoundsRight = mainCam.transform.position.x + cameraRatio - 1;
        movementBoundsUp = mainCam.transform.position.y + camOrthoSize - 1;
        movementBoundsDown = mainCam.transform.position.y - camOrthoSize + 1;
                if(transform.position.x < movementBoundsRight && transform.position.x > movementBoundsLeft && transform.position.y < movementBoundsUp && transform.position.y > movementBoundsDown)
                {
                    myAnim.SetBool("Aggro", true);
                    enemyState = "fighting";
                    canAttack = true;
                    attackCooldown = Time.time + attackCooldownTime;
                   CalculateNewPos();
                }
                else
                {
                    if (!JUSTWAIT)
                    {
                        transform.Translate((goToPosition - transform.position).normalized * moveSpeed * Time.deltaTime);
                    }
                }
                break;

            case "fighting":
                if (canAttack)
                {
                    if (Time.time > attackCooldown)
                    {
                        enemyState = "casting";
                        myAnim.SetBool("Casting", true);
                    }
                }
                if (!damaged)
                {
                    count += 1.0f * Time.deltaTime;
                    Vector3 m1 = Vector3.Lerp(startPos, midPoint, count);
                    Vector3 m2 = Vector3.Lerp(midPoint, wiggleUpPos, count);
                    transform.position = Vector3.Lerp(m1, m2, count);
                    if (Vector3.Distance(transform.position, wiggleUpPos) < .01f)
                    {
                        CalculateNewPos();
                    }
                }
                break;

            case "casting":
                break;

            case "dying":
               myRigidbody.velocity = Vector3.zero;
                break;
        }
        if (enemyState != "dying")
        {
            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1.25f, 1.25f, 1);
            }
            else
            {
                transform.localScale = Vector3.one*1.25f;
            }
            if (damaged)
            {
                if (Time.time > damageTime)
                {
                    myRigidbody.velocity = Vector3.zero;
                    CalculateNewPos();
                    damaged = false;
                }
            }

            if (enemyHealth.health <= 0 && enemyState != "dying")
            {
                enemyState = "dying";
                damaged = false;
                 damageMask.SetActive(false);
                damageMaskSprite.enabled = false;
                deathSound.Play();
                myAnim.SetTrigger("Die");
            }
        }

    }

    public void Reset()
    {
        transform.position = initialPos;
        enemyHealth.health = enemyHealth.originalHealth;
        gameObject.SetActive(true);
        myAnim.Rebind();
        rotatingFireball1.SetActive(true);
        
        rotatingFireball1.GetComponent<Animator>().Rebind();
        rotatingFireball2.SetActive(true);
        rotatingFireball2.GetComponent<Animator>().Rebind();
        enemyState = "chilling";
        canAttack = false;
    }

    private void CalculateNewPos()
    {
        wiggleUpPos = transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
        movementBoundsLeft = mainCam.transform.position.x - cameraRatio + 1;
        movementBoundsRight = mainCam.transform.position.x + cameraRatio - 1;
        movementBoundsUp = mainCam.transform.position.y + camOrthoSize - 1;
        movementBoundsDown = mainCam.transform.position.y - camOrthoSize + 1;
        if(transform.position.x > movementBoundsRight || transform.position.x < movementBoundsLeft || transform.position.y > movementBoundsUp || transform.position.y < movementBoundsDown){
             myAnim.SetBool("Aggro", false);
                    enemyState = "chilling";
                    canAttack = false;
                    return;
        }
        while (wiggleUpPos.x < movementBoundsLeft || wiggleUpPos.x > movementBoundsRight)
        {
            wiggleUpPos.x = transform.position.x + Random.Range(-3, 3);
        }
        while (wiggleUpPos.y < movementBoundsDown || wiggleUpPos.y > movementBoundsUp)
        {
            wiggleUpPos.y = transform.position.y + Random.Range(-3, 3);
        }
        rotatePoint = transform.position + (wiggleUpPos - transform.position) / 2;
        count = 0;
        startPos = transform.position;
        flipVal *= -1;
        midPoint = startPos + (wiggleUpPos - startPos) / 2 + Vector3.up * Random.Range(1f, 2f) * flipVal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack" && !damaged)
        {
            damaged = true;
            if(enemyHealth.health > 0){
                damageTime = Time.time + .2f;
            } else{
                damageTime = Time.time + .1f;
            }
            
            myRigidbody.AddForce((other.transform.position - transform.position) * -20f);
        }
    }

    public void Damage(){

    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
    public void Fire()
    {
        castSound.Play();
        attackCooldown = Time.time + attackCooldownTime;
        GameObject newProjectile = Instantiate(projectile, castTransform.position, Quaternion.identity);
        newProjectile.transform.localScale = transform.localScale;
        Vector3 projectileDirection = target.transform.position - (castTransform.position - target.transform.position);
        newProjectile.GetComponent<ProjectileScript>().targetDirection = projectileDirection;
        if (tripleShot)
        {
            GameObject secondProjectile = Instantiate(projectile, castTransform.position, Quaternion.identity);
            GameObject thirdProjectile = Instantiate(projectile, castTransform.position, Quaternion.identity);
            secondProjectile.transform.localScale = transform.localScale;
            thirdProjectile.transform.localScale = transform.localScale;
            Vector3 upPos = new Vector3((projectileDirection.x - castTransform.position.x) * Mathf.Cos(Mathf.PI / 180 * 10f) - (projectileDirection.y - castTransform.position.y) * Mathf.Sin(Mathf.PI / 180 * 10f), (projectileDirection.y - castTransform.position.y) * Mathf.Cos(Mathf.PI / 180 * 10f) + (projectileDirection.x - castTransform.position.x) * Mathf.Sin(Mathf.PI / 180 * 10f), 0);
            Vector3 downPos = new Vector3((projectileDirection.x - castTransform.position.x) * Mathf.Cos(Mathf.PI / 180 * 350f) - (projectileDirection.y - castTransform.position.y) * Mathf.Sin(Mathf.PI / 180 * 350f), (projectileDirection.y - castTransform.position.y) * Mathf.Cos(Mathf.PI / 180 * 350f) + (projectileDirection.x - castTransform.position.x) * Mathf.Sin(Mathf.PI / 180 * 350f), 0);

            upPos += castTransform.position;
            downPos += castTransform.position;
            secondProjectile.GetComponent<ProjectileScript>().targetDirection = upPos;
            thirdProjectile.GetComponent<ProjectileScript>().targetDirection = downPos;


        }
        enemyState = "fighting";
        myAnim.SetBool("Casting", false);
        CalculateNewPos();
    }
}
