using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEnemySpawner : MonoBehaviour
{
    public GameObject enemyHolder;
    public GameObject explodingEnemyHolder;
    public float spawnEnemyCooldown;
    public float spawnEnemyCooldownTime = 7f;
    public int wave = 0;
    public GameObject[] waves;
    public bool waveCleared = false;
    public bool waiting;
    public PlayerScript player;
    public CheckpointScript checkpoint2;
    public LevelOneBackgroundMountainScrollScript levelOneBackgroundMountainScrollScript;
    public GameObject urchinEnemyHolder;
    public GameObject[] backgroundObjects;
    public MoveBoatScript moveBoat;
    void Start()
    {
        spawnEnemyCooldown = Time.time + spawnEnemyCooldownTime;
    }

    void Update()
    {
        if (!waiting)
        {
            waveCleared = true;
            if(wave > 0){
                 for (int i = 0; i < waves[wave-1].transform.childCount; i++)
            {
                if (waves[wave-1].transform.GetChild(i).gameObject.activeSelf)
                {
                    waveCleared = false;
                }
            }
            }
           


            if (waveCleared )
            {
                if(wave < waves.Length){
                    backgroundObjects[wave-1].SetActive(true);
                if(wave == 4){
                    urchinEnemyHolder.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if(wave == 5){
                    player.currentCheckpoint = checkpoint2;
                    levelOneBackgroundMountainScrollScript.ChangeCheckpointPos();
                } else if(wave == 6){
                    urchinEnemyHolder.transform.GetChild(1).gameObject.SetActive(true);
                    urchinEnemyHolder.transform.GetChild(2).gameObject.SetActive(true);
                } 
               if(spawnEnemyCooldownTime < 15){
                  spawnEnemyCooldownTime += 5f;
               }
                
                
                spawnEnemyCooldown = Time.time + spawnEnemyCooldownTime;
                waiting = true;
                } else{
                    moveBoat.done = true;
                }
                
                
            }
        } else{
                
                if (Time.time > spawnEnemyCooldown)
                {
                    waiting = false;
                    waveCleared = false;
                    waves[wave].SetActive(true);
                    wave++;
                }
        }

    }
}
