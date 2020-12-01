using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControllerScript : MonoBehaviour
{
    public AudioSource[] BGMs;
    public float waitTime;
    public bool playSecond;
    // Start is called before the first frame update
    void Start()
    {
       BGMs[0].Play();
       waitTime = BGMs[0].clip.length ;
    }

    // Update is called once per frame
    void Update()
    {
        if(!BGMs[0].isPlaying && !BGMs[1].isPlaying){
            BGMs[1].Play();
        }
    }

}
