using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int health;

    void Awake(){
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageHealth(int damage){
        health = Mathf.Max(health - damage, 0);
    }

    public void Heal(){
        health = maxHealth;
    }
}
