using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
public class NPCScript : MonoBehaviour
{
    public GameObject pointer;
    public GameObject textBubble;
    public string[] textArray;
    public int textNum = 0;
    public SpriteRenderer faceSprite;
    public Sprite monmonFace;
    public Sprite oldLadyFace;
    public bool canInteract;
    public TextMeshPro npcText;
    public int[] whichFaceArray;
    public LetterByLetterScript letterByLetter;
    public PlayerScript player;
    public string[] yesText;
    public string[] noText;
    public GameObject oldLadyLight;
    public bool glowing;
    public float glowTime;
    public GameObject buttonCanvas;
    public Button yesButton;
    public GameObject helpText;
    public TextMeshPro helpTextText;
    public bool helpTextActivated;
    public Transform monmonMaskPos;
    public GameObject monmonLight;
    public GameObject witchEnemy;
    public GameObject explodingEnemy;
    public BoxCollider2D boatCollider;
    public PointerHoverScript pointerHover;
    void Start()
    {
        letterByLetter.letterNum = 0;
        letterByLetter.completeText = textArray[textNum];
        letterByLetter.finished = false;
        textBubble.SetActive(true);
        if (whichFaceArray[textNum] == 0)
        {
            faceSprite.sprite = monmonFace;
        }
        else
        {
            faceSprite.sprite = oldLadyFace;
        }
    }

