using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool paused = false;
    public GameObject GameUI;
    public GameObject PauseUI;
    bool endGameCalled = false;

    void Awake(){
        
        
    }

    public void EndGame(){
        if(!endGameCalled){
            endGameCalled = true;
            Debug.Log("ENDGAME");
            RestartGame();
        }
    }

    public void StartGame(){
        Debug.Log("STARTGAME");
        SceneManager.LoadScene(1);
        GameUI.SetActive(true);
        PauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame(){
        if(!paused){
            paused = true;
            Debug.Log("PAUSEGAME");
            GameUI.SetActive(false);
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }else {
            paused = false;
            Debug.Log("RESUMEGAME");
            GameUI.SetActive(true);
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
