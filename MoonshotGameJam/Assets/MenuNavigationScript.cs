using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MenuNavigationScript : MonoBehaviour
{
    public GameObject settings;
    public GameObject quitPanel;
    public GameObject mainMenuOptions;
    public GameObject[] stars;
    public GameObject selectedButton;
    public GameObject lastSelectedButton;
    public GameObject PlayButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;
    public GameObject settingsBackButton;
    public GameObject controlsBackButton;
    public GameObject quitBackButton;
    public FadeScreenUI fadeScreen;
    public bool play;
    public GameObject canvas;
    public GameObject controlPanel;
    public GameObject controlButton;
    public Texture2D cursorTexture;
    void Start()
    {
         PlayerPrefs.DeleteAll();
         Vector2 cursorHotspot = new Vector2 (cursorTexture.width / 2, cursorTexture.height / 2);
         Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    void Update()
    {
        if(play){
            if(fadeScreen.fadeScreen.color.a >= 1){
                SceneManager.LoadScene("Lighthouse",LoadSceneMode.Single);
            }
        } else{
            if(selectedButton == null){
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }
        
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) ){
            if(lastSelectedButton == PlayButton){
                 play = true;
            fadeScreen.fadeOut = true;
            } else if(lastSelectedButton == SettingsButton){
                quitPanel.SetActive(false);
                mainMenuOptions.SetActive(false);
                settings.SetActive(true);
            }else if(lastSelectedButton == QuitButton){
                quitPanel.SetActive(true);
                mainMenuOptions.SetActive(false);
                settings.SetActive(false);
            } else if(lastSelectedButton == controlButton){
                controlPanel.SetActive(true);
            }
        }
        }
        
    }

    public void SetActivePanel(string panel){
        if(panel == "MainMenu"){
            quitPanel.SetActive(false);
                mainMenuOptions.SetActive(true);
                settings.SetActive(false);
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        } else if(panel == "Settings"){
                quitPanel.SetActive(false);
                mainMenuOptions.SetActive(false);
                settings.SetActive(true);
        } else if(panel == "Play"){
            play = true;
            fadeScreen.fadeOut = true;
        } else if(panel == "Quit"){
                quitPanel.SetActive(true);
                mainMenuOptions.SetActive(false);
                settings.SetActive(false);
        }
    }
    public void EnableControls(){
        controlPanel.SetActive(true);
    }
    public void DisableControls(){
        controlPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(controlButton);
        
    }

    public void QuitApplication(){
        Application.Quit();
    }
    
}
