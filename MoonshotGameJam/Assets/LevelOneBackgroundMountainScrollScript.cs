using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBackgroundMountainScrollScript : MonoBehaviour
{
    public BoatEnemySpawner boatEnemySpawner;
    public float moveSpeed;
    public Vector3 checkpointPos;
    public MoveBoatScript boatScript;

    void Start()
    {
        checkpointPos = transform.position;
    }
    void Update()
    {
        if(boatEnemySpawner.waveCleared){
            transform.Translate(Vector3.left*Time.deltaTime*moveSpeed);
            
         }
        if(transform.position.x < -26.5){
             boatScript.done = true;
             gameObject.SetActive(false);
        }
    }

    public void ChangeCheckpointPos(){
        checkpointPos = transform.position;
    }
}
