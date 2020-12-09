using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectileImpactGraphic;

    public LayerMask enemyMask;

    public int damage = 10;

    private void OnCollisionEnter(Collision collision){
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.LookRotation(contact.normal);
        Vector3 pos = contact.point;
        
        GameObject other = collision.gameObject;

        while(other.transform.parent != null)
        {
            other = other.transform.parent.gameObject;
        }
        
        
        if(other.layer == 12 || other.layer == 8){
            Debug.Log(other.GetComponent<Health>().health);

            other.GetComponent<Health>().DamageHealth(damage);
        }
        else {
            Instantiate(projectileImpactGraphic, pos, rot);
        }


        Destroy(transform.gameObject);
    }

    void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // parentWeapon.GetComponent<Weapon>().shotsRemaining--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
