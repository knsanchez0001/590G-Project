using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    public int type;

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
            Debug.Log(gameObject.name);
            if (type == 0)
            {
                Health health = other.GetComponent<Health>();
                if (health.health < health.maxHealth)
                {
                    health.Heal();
                    Destroy (gameObject);
                }
            }

            // else
            // {
            //     Weapon w;
            //     if(type == 1)
            //     {
            //         w = GameObject.FindWithTag("Pistol").GetComponent<Weapon>();
            //     }
            //     else if (type == 2)
            //     {
            //         w = GameObject.FindWithTag("Rifle").GetComponent<Weapon>();
            //     }
            //     if(w.totalShotsRemaining < w.totalCapacity)
            //     {
            //         w.totalShotsRemaining = w.totalCapacity;
            //         Destroy (gameObject);
            //     }
            // }
            if (type == 1)
            {
                GameObject pistol = GameObject.FindWithTag("Pistol");
                if (pistol != null)
                {
                    Weapon weapon = pistol.GetComponent<Weapon>();
                    if (weapon.totalShotsRemaining < weapon.totalCapacity)
                    {
                        weapon.totalShotsRemaining = weapon.totalCapacity;
                        Destroy (gameObject);
                    }
                }
            }
            if (type == 2)
            {
                GameObject rifle = GameObject.FindWithTag("Rifle");
                if (rifle != null)
                {
                    Weapon weapon = rifle.GetComponent<Weapon>();
                    if (weapon.totalShotsRemaining < weapon.totalCapacity)
                    {
                        weapon.totalShotsRemaining = weapon.totalCapacity;
                        Destroy (gameObject);
                    }
                }
            }
        }
    }
}
