using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LetterByLetterScript : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public string completeText;
    public float timeBetweenLetters;
    public float waitTime;
    public bool finished;
    public int letterNum;
    void Update()
    {
        
        if(!finished){
            
            if(Time.time > waitTime){
               
                
                waitTime = Time.time + timeBetweenLetters;
                textMeshPro.text = completeText.Substring(0,letterNum);
                letterNum++;
                if(letterNum >= completeText.Length+1){
                    finished = true;
                }
            } 
    }
}
