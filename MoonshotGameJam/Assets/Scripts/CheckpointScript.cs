using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public EnemyScript[] enemies;
    public ExplodingEnemyScript[] explodingEnemies;
    public UrchinBoiScript[] urchinBois;
    public GameObject[] enemyWaves;
    public MoveBoatScript boatScript;
    public MoonlightScript moonlight;
    public BoatEnemySpawner boatEnemySpawner;
    public int boatEnemyCurrentWave;
    public PlayerScript player;
    public LevelOneBackgroundMountainScrollScript level1BackgroundMountains;
    public BackgroundManagerScript[] backgroundManagers;
    public float spawnEnemyTime;
    public int level;
    public LevelTwoWitchEnemyScript[] levelTwoWitches;
    public DropEnemyScript[] dropEnemies;
    public FinalWitchScript finalWitch;
    public ExplodingSkullLevelThreeScript[] explodingSkullsLevelThree;
    public bool doneTalking;
    public MoveBackgroundObjectScript[] backgroundObjects;
    public GameObject[] backgroundSets;
    public CloudSpawnerScript cloudSpawner;

    public void Refresh()
    {
        cloudSpawner = FindObjectOfType<CloudSpawnerScript>();
        ProjectileScript[] projectiles = FindObjectsOfType<ProjectileScript>();
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (projectiles[i].tag != "Level2Projectile")
            {
                Destroy(projectiles[i].gameObject);
            }

        }
        cloudSpawner.Reset();



        if (level == 0)
        {
            if (enemies.Length > 0)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].Reset();
                }
            }
            if (explodingEnemies.Length > 0)
            {
                for (int i = 0; i < explodingEnemies.Length; i++)
                {
                    explodingEnemies[i].Reset();
                }
            }
            if (urchinBois.Length > 0)
            {
                for (int i = 0; i < urchinBois.Length; i++)
                {
                    urchinBois[i].Reset();
                }
            }
            if (enemyWaves.Length > 0)
            {
                for (int i = 0; i < enemyWaves.Length; i++)
                {
                    enemyWaves[i].SetActive(false);
                }
            }
            if(backgroundObjects.Length > 0){
                for(int i = 0; i < backgroundObjects.Length;i++){
                    backgroundObjects[i].Reset();
                }
            }
            if (backgroundSets.Length > 0)
            {
                for (int i = 0; i < backgroundSets.Length; i++)
                {
                    backgroundSets[i].SetActive(false);
                }
            }
            boatScript.gameObject.SetActive(true);
            boatScript.transform.position = new Vector3(transform.position.x, boatScript.transform.position.y, 0);
            boatScript.boatHealth = 100f;
            boatScript.top.sprite = boatScript.healthyTop;
            boatScript.bottom.sprite = boatScript.healthyBottom;
            boatScript.middle.sprite = boatScript.healthyMid;
            boatEnemySpawner.wave = boatEnemyCurrentWave;
            boatEnemySpawner.spawnEnemyCooldownTime = spawnEnemyTime;
            boatEnemySpawner.spawnEnemyCooldown = Time.time + boatEnemySpawner.spawnEnemyCooldownTime;
            boatEnemySpawner.waveCleared = true;
            boatEnemySpawner.waiting = true;
            player.transform.position = boatScript.transform.position + Vector3.up;
            level1BackgroundMountains.transform.position = level1BackgroundMountains.checkpointPos;
        }
        else if (level == 1)
        {
            player.transform.position = transform.position;

            if (levelTwoWitches.Length > 0)
            {
                for (int i = 0; i < levelTwoWitches.Length; i++)
                {
                    levelTwoWitches[i].Reset();
                }
            }
            if (dropEnemies.Length > 0)
            {
                for (int i = 0; i < dropEnemies.Length; i++)
                {
                    dropEnemies[i].Reset();
                }
            }

        }
        else if (level == 2)
        {
            player.transform.position = transform.position;
            if (explodingSkullsLevelThree.Length > 0)
            {
                for (int i = 0; i < explodingSkullsLevelThree.Length; i++)
                {
                    explodingSkullsLevelThree[i].Reset();
                }
            }
            if (levelTwoWitches.Length > 0)
            {
                for (int i = 0; i < levelTwoWitches.Length; i++)
                {
                    levelTwoWitches[i].Reset();
                }
            }
            if (dropEnemies.Length > 0)
            {
                for (int i = 0; i < dropEnemies.Length; i++)
                {
                    dropEnemies[i].Reset();
                }
            }
        }
        else if (level == 3)
        {
            if (doneTalking)
            {
                finalWitch.Reset();
                HeatSeekingProjectileScript[] heatSeekingProjectiles = FindObjectsOfType<HeatSeekingProjectileScript>();
                for (int i = 0; i < heatSeekingProjectiles.Length; i++)
                {
                    Destroy(heatSeekingProjectiles[i].gameObject);
                }
            }
            player.transform.position = transform.position;

        }

        player.myRigidbody.gravityScale = 5;
        player.ResetState();

        moonlight.moonLight = 100f;
    }

}
