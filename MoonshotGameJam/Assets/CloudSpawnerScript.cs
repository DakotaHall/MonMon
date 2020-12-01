using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour
{
    public GameObject[] clouds;
    public float spawnInterval;
    public float spawnTime;
    public bool[] activeStates;
    public Vector3[] cloudPositions;

    void Start(){
        activeStates = new bool[transform.childCount];
         cloudPositions = new Vector3[transform.childCount];
        for(int i = 0; i < transform.childCount;i++){
            activeStates[i] = transform.GetChild(i).gameObject.activeSelf;
            cloudPositions[i] = transform.GetChild(i).position;
        }
    }

    void Update()
    {
        if(Time.time > spawnTime){
            spawnTime = Time.time + spawnInterval;
            int numClouds = Random.Range(1,3);
            Vector3 spawnedPos = Vector3.zero;
            
            for(int i = 0; i < numClouds;i++){
                transform.GetChild(0).gameObject.SetActive(true);
                if(spawnedPos == Vector3.zero){
                    transform.GetChild(0).position = transform.position + new Vector3(0,Random.Range(-1f,1f),0);
                } else{
                    if(spawnedPos.y < 1){
                         transform.GetChild(0).position = transform.position + new Vector3(Random.Range(-3f,3f),spawnedPos.y + Random.Range(2.5f,3f),0);
                    } else{
                        transform.GetChild(0).position = transform.position + new Vector3(Random.Range(-3f,3f),spawnedPos.y - Random.Range(2f,3f),0);
                    }
                   
                }
                
                
                spawnedPos = transform.GetChild(0).localPosition;
                transform.GetChild(0).SetAsLastSibling();
                
            }
        }
    }

    public void Reset(){
    for(int i = 0; i < transform.childCount;i++){
             transform.GetChild(i).gameObject.SetActive(activeStates[i] );
             transform.GetChild(i).position = cloudPositions[i];
        }
    }
}
