using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class MoonlightSceneColorChangingScript : MonoBehaviour
{
     private Volume volume;
    UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    [SerializeField]
    private MoonlightScript moonlight;
    void Start()
    {
         volume  = gameObject.GetComponent<Volume>();
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;


 
        volumeProfile.TryGet(out colorAdjustments);
 
    }

    void Update()
    {
            colorAdjustments.postExposure.value = -2 + moonlight.moonLight/50f;
            colorAdjustments.colorFilter.value = new Color(moonlight.moonLight/100,moonlight.moonLight/100,1*Mathf.Pow(2,.5f-(moonlight.moonLight/200)),1);
            colorAdjustments.hueShift.value = -25 + moonlight.moonLight/4;
            colorAdjustments.saturation.value = -50 + moonlight.moonLight/2;
    }
}
