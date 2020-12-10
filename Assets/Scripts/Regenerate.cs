using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (collision.gameObject.layer == 8)
        {
            Health health = other.GetComponent<Health>();
            if(health.health < health.maxHealth){
                health.Heal();
                Destroy(gameObject);
            }
            
        }
    }
}
