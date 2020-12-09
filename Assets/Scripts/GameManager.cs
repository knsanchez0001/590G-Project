using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool endGameCalled = false;
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
    }

    public void PauseGame(){
        Debug.Log("PAUSEGAME");
    }

    void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
