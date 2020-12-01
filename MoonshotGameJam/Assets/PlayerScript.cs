using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    public GameObject lightMask;
    public bool moonBlocked;
    public Rigidbody2D myRigidbody;
    public Animator myAnim;
    public float health = 100;
    public float knockbackTime;
    public CheckpointScript currentCheckpoint;
    [SerializeField]
    private GameObject monmonEyes;
    [SerializeField]
    private GameObject attackObject;
    [SerializeField]
    private GameObject parentObject;
    [SerializeField]
    private GameObject waterSplash;
    [SerializeField]
    private Light2D monmonEyesLight;
    [SerializeField]
    private MoonlightScript moonlight;
    [SerializeField]
    private Transform groundTransform;
    [SerializeField]
    private Transform frontCheckTransform;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float airTimeCount;
    [SerializeField]
    private float airTime;
    private float damageTime;
    private float previousHealth;
    [SerializeField]
    private float lerpTime;
    private float respawnTime;
    private float dashCooldown;
    private float moveDir;
    [SerializeField]
    private SpriteRenderer sprite;

    private bool isJumping;
    [SerializeField]
    private bool isGrounded;
    private bool damaged;
    private bool attacking;
    [SerializeField]
    private bool canDash;
    private bool dashOnCooldown;
    [SerializeField]
    private bool canDoubleJump;
    private bool doubleJumpOnCooldown;
    [SerializeField]
    private Sprite characterSprite;

    [SerializeField]
    private CapsuleCollider2D bodyCollider;
    [SerializeField]
    private CircleCollider2D groundCollider;
    [SerializeField]
    private CameraFollowScript myCamera;
    [SerializeField]
    private string playerState;

    public FadeScreenScript fadeScreen;
    private float dashLerpTime;
    [SerializeField]
    private AudioSource groundStep1;
    [SerializeField]
    private AudioSource groundStep2;
    [SerializeField]
    private AudioSource woodStep1;
    [SerializeField]
    private AudioSource woodStep2;
     [SerializeField]
    private AudioSource slash;
    [SerializeField]
    private AudioSource dash;
    private string groundTag;
    [SerializeField]
    private AudioSource dashError;
    [SerializeField]
    private AudioSource deathSound;
    [SerializeField]
    private AudioSource splashSound;
    public GameObject controlPanel;
    void Start()
    {
        characterSprite = sprite.sprite;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        previousHealth = health;
        myAnim.SetBool("Grounded", true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            controlPanel.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.C)){
            controlPanel.SetActive(false);
        }
        switch (playerState)
        {
            case "respawning":
                if (Time.time > respawnTime)
                {
                    fadeScreen.fadeIn = true;
                    fadeScreen.fadeOut = false;
                    myAnim.Rebind();
                    sprite.color = Color.white;
                    currentCheckpoint.Refresh();
                    sprite.enabled = true;
                    bodyCollider.enabled = true;
                    groundCollider.enabled = true;
                    health = 100f;
                    playerState = "";
                }
                break;
            case "talking":
            if(lightMask.activeSelf){
                lightMask.SetActive(false);
            }
                break;
            case "dashing":
            RaycastHit2D horizontalRay = Physics2D.Raycast(frontCheckTransform.position, Vector3.right * transform.localScale.x, groundCollider.radius+.5f, groundLayer);
                Vector3 newVelocity = myRigidbody.velocity;
                newVelocity.y = Mathf.Clamp(newVelocity.y, -30, 30);
                myRigidbody.velocity = newVelocity;
                if (horizontalRay)
                {
                    myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
                }
                else
                {
                    if (parentObject != null)
                {
                    myRigidbody.velocity = new Vector3(moveSpeed * 3 * transform.localScale.x, 0, 0);
                }
                else
                {
                    myRigidbody.velocity = new Vector3(moveSpeed * 3 * transform.localScale.x, 0, 0);
                }
                }
                
                break;
            case "knockback":
                if (Time.time > knockbackTime)
                {
                    playerState = "";
                }
                break;

            default:
                monmonEyesLight.pointLightOuterRadius = health / 200;
                if (Input.GetMouseButtonDown(1) && !moonBlocked)
                {
                    moonlight.depleting = true;
                    lightMask.SetActive(true);


                }
                else if (Input.GetMouseButtonUp(1))
                {
                    moonlight.depleting = false;
                    lightMask.SetActive(false);
                }
                isGrounded = Physics2D.OverlapCircle(groundTransform.position, .2f, groundLayer);
                
                
                myAnim.SetBool("Grounded", isGrounded);

                moveDir = Input.GetAxisRaw("Horizontal");
                if (lightMask.activeSelf)
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    if (parentObject != null)
                    {
                        if (lightMask.transform.position.x > mousePosition.x)
                        {
                            transform.localScale = new Vector3(-1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1);
                        }
                    }
                    else
                    {
                        if (lightMask.transform.position.x > mousePosition.x)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                    }
                }
                else
                {
                    if (moveDir > 0)
                    {
                        if (parentObject != null)
                        {
                            transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }

                    }
                    else if (moveDir < 0)
                    {
                        if (parentObject != null)
                        {
                            transform.localScale = new Vector3(-1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }

                    }
                }

                if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
                {
                    isJumping = false;
                }
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    myAnim.SetBool("Running", true);
                }
                else
                {
                    myAnim.SetBool("Running", false);
                }
                if (isGrounded)
                {
                    if(Physics2D.OverlapCircle(groundTransform.position, .2f, groundLayer)){
                        groundTag = Physics2D.OverlapCircle(groundTransform.position, .2f, groundLayer).gameObject.tag;
                    }
                    
                    if (doubleJumpOnCooldown)
                    {
                        doubleJumpOnCooldown = false;
                    }
                    myAnim.SetBool("GoingDown", false);
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                    {
                        myRigidbody.velocity = transform.up * jumpHeight;
                        isJumping = true;
                        airTimeCount = airTime;
                    }
                }
                else
                {

                    if (myRigidbody.velocity.y < 0)
                    {
                        myAnim.SetBool("GoingDown", true);
                    }
                    if (canDoubleJump && !doubleJumpOnCooldown)
                    {
                        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                        {
                            doubleJumpOnCooldown = true;
                            myRigidbody.velocity = transform.up * jumpHeight;
                            isJumping = true;
                            airTimeCount = airTime;
                        }
                    }
                }
                if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isJumping == true)
                {
                    if (airTimeCount > 0)
                    {
                        myRigidbody.velocity = transform.up * jumpHeight * 2;
                        airTimeCount -= Time.deltaTime;
                    }
                    else
                    {
                        isJumping = false;
                    }
                }

                if (Input.GetMouseButtonDown(0) && playerState != "dashing" && !attacking)
                {
                    slash.Play();
                    myAnim.SetTrigger("AttackTrigger");
                    attackObject.SetActive(true);
                    attacking = true;
                    monmonEyes.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.LeftShift) && canDash )
                {
                    if(!attacking && !dashOnCooldown && !moonBlocked){
                        dash.Play();
                    myAnim.SetTrigger("DashTrigger");
                    moonlight.moonLight -= 10;
                    playerState = "dashing";
                    isJumping = false;
                    bodyCollider.enabled = false;
                    dashOnCooldown = true;
                    dashCooldown = Time.time + 3f;
                    dashLerpTime = 0;
                    } else{
                        dashError.Play();
                    }
                    

                }
                if (dashOnCooldown)
                {
                    sprite.color = Color.Lerp(Color.blue, Color.white, dashLerpTime);
                    if (dashLerpTime < 1)
                    {
                        dashLerpTime += Time.deltaTime / 3;
                    }
                    if (Time.time > dashCooldown)
                    {
                        dashOnCooldown = false;
                    }
                }

                break;
        }


        if (damaged)
        {
            if (Time.time > damageTime)
            {
                sprite.color = Color.white;
                damaged = false;
            }
        }
          
        if (health <= 0 && playerState != "respawning")
        {
            fadeScreen.fadeOut = true;
            respawnTime = Time.time + 1f;
            myAnim.SetBool("Dead", true);
            deathSound.Play();
            bodyCollider.enabled = false;
            groundCollider.enabled = false;
            myRigidbody.gravityScale = 0;
            moonlight.depleting = false;
            dashOnCooldown = false;
            lightMask.SetActive(false);
            damaged = false;
            sprite.color = Color.white;
            myRigidbody.velocity = Vector3.zero;
            playerState = "respawning";

        }
    }
    void FixedUpdate()
    {
        switch (playerState)
        {
            case "respawning":

                break;
            case "talking":
                break;
            case "dashing":
                
                if (parentObject != null)
                {
                    myRigidbody.velocity = new Vector3(moveSpeed * 3 * transform.localScale.x, 0, 0);
                }
                else
                {
                    myRigidbody.velocity = new Vector3(moveSpeed * 3 * transform.localScale.x, 0, 0);
                }
                break;
            case "knockback":

                break;

            default:

                RaycastHit2D horizontalRay = Physics2D.Raycast(frontCheckTransform.position, Vector3.right * transform.localScale.x, groundCollider.radius+.5f, groundLayer);
                Vector3 newVelocity = myRigidbody.velocity;
                newVelocity.y = Mathf.Clamp(newVelocity.y, -30, 30);
                myRigidbody.velocity = newVelocity;
                if (horizontalRay)
                {
                    myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
                }
                else
                {
                    myRigidbody.velocity = new Vector2(moveSpeed * moveDir, myRigidbody.velocity.y);
                }
                break;
        }


    }

    public void EndAttack()
    {
        attackObject.SetActive(false);
        attacking = false;
        monmonEyes.SetActive(true);
    }
    public void EndDash()
    {
        playerState = "";
        sprite.color = Color.blue;
        dashCooldown = Time.time + 3f;
        bodyCollider.enabled = true;

    }
    public void DisableSprite()
    {
        sprite.enabled = false;
    }
    public void takeDamage()
    {
        if (playerState != "dashing" && !damaged)
        {
            previousHealth = health;
            health -= 10;
            sprite.color = new Color(255, 0, 0);
            damageTime = Time.time + .5f;
            damaged = true;
            lerpTime = 0;
        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "EnemyAttack" || other.gameObject.name.Contains("LevelTwoWitchProjectile"))
        {


        }
        else if (other.gameObject.tag == "Explosion")
        {
            if (playerState != "dashing")
            {
                previousHealth = health;
                health -= 20;
                sprite.color = new Color(255, 0, 0);
                damageTime = Time.time + .5f;
                damaged = true;
                lerpTime = 0;
            }

        }
        else if (other.gameObject.tag == "Checkpoint")
        {
            if (currentCheckpoint != other.gameObject.GetComponent<CheckpointScript>())
            {
                currentCheckpoint = other.gameObject.GetComponent<CheckpointScript>();
            }

        }
        else if (other.gameObject.tag == "Death")
        {
            health = 0;
            lerpTime = 0;
        }
        else if (other.gameObject.tag == "Water")
        {
            health = 0;
            lerpTime = 0;
             fadeScreen.fadeOut = true;
            respawnTime = Time.time + 1f;
            myAnim.SetBool("Dead", true);
            splashSound.Play();
            bodyCollider.enabled = false;
            groundCollider.enabled = false;
            myRigidbody.gravityScale = 0;
            moonlight.depleting = false;
            dashOnCooldown = false;
            lightMask.SetActive(false);
            damaged = false;
            sprite.color = Color.white;
            myRigidbody.velocity = Vector3.zero;
            playerState = "respawning";
            GameObject newSplash = Instantiate(waterSplash, new Vector3(transform.position.x, -7.9f, 0f), Quaternion.identity);
        }
        else if (other.gameObject.tag == "Fire")
        {
            if (playerState != "dashing")
            {
                previousHealth = health;
                health -= 100;
                sprite.color = new Color(255, 0, 0);
                damageTime = Time.time + .5f;
                damaged = true;
                lerpTime = 0;
            }

        }
    }

    public void ResetState()
    {
        playerState = "";
    }

    public bool GetState(string sentState)
    {
        if (playerState == sentState)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetState(string sentState)
    {
        playerState = sentState;
    }

    public void EnableDash()
    {
        canDash = true;
    }

    public bool CheckGrounded()
    {
        if (isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DoubleJumpCheck()
    {
        if (canDoubleJump)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            parentObject = other.gameObject;
            transform.SetParent(parentObject.transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            parentObject = null;
            transform.SetParent(null);
        }
    }

    public void PlayFootstep(int num){
        if(groundTag == "Ground"){
            if(num == 0){
            groundStep1.Play();
        } else{
            groundStep2.Play();
        }
        } else if(groundTag == "Boat"){
            if(num == 0){
            woodStep1.Play();
        } else{
            woodStep2.Play();
        }
        }
        
    }
}
