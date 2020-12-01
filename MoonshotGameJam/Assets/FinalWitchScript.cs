using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class FinalWitchScript : MonoBehaviour
{
    public Light2D levelLight;
    public bool turnLightRed;
    public SpriteRenderer witchMoonCover;
    public Transform target;
    public GameObject witchFireballs;
    public FireSpinScript fireSpin;
    public BoxCollider2D areaBounds;
    public float movementBoundsLeft;
    public float movementBoundsRight;
    public float movementBoundsUp;
    public float movementBoundsDown;
    public Vector3 movePos;
    public float count;
    public Vector3 rotatePoint;
    public Vector3 startPos;
    public Vector3 midPoint;
    public float flipVal = 1;
    public GameObject[] lightningBolts;
    public float moonAbilityCooldown;
    public float mirrorImageCooldown;
    public float lightningAbilityCooldown;
    public string enemyState = "basic";
    public float defaultCastTime;
    public GameObject projectile;
    public Transform castTransform;
    public GameObject spellcastObject;
    public EnemyHealthScript healthScript;
    public float witchHealth;
    public float witchOriginalHealth;
    public int castNum;
    public FinalWitchScript mirrorImage1;
    public FinalWitchScript mirrorImage2;
    public bool realOne;
    public bool activateLightning;
    public bool activateFire;
    public float fireTime;
    public float lightningTime;
    public float betweenLightningTime;
    public float blockTime;
    public PlayerScript player;
    public bool teleportToCenter;
    public float fireStartTime;
    public List<int> possibleAbilities;
    public Vector3 initialPos;
    public float lerpTime;
    public Animator myAnim;
    public AudioSource castSound;
    public WitchDialogueScript witchDialogue;
    public float cancelDamage;
    void Start()
    {
        myAnim = GetComponent<Animator>();
        initialPos = transform.position;
        witchOriginalHealth = healthScript.originalHealth;
        moonAbilityCooldown = Time.time + 30f;
        movementBoundsLeft = areaBounds.bounds.min.x + 2;
        movementBoundsRight = areaBounds.bounds.max.x - 2;
        movementBoundsDown = areaBounds.bounds.min.y + 2;
        movementBoundsUp = areaBounds.bounds.max.y - 2;

        movePos = transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));

        while (movePos.x < -42 || movePos.x > 42)
        {
            movePos.x = transform.position.x + Random.Range(-5, 5);
        }
        while (movePos.y < -13 || movePos.y > 30)
        {
            movePos.y = transform.position.y + Random.Range(-5, 5);
        }
        rotatePoint = transform.position + (movePos - transform.position) / 2;
        count = 0;
        startPos = transform.position;
        flipVal *= -1;
        midPoint = startPos + (movePos - startPos) / 2 + Vector3.up * Random.Range(1f, 2f) * flipVal;
        defaultCastTime = Time.time + 7f;
    }

    void Update()
    {
        if (healthScript.health <= 0 && enemyState != "dead")
        {
            enemyState = "dead";
            if (realOne)
            {
                witchDialogue.gameObject.SetActive(true);
                witchDialogue.PlayDefeatedText();

            }
            else
            {
                gameObject.SetActive(false);
            }

        }
        switch (enemyState)
        {
            case "dead":
                if (target.transform.position.x > transform.position.x)
                {
                    transform.localScale = Vector3.one;
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                break;
            case "basic":
                if (witchOriginalHealth - healthScript.health >= 50)
                {
                    witchOriginalHealth = healthScript.health;
                    enemyState = "teleporting";
                    spellcastObject.SetActive(true);
                }
                if (realOne)
                {
                    if (player.moonBlocked)
                    {
                        if (Time.time > blockTime)
                        {
                            lerpTime = 0;
                            player.moonBlocked = false;
                        }
                        else
                        {
                            lerpTime += Time.deltaTime / 2;
                            if (levelLight.color != Color.red)
                            {
                                levelLight.color = Color.Lerp(Color.white, Color.red, lerpTime);
                            }
                            if (witchMoonCover.color.a < 1)
                            {
                                Color replacementColor = witchMoonCover.color;
                                replacementColor.a += Time.deltaTime / 2;
                                witchMoonCover.color = replacementColor;
                            }
                        }
                    }
                    else
                    {
                        lerpTime += Time.deltaTime / 2;
                        if (levelLight.color != Color.white)
                        {
                            levelLight.color = Color.Lerp(Color.red, Color.white, lerpTime);
                        }
                        if (witchMoonCover.color.a > 0)
                        {
                            Color replacementColor = witchMoonCover.color;
                            replacementColor.a -= Time.deltaTime / 2;
                            witchMoonCover.color = replacementColor;
                        }
                    }
                }
                count += 1.0f * Time.deltaTime;

                Vector3 m1 = Vector3.Lerp(startPos, midPoint, count);
                Vector3 m2 = Vector3.Lerp(midPoint, movePos, count);
                transform.position = Vector3.Lerp(m1, m2, count);
                if (Vector3.Distance(transform.position, movePos) < .01f)
                {
                    CalculateNewPos();
                }
                if (target.transform.position.x > transform.position.x)
                {
                    transform.localScale = Vector3.one;
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (realOne)
                {
                    if (Time.time > defaultCastTime)
                    {
                        if (realOne)
                        {
                            castNum++;
                        }

                        defaultCastTime = Time.time + 7;
                        if (castNum == 4)
                        {
                            castNum = 0;
                            if (possibleAbilities.Count == 0)
                            {
                                possibleAbilities.Add(0);
                                possibleAbilities.Add(1);
                                possibleAbilities.Add(2);
                                possibleAbilities.Add(3);
                            }
                            int spellNum = possibleAbilities[Random.Range(0, possibleAbilities.Count)];
                            if (spellNum == 0)
                            {
                                possibleAbilities.Remove(0);

                                lerpTime = 0f;
                                blockTime = Time.time + 20f;
                                player.moonBlocked = true;
                                player.lightMask.SetActive(false);

                            }
                            else if (spellNum == 1)
                            {
                                possibleAbilities.Remove(1);
                                activateFire = true;
                                fireTime = Time.time + 12f;
                                cancelDamage = healthScript.health - 100f;
                                enemyState = "casting";
                                teleportToCenter = true;
                                spellcastObject.SetActive(true);
                                fireStartTime = Time.time + 2f;
                            }
                            else if (spellNum == 2)
                            {
                                possibleAbilities.Remove(2);
                                activateLightning = true;
                                cancelDamage = healthScript.health - 100f;
                                lightningTime = Time.time + 10f;
                                betweenLightningTime = Time.time + 3f;
                                enemyState = "casting";
                            }
                            else if (spellNum == 3)
                            {
                                possibleAbilities.Remove(3);
                                mirrorImage1.gameObject.SetActive(true);
                                mirrorImage1.enemyState = "teleporting";
                                mirrorImage1.spellcastObject.SetActive(true);
                                mirrorImage2.gameObject.SetActive(true);
                                mirrorImage2.enemyState = "teleporting";
                                mirrorImage2.spellcastObject.SetActive(true);
                                enemyState = "teleporting";
                                spellcastObject.SetActive(true);

                            }
                        }
                        else
                        {
                            enemyState = "casting";
                            myAnim.SetTrigger("Cast");

                        }
                    }
                }


                break;
            case "casting":
                if (activateLightning)
                {
                    if (Time.time > lightningTime || healthScript.health < cancelDamage)
                    {
                        enemyState = "basic";
                        activateLightning = false;
                        defaultCastTime = Time.time + 7f;
                    }
                    else if (Time.time > betweenLightningTime)
                    {
                        myAnim.SetTrigger("Cast");
                        betweenLightningTime = Time.time + 2f;
                        int randomLightningNum = Random.Range(0, lightningBolts.Length);
                        int randomLightningNum2 = Random.Range(0, lightningBolts.Length);
                        while (randomLightningNum2 == randomLightningNum)
                        {
                            randomLightningNum2 = Random.Range(0, lightningBolts.Length);
                        }
                        int randomLightningNum3 = Random.Range(0, lightningBolts.Length);
                        while (randomLightningNum3 == randomLightningNum || randomLightningNum3 == randomLightningNum2)
                        {
                            randomLightningNum3 = Random.Range(0, lightningBolts.Length);
                        }
                        int randomLightningNum4 = Random.Range(0, lightningBolts.Length);
                        while (randomLightningNum4 == randomLightningNum || randomLightningNum4 == randomLightningNum2 || randomLightningNum4 == randomLightningNum3)
                        {
                            randomLightningNum4 = Random.Range(0, lightningBolts.Length);
                        }
                        int randomLightningNum5 = Random.Range(0, lightningBolts.Length);
                        while (randomLightningNum5 == randomLightningNum || randomLightningNum5 == randomLightningNum2 || randomLightningNum5 == randomLightningNum3 || randomLightningNum5 == randomLightningNum4)
                        {
                            randomLightningNum5 = Random.Range(0, lightningBolts.Length);
                        }

                        lightningBolts[randomLightningNum].SetActive(true);
                        lightningBolts[randomLightningNum2].SetActive(true);
                        lightningBolts[randomLightningNum3].SetActive(true);
                        lightningBolts[randomLightningNum4].SetActive(true);
                        lightningBolts[randomLightningNum5].SetActive(true);
                    }
                }
                else if (activateFire)
                {
                    if (Time.time > fireTime || healthScript.health < cancelDamage)
                    {
                        enemyState = "basic";
                        activateFire = false;

                        witchFireballs.SetActive(false);
                        fireSpin.radius = 5f;
                        for (int i = 0; i < fireSpin.fires.Length; i++)
                        {
                            fireSpin.fires[i].GetComponent<SpriteRenderer>().enabled = true;
                            fireSpin.fires[i].GetComponent<BoxCollider2D>().enabled = true;
                        }
                        defaultCastTime = Time.time + 7f;
                    }
                    else if (Time.time > fireStartTime)
                    {
                        if (!witchFireballs.activeSelf)
                        {
                            witchFireballs.SetActive(true);
                        }

                        fireSpin.radius += 5 * Time.deltaTime;
                    }
                }
                break;
            case "teleporting":

                break;

            default:
                break;
        }



    }

    public void CalculateNewPos()
    {
        movePos = transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));

        while (movePos.x < -42 || movePos.x > 42)
        {
            movePos.x = transform.position.x + Random.Range(-5, 5);
        }
        while (movePos.y < -14 || movePos.y > 30)
        {
            movePos.y = transform.position.y + Random.Range(-5, 5);
        }
        rotatePoint = transform.position + (movePos - transform.position) / 2;
        count = 0;
        startPos = transform.position;
        flipVal *= -1;
        midPoint = startPos + (movePos - startPos) / 2 + Vector3.up * Random.Range(1f, 2f) * flipVal;
    }

    public void Fire()
    {
        castSound.Play();
        if (!activateFire && !activateLightning)
        {
            GameObject newProjectile = Instantiate(projectile, castTransform.position, Quaternion.identity);

            newProjectile.GetComponent<HeatSeekingProjectileScript>().target = target;
            GameObject newProjectile2 = Instantiate(projectile, castTransform.position + Vector3.up, Quaternion.identity);

            GameObject newProjectile3 = Instantiate(projectile, castTransform.position + Vector3.down, Quaternion.identity);

            newProjectile2.GetComponent<HeatSeekingProjectileScript>().target = target;
            newProjectile3.GetComponent<HeatSeekingProjectileScript>().target = target;
            newProjectile2.transform.Rotate(new Vector3(0, 0, 60), Space.Self);
            newProjectile3.transform.Rotate(new Vector3(0, 0, 300), Space.Self);
            if (transform.localScale.x > 0)
            {

            }
            else
            {
                newProjectile.transform.Rotate(0, 0, 180);
                newProjectile2.transform.Rotate(0, 0, 180);
                newProjectile3.transform.Rotate(0, 0, 180);
            }
            enemyState = "basic";
            CalculateNewPos();
        }


    }

    public void Teleport()
    {
        if (teleportToCenter)
        {
            transform.position = new Vector3(.5f, 6, 0);
        }
        else
        {
            transform.position = new Vector3(Random.Range(-42, 42), Random.Range(-14, 30), 0);
        }

    }

    public void Reset()
    {
        transform.position = initialPos;
        enemyState = "basic";
        spellcastObject.SetActive(false);
        teleportToCenter = false;
        activateFire = false;
        activateLightning = false;
        mirrorImage1.gameObject.SetActive(false);
        mirrorImage2.gameObject.SetActive(false);
        levelLight.color = Color.white;
        witchMoonCover.color = Color.white;
        player.moonBlocked = false;
        witchFireballs.SetActive(false);
        healthScript.health = healthScript.originalHealth;
        witchHealth = healthScript.health;
        witchOriginalHealth = healthScript.health;
        castNum = 0;
        CalculateNewPos();
        defaultCastTime = Time.time + 7f;
    }
}
