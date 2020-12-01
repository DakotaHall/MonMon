using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCloudScript : MonoBehaviour
{
    public GameObject lightning;
    

    public void ActivateLightning(){
        lightning.SetActive(true);
    }

    public void DeactivateCloud(){
        gameObject.SetActive(false);
    }
}