    void OnEnable()
    {

    }
    void Update()
    {
        if (glowing)
        {
            oldLadyLight.transform.position = Vector3.MoveTowards(oldLadyLight.transform.position, monmonMaskPos.position, 5 * Time.deltaTime);
            if (oldLadyLight.transform.position == monmonMaskPos.position)
            {
                glowing = false;
                oldLadyLight.SetActive(false);
                monmonLight.SetActive(true);
            }
        }
        if (textNum == 18 && !witchEnemy.activeSelf )
            {
                   if(!pointer.activeSelf){
                      pointer.SetActive(true);
                        }       
                    if(!pointerHover.hover){
                        pointerHover.hover = true;
                    }
                
                
            } 
            if (textNum == 25 && !explodingEnemy.activeSelf )
            {
                   if(!pointer.activeSelf){
                      pointer.SetActive(true);
                        }       
                    if(!pointerHover.hover){
                        pointerHover.hover = true;
                    }
                
                
            } 
        if (Input.GetKeyDown(KeyCode.E) && pointer.activeSelf == true && textBubble.activeSelf == false)
        {
            letterByLetter.letterNum = 0;

            letterByLetter.completeText = textArray[textNum];
            if (textNum == 18)
            {
                if(witchEnemy.activeSelf){
                    letterByLetter.completeText = "What are you waiting for, kiddo? Time to practice!";
                } else{
                     pointerHover.transform.position = pointerHover.initialPos;
                    pointerHover.hover = true;
                } 
                
            } else if(textNum == 25 && explodingEnemy.activeSelf){
                if(explodingEnemy.activeSelf){
                    letterByLetter.completeText = "Take care of that enemy before we continue our lesson.";
                } else{
                     pointerHover.transform.position = pointerHover.initialPos;
                    pointerHover.hover = true;
                }
                
            }
            letterByLetter.finished = false;
            textBubble.SetActive(true);
            player.SetState("talking");
            if (whichFaceArray[textNum] == 0)
            {
                faceSprite.sprite = monmonFace;
            }
            else
            {
                faceSprite.sprite = oldLadyFace;
            }
        }
        else if (textBubble.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (letterByLetter.finished)
            {
                if (textNum < textArray.Length - 1)
                {
                    if (textNum != 31 && textNum != 14 && textNum != 17 && textNum != 18 && textNum != 24 && textNum != 25)
                    {
                        textNum++;

                        letterByLetter.letterNum = 0;
                        if (textNum == 16)
                        {
                            witchEnemy.SetActive(true);
                        }
                        else if (textNum == 23)
                        {
                            explodingEnemy.SetActive(true);
                        }



                        letterByLetter.completeText = textArray[textNum];
                        letterByLetter.finished = false;
                        if (whichFaceArray[textNum] == 0)
                        {
                            faceSprite.sprite = monmonFace;
                        }
                        else
                        {
                            faceSprite.sprite = oldLadyFace;
                        }


                    }
                    else
                    {
                        if (textNum == 14)
                        {
                            glowing = true;
                            if (monmonLight.activeSelf)
                            {
                                textNum++;

                                letterByLetter.letterNum = 0;


                                letterByLetter.completeText = textArray[textNum];
                                letterByLetter.finished = false;
                                if (whichFaceArray[textNum] == 0)
                                {
                                    faceSprite.sprite = monmonFace;
                                }
                                else
                                {
                                    faceSprite.sprite = oldLadyFace;
                                }
                            }
                        }
                        else if (textNum == 17)
                        {
                            helpText.SetActive(true);
                            helpTextText.text = "Hold down Right Mouse Button to use the moon ray. Aim it using your mouse cursor.";
                            pointerHover.hover = false;
                            pointerHover.transform.position = pointerHover.initialPos;
                            textNum = 18;
                            textBubble.SetActive(false);
                            npcText.text = "";
                            player.ResetState();


                        }
                        else if (textNum == 18)
                        {
                            if (witchEnemy.activeSelf)
                            {
                                npcText.text = "";
                                player.ResetState();
                                textBubble.SetActive(false);
                                
                            }
                            else
                            {
                                textNum = 19;
                                letterByLetter.letterNum = 0;
                               
                                letterByLetter.completeText = textArray[textNum];
                                letterByLetter.finished = false;
                                npcText.text = "";

                            }


                        } else if(textNum == 24){
                            helpText.SetActive(true);
                            helpTextText.text = "Press Left Mouse Button to melee attack.";
                            pointerHover.hover = false;
                            pointerHover.transform.position = pointerHover.initialPos;
                            textNum = 25;
                            textBubble.SetActive(false);
                            npcText.text = "";
                            player.ResetState();
                        } else if (textNum == 25)
                        {
                            if (explodingEnemy.activeSelf)
                            {
                                npcText.text = "";
                                player.ResetState();
                                textBubble.SetActive(false);
                            }
                            else
                            {
                                textNum = 26;
                                letterByLetter.letterNum = 0;


                                letterByLetter.completeText = textArray[textNum];
                                letterByLetter.finished = false;
                                npcText.text = "";

                            }


                        } 
                        else if (textNum == 31)
                        {
                            textNum = 30;
                            npcText.text = "";
                            textBubble.SetActive(false);
                            player.ResetState();
                             boatCollider.enabled = true;
                           
                        }
                    }

                }
                else
                {
                    textNum = 30;
                    npcText.text = "";
                    textBubble.SetActive(false);
                    player.ResetState();
                     boatCollider.enabled = true;
                }
            }
            else
            {
                letterByLetter.letterNum = letterByLetter.completeText.Length - 1;

            }


        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pointer.SetActive(true);

        }

    }


    public void YesChosen()
    {
        textNum = 17;
        npcText.text = textArray[textNum];
        buttonCanvas.SetActive(false);
        letterByLetter.letterNum = 0;
        letterByLetter.completeText = textArray[textNum];
        letterByLetter.finished = false;
        if (whichFaceArray[textNum] == 0)
        {
            faceSprite.sprite = monmonFace;
        }
        else
        {
            faceSprite.sprite = oldLadyFace;
        }

    }

    public void NoChosen()
    {
        textNum = 22;
        npcText.text = textArray[textNum];
        buttonCanvas.SetActive(false);
        letterByLetter.letterNum = 0;
        letterByLetter.completeText = textArray[textNum];
        letterByLetter.finished = false;
        if (whichFaceArray[textNum] == 0)
        {
            faceSprite.sprite = monmonFace;
        }
        else
        {
            faceSprite.sprite = oldLadyFace;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pointer.SetActive(false);
            textBubble.SetActive(false);
        }
    }
}
