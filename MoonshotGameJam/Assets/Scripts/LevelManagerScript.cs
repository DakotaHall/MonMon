using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{


    public void NextScene(){
        SceneManager.LoadScene("BoatPractice", LoadSceneMode.Single);
    }
}
