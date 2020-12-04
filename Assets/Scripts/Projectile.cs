using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectileImpactGraphic;

    public int damage;
    public GameObject parentWeapon;

    private void OnCollisionEnter(Collision collision){
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.LookRotation(contact.normal);
        Vector3 pos = contact.point;
        Instantiate(projectileImpactGraphic, pos, rot);
        // Instantiate(projectileImpactGraphic, transform.position, Quaternion.identity);
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
