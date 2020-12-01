using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCheckpointScript : MonoBehaviour
{
    public PlayerScript player;
    public GameObject checkpoint;
    public bool checkpointEnabled = false;
    public LevelOneBackgroundMountainScrollScript levelOneBackgroundMountainScroll;

    void Update()
    {
        if(!checkpointEnabled){
            bool childActive = false;
        for(int i = 0; i < transform.childCount;i++){
            if(transform.GetChild(i).gameObject.activeSelf){
                childActive = true;
            }
        }
        if(childActive == false){
            player.currentCheckpoint.gameObject.SetActive(false);
            player.currentCheckpoint = checkpoint.GetComponent<CheckpointScript>();
            checkpoint.SetActive(true);
            checkpointEnabled = true;
        }
        }
        
    }
}
