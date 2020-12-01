using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManagerScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource mouseUpAudio;
    public bool intro;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("MusicVolume") != 0){
            audioMixer.SetFloat("Music",Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume"))*20);
        }
        if(PlayerPrefs.GetFloat("MasterVolume") != 0){
            audioMixer.SetFloat("Master",Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume"))*20);
        }
        if(PlayerPrefs.GetFloat("SFXVolume") != 0){
            audioMixer.SetFloat("SFX",Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume"))*20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(intro && (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))){
            
            mouseUpAudio.Play();
        }
    }


    public void SetMusicVolume(Slider volume){
        audioMixer.SetFloat("Music",Mathf.Log10(volume.value)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume.value);
    }
    public void SetMasterVolume(Slider volume){
        audioMixer.SetFloat("Master",Mathf.Log10(volume.value)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume.value);
    }
    public void SetSFXVolume(Slider volume){
        audioMixer.SetFloat("SFX",Mathf.Log10(volume.value)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume.value);
    }
 
}
