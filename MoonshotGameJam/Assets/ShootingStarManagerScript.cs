using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarManagerScript : MonoBehaviour
{
    public GameObject shootingStar;
    public float shootingStarCooldownTime;
    public float shootingStarCooldown;
    void Start()
    {
        shootingStarCooldown = Time.time + Random.Range(25f,30f);
    }

    void Update()
    {
        if(Time.time > shootingStarCooldown){
            shootingStar.SetActive(true);
            shootingStarCooldown = Time.time + Random.Range(25f,30f);
        }
    }
}
