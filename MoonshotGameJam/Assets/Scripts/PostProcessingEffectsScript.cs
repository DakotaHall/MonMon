using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class PostProcessingEffectsScript : MonoBehaviour
{
    public Volume volume;
    UnityEngine.Rendering.Universal.DepthOfField depthOfField;
    void Start()
    {
        volume  = gameObject.GetComponent<Volume>();
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;


 
        volumeProfile.TryGet(out depthOfField);
 
    }

    public void firstBlink(){
        depthOfField.gaussianMaxRadius.value = 0;
    }

    public void secondBlink(){
        depthOfField.active = false;
    }

    public void enableGaussian(){
        depthOfField.active = true;
    }
}
