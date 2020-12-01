using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
public class MrBunnyScript : MonoBehaviour
{
   public GameObject pointer;
    public GameObject textBubble;
    public string[] textArray;
    public int textNum = 0;
    public SpriteRenderer faceSprite;
    public Sprite monmonFace;
    public Sprite bunnyFace;
    public bool canInteract;
    public TextMeshPro npcText;
    public int[] whichFaceArray;
    public LetterByLetterScript letterByLetter;
    public PlayerScript player;
    public Light2D oldLadyLight;
    public GameObject helpText;
    public bool helpTextActivated;
    public PointerHoverScript pointerHover;

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && pointer.activeSelf == true && textBubble.activeSelf == false){
            pointerHover.hover = true;
            letterByLetter.letterNum = 0;
            letterByLetter.completeText = textArray[textNum];
            letterByLetter.finished = false;
            textBubble.SetActive(true);
            player.SetState("talking");
             if(whichFaceArray[textNum] == 0){
                    faceSprite.sprite = monmonFace;
                } else{
                    faceSprite.sprite = bunnyFace;
                }

        }
        else if(textBubble.activeSelf && Input.GetKeyDown(KeyCode.E)){
            if(letterByLetter.finished){
                if(textNum < textArray.Length-1){
                    if(textNum ==19 || textNum == 20){
                         textNum = 20;
                            textBubble.SetActive(false);
                            player.ResetState();
                            player.EnableDash();
                            letterByLetter.letterNum = 0;
                            letterByLetter.completeText = textArray[textNum];
                            letterByLetter.finished = false;
                    } else{
                         textNum++;
                       //  helpText.SetActive(true);
                        letterByLetter.letterNum = 0;
                        letterByLetter.completeText = textArray[textNum];
                        letterByLetter.finished = false;
                        if(whichFaceArray[textNum] == 0){
                            faceSprite.sprite = monmonFace;
                        } else{
                            faceSprite.sprite = bunnyFace;
                        }
                    }
                       

            } else{
                textNum =19;
                pointerHover.hover = false;
                pointerHover.transform.position = pointerHover.initialPos;
                if(!helpTextActivated){
                    helpText.SetActive(true);
                }
                 
                 helpTextActivated = true;
                textBubble.SetActive(false);
                player.ResetState();
                player.EnableDash();
                letterByLetter.letterNum = 0;
                            letterByLetter.completeText = textArray[textNum];
                            letterByLetter.finished = false;
                            npcText.text = "";
            }
            } else{
                letterByLetter.letterNum = letterByLetter.completeText.Length-1;

            }
            
            
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            pointer.SetActive(true);
            
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            pointer.SetActive(false);
            textBubble.SetActive(false);
        }
    }
}
