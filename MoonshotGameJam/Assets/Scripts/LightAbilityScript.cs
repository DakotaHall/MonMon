using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightAbilityScript : MonoBehaviour
{
    public Vector3 lookatVector;
    public Light2D maskLight;
    public MoonlightScript moonlight;
    public LayerMask enemyMask;
    public SpriteRenderer playerSprite;
    public float lightValue = -90;
    public Vector3 previousMousePos;
    public List<GameObject> hitEnemies;
    public PlayerScript player;
    public ParticleSystem blueDotsSystem;
    public Camera mainCam;
    void OnEnable()
    {
        previousMousePos = Input.mousePosition;
    }
    void OnDisable()
    {
 
    }
    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        maskLight.pointLightOuterRadius = moonlight.moonLight / 2;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.up = (mousePosition - transform.position).normalized;
        Vector3 upPos = new Vector3((transform.up.x) * Mathf.Cos(Mathf.PI / 180 * 5f) - (transform.up.y) * Mathf.Sin(Mathf.PI / 180 * 5f), (transform.up.y) * Mathf.Cos(Mathf.PI / 180 * 5f) + (transform.up.x) * Mathf.Sin(Mathf.PI / 180 * 5f), 0);
        Vector3 downPos = new Vector3((transform.up.x) * Mathf.Cos(Mathf.PI / 180 * 355f) - (transform.up.y) * Mathf.Sin(Mathf.PI / 180 * 355f), (transform.up.y) * Mathf.Cos(Mathf.PI / 180 * 355f) + (transform.up.x) * Mathf.Sin(Mathf.PI / 180 * 355f), 0);

        upPos += transform.up;
        downPos += transform.up;
        RaycastHit2D[] ray1 = Physics2D.RaycastAll(transform.position, transform.up, moonlight.moonLight / 2, enemyMask);
        RaycastHit2D[] ray2 = Physics2D.RaycastAll(transform.position, upPos, moonlight.moonLight / 2, enemyMask);
        RaycastHit2D[] ray3 = Physics2D.RaycastAll(transform.position, downPos, moonlight.moonLight / 2, enemyMask);
        List<GameObject> ray1Objects = new List<GameObject>();
        List<GameObject> ray2Objects = new List<GameObject>();
        List<GameObject> ray3Objects = new List<GameObject>();
        List<float> rayDistances = new List<float>();
        if (ray1.Length > 0)
        {
            for (int i = 0; i < ray1.Length; i++)
            {
                if (ray1[i].collider.gameObject.tag == "Boat")
                {
                    rayDistances.Add(ray1[i].distance);
                    break;
                }
                ray1Objects.Add(ray1[i].collider.gameObject);
                
                if (ray1[i].collider.gameObject.GetComponent<EnemyHealthScript>() != null)
                {
                    Vector3 enemyPoint = mainCam.WorldToViewportPoint(ray1[i].collider.gameObject.transform.position);
                    if (enemyPoint.x > 0 && enemyPoint.x < 1 && enemyPoint.y > 0 && enemyPoint.y < 1)
                    {
                        ray1[i].collider.gameObject.GetComponent<EnemyHealthScript>().health -= 25 * Time.deltaTime;
                        ray1[i].collider.gameObject.GetComponent<EnemyHealthScript>().Damage();
                    }

                }
                else if (ray1[i].collider.gameObject.tag == "ShootingStar")
                {
                    ray1[i].collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,ray1[i].collider.gameObject.GetComponent<SpriteRenderer>().color.a - .30f*Time.deltaTime);
                    if (player.health > 100)
                    {
                        player.health = 100;
                    }
                    else
                    {
                        if (player.health < 100)
                        {
                            player.health += 5 * Time.deltaTime;
                        }
                    }

                }
            }
        }
        if (ray2.Length > 0)
        {

            for (int i = 0; i < ray2.Length; i++)
            {
                if (ray2[i].collider.gameObject.tag == "Boat")
                {
                    rayDistances.Add(ray2[i].distance);
                    break;
                }
                if (!ray1Objects.Contains(ray2[i].collider.gameObject))
                {
                    ray2Objects.Add(ray2[i].collider.gameObject);
                    
                    if (ray2[i].collider.gameObject.GetComponent<EnemyHealthScript>() != null)
                    {
                        Vector3 enemyPoint = mainCam.WorldToViewportPoint(ray2[i].collider.gameObject.transform.position);
                        if (enemyPoint.x > 0 && enemyPoint.x < 1 && enemyPoint.y > 0 && enemyPoint.y < 1)
                        {
                            ray2[i].collider.gameObject.GetComponent<EnemyHealthScript>().health -= 30 * Time.deltaTime;
                            ray2[i].collider.gameObject.GetComponent<EnemyHealthScript>().Damage();
                        }

                    }
                    else if (ray2[i].collider.gameObject.tag == "ShootingStar")
                    {
                        ray2[i].collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,ray2[i].collider.gameObject.GetComponent<SpriteRenderer>().color.a - .30f*Time.deltaTime);
                        if (player.health > 100)
                        {
                            player.health = 100;
                        }
                        else
                        {
                            if (player.health < 100)
                            {
                                player.health += 5 * Time.deltaTime;
                            }
                        }

                    }
                }

            }
        }
        if (ray3.Length > 0)
        {
            for (int i = 0; i < ray3.Length; i++)
            {
                if (ray3[i].collider.gameObject.tag == "Boat")
                {
                    rayDistances.Add(ray3[i].distance);
                    break;
                }
                if (!ray1Objects.Contains(ray3[i].collider.gameObject) && !ray2Objects.Contains(ray3[i].collider.gameObject))
                {
                    ray3Objects.Add(ray3[i].collider.gameObject);
                    
                    if (ray3[i].collider.gameObject.GetComponent<EnemyHealthScript>() != null)
                    {
                        Vector3 enemyPoint = mainCam.WorldToViewportPoint(ray3[i].collider.gameObject.transform.position);
                        if (enemyPoint.x > 0 && enemyPoint.x < 1 && enemyPoint.y > 0 && enemyPoint.y < 1)
                        {
                            ray3[i].collider.gameObject.GetComponent<EnemyHealthScript>().health -= 25 * Time.deltaTime;
                            ray3[i].collider.gameObject.GetComponent<EnemyHealthScript>().Damage();
                        }

                    }
                    else if (ray3[i].collider.gameObject.tag == "ShootingStar")
                    {
                        ray3[i].collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,ray3[i].collider.gameObject.GetComponent<SpriteRenderer>().color.a - .25f*Time.deltaTime);
                        if (player.health > 100)
                        {
                            player.health = 100;
                        }
                        else
                        {
                            if (player.health < 100)
                            {
                                player.health += 5 * Time.deltaTime;
                            }
                        }

                    }
                }

            }
        }

        float raydistance = 50f;
        for(int i = 0; i < rayDistances.Count;i++){
            if(rayDistances[i] < raydistance){
                raydistance = rayDistances[i];
            }
        }
        
        if (moonlight.moonLight / 2 < raydistance)
        {
            raydistance = moonlight.moonLight / 2;
        }
        var particleSystemMain = blueDotsSystem.main;
        var particleShape = blueDotsSystem.shape;
        particleSystemMain.maxParticles = Mathf.RoundToInt(raydistance / 2.5f);
        particleShape.length = raydistance;

    }


}
