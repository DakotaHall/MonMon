using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
public class WitchDialogueScript : MonoBehaviour
{
    public GameObject textBubble;
    public string[] textArray;
    public int textNum = 0;
    public SpriteRenderer faceSprite;
    public Sprite monmonFace;
    public Sprite witchFace;
    public bool canInteract;
    public TextMeshPro npcText;
    public LetterByLetterScript letterByLetter;
    public PlayerScript player;
    public Light2D oldLadyLight;
    public GameObject helpText;
    public bool helpTextActivated;
    public Vector3 witchPos;
    public FinalWitchScript finalWitch;
    public GameObject introTeleport;
    public Camera mainCam;
    public bool moveCam;
    public Vector3 finalCameraPos;
    public CameraFollowScript cameraFollow;
    public WitchTrackingScript witchTracker;
    public CheckpointScript checkpointScript;
    public int[] whichFaceArray;
    public FadeScreenScript fadeScreen;
    public GameObject thanksForPlaying;
    public GameObject pointer;
    public bool defeated;
    void Start()
    {
        mainCam = Camera.main;


    }

    void Update()
    {
        if(defeated){
            if(player.CheckGrounded() && !player.GetState("talking")){
                player.SetState("talking");
                player.myAnim.Rebind();
            }
        }
        pointer.transform.localScale = finalWitch.transform.localScale;
        if(textNum == 20){
            if(fadeScreen.fadeScreen.color.a >= 1 && !thanksForPlaying.activeSelf){
                thanksForPlaying.SetActive(true);

            }
            if(Input.anyKey && thanksForPlaying.activeSelf){
                Application.Quit();
            }
        } else{
            if (moveCam)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, finalCameraPos, 5 * Time.deltaTime);
            if (mainCam.transform.position == finalCameraPos)
            {
                moveCam = false;
            }
        }
        if (textBubble.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if(!pointer.activeSelf){
                pointer.SetActive(true);
            }
            if (letterByLetter.finished)
            {
                if (textNum < textArray.Length - 1)
                {
                    if (textNum == 5)
                    {
                        textBubble.SetActive(false);
                        pointer.SetActive(false);
                        player.ResetState();
                    checkpointScript.doneTalking = true;
                        letterByLetter.letterNum = 0;
                        letterByLetter.completeText = textArray[textNum];
                        letterByLetter.finished = false;
                        witchTracker.enabled = true;
                        cameraFollow.enabled = true;
                        finalWitch.enabled = true;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        textNum++;
                        if (whichFaceArray[textNum] == 0)
                        {
                            faceSprite.sprite = monmonFace;
                            npcText.color = Color.blue;
                        }
                        else
                        {
                            faceSprite.sprite = witchFace;
                            npcText.color = Color.red;
                        }
                        letterByLetter.letterNum = 0;
                        letterByLetter.completeText = textArray[textNum];
                        letterByLetter.finished = false;
                    }


                }
                else
                {
                    fadeScreen.fadeOut = true;
                    textNum = 20;
                    textBubble.SetActive(false);
                    pointer.SetActive(false);
                }
            }
            else
            {
                letterByLetter.letterNum = letterByLetter.completeText.Length - 1;

            }


        }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            if (player.CheckGrounded())
            {
                if(mainCam.transform.position != finalCameraPos){
                    moveCam = true;
                    cameraFollow.enabled = false;
                }
                if (!player.GetState("talking"))
                {
                    player.SetState("talking");
                    player.myRigidbody.velocity = Vector3.zero;
                    player.myAnim.Rebind();
                    player.myAnim.SetBool("Grounded", true);
                    player.myAnim.Play("MonMonIdleAnimation");
                }
                if (!moveCam && !introTeleport.activeSelf && !finalWitch.gameObject.activeSelf)
                {
                    Vector3 witchPos = finalWitch.transform.position;
                    if (player.transform.position.x > 0)
                    {
                        player.transform.localScale = new Vector3(-1, 1, 1);
                        witchPos.x = -5;
                    }
                    else
                    {
                        witchPos.x = 6;
                        finalWitch.transform.localScale = new Vector3(-1, 1, 1);
                        player.transform.localScale = Vector3.one;
                    }
                    finalWitch.transform.position = witchPos;
                    introTeleport.transform.position = witchPos;
                    introTeleport.gameObject.SetActive(true);
                }


            }


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.ResetState();


        }
    }

    public void ActivateWitch()
    {
        finalWitch.gameObject.SetActive(true);
    }

    public void ActivateDialogue()
    {
        textBubble.SetActive(true);
        pointer.SetActive(true);
         letterByLetter.letterNum = 0;
            letterByLetter.completeText = textArray[textNum];
            letterByLetter.finished = false;
    }

    public void PlayDefeatedText(){
        letterByLetter.letterNum = 0;
        textNum++;
        letterByLetter.completeText = textArray[textNum];
        letterByLetter.finished = false;
        textBubble.SetActive(true);
        pointer.SetActive(true);
        defeated = true;
    }   

}
