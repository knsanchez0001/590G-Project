using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void EndGame(){
        Debug.Log("ENDGAME");
    }

    public void PauseGame(){
        Debug.Log("PAUSEGAME");
    }

    public void RestartGame(){
        Debug.Log("RESTARTGAME");
    }
}
