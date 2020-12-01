using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EyeBlinkScript : MonoBehaviour
{
    public PostProcessingEffectsScript postProcessingEffectsScript;
    public TextMeshPro textMeshPro;
    public GameObject textHolder;
    public int textNum = 0;
    public LetterByLetterScript letterByLetter;
    public GameObject textBox;
    public NPCScript nPCScript;

    public void FirstBlink(){
        letterByLetter.completeText = "";
        letterByLetter.textMeshPro.text = "";
        postProcessingEffectsScript.firstBlink();
        postProcessingEffectsScript.enableGaussian();
        textHolder.SetActive(false);
    }
    public void SecondBlink(){
        
        if(textNum == 0){
            letterByLetter.finished = false;
            letterByLetter.completeText = "Are you finally awake, little one?";
            letterByLetter.letterNum = 0;
        } else{
            letterByLetter.finished = false;
            letterByLetter.completeText = "You sure sleep a lot for one so young.";
            letterByLetter.letterNum = 0;
        }
        textNum++;
        textHolder.SetActive(true);
        postProcessingEffectsScript.secondBlink();
    }

    public void enableGaussian(){
        letterByLetter.textMeshPro.text = "";
        letterByLetter.completeText = "";
        textHolder.SetActive(false);
        postProcessingEffectsScript.enableGaussian();
    }
    public void hideText(){
        textHolder.SetActive(false);
    }

    public void ActivateTextbox(){
        nPCScript.enabled = true;
        textBox.SetActive(true);
    }
}
