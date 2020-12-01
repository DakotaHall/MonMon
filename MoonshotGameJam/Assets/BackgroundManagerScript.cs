using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManagerScript : MonoBehaviour
{
    public float[] spawnTimes;
    public float spawnInterval;
    public float spawnTime;
    public GameObject[] spawnItems;
    public int spawnedIndex;
    void Start()
    {
        spawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if(Time.time > spawnTime && spawnedIndex < transform.childCount){
            transform.GetChild(spawnedIndex).gameObject.SetActive(true);
            spawnedIndex++;
            spawnTime = Time.time + spawnInterval;
        }
    }

    public void Reset(){
        spawnTime = Time.time + spawnInterval;
        
    }
}
