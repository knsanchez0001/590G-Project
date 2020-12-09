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

    public void PauseGame(){
        Debug.Log("PAUSEGAME");
    }

    void RestartGame(){
        Debug.Log("RESTARTGAME");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
