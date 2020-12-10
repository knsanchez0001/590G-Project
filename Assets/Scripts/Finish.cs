using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (collision.gameObject.layer == 8)
        {
            Debug.Log(gameObject.name);
            FindObjectOfType<GameManager>().EndGame();
            
        }
    }
}
