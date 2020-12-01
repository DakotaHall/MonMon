using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScriptLevelTwo : MonoBehaviour
{
     public LevelTwoWitchEnemyScript[] levelTwoWitches;
    public DropEnemyScript[] dropEnemies;

    public MoonlightScript moonlight;
    public PlayerScript player;
    public BackgroundManagerScript[] backgroundManagers;
    public float spawnEnemyTime;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh(){
        ProjectileScript[] projectiles = FindObjectsOfType<ProjectileScript>();
        for(int i = 0; i < projectiles.Length;i++){
            Destroy(projectiles[i].gameObject);
        }
        if(levelTwoWitches.Length > 0){
            for(int i = 0; i < levelTwoWitches.Length;i++){
            levelTwoWitches[i].Reset();
        }
        }
        if(dropEnemies.Length > 0){
            for(int i = 0; i < dropEnemies.Length;i++){
            dropEnemies[i].Reset();
        }
        }

        moonlight.moonLight = 100f;
    }
}
