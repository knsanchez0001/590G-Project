using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Projectile
    public GameObject projectile;

    // Speed of projectile
    public float muzzleVelocity;

    // Weapon stats
    public float rateOfFire;
    public float projectileSpread;
    public float reloadTime;
    public int magazineCapacity;
    public int damage;
    public float range;
    public bool fullAuto;
    public int shotsRemaining;

    public bool triggerPulled;
    public bool triggerSet;
    public bool reloading;

    public Camera fpsCam;
    public Transform muzzle;
    public bool allowInvoke = true;

    public ParticleSystem muzzleFlash;
    public AudioSource sound;

    Vector3 direction;

    private void Awake()
    {
        shotsRemaining = magazineCapacity;
        triggerSet = true;
    }

    private void WeaponInputs()
    {
        triggerPulled =
            fullAuto
                ? Input.GetKey(KeyCode.Mouse0)
                : Input.GetKeyDown(KeyCode.Mouse0);

        if (
            Input.GetKeyDown(KeyCode.R) && shotsRemaining < magazineCapacity && !reloading ||
            triggerSet && !reloading && shotsRemaining == 0
        )
        {
            Reload();
        }

        if (triggerSet && triggerPulled && !reloading && shotsRemaining > 0)
        {
            Shoot();
        }
    }

    private void Reload()
    {
        reloading = true;
        Invoke("Reloaded", reloadTime);
    }

    private void Reloaded()
    {
        reloading = false;
        shotsRemaining = magazineCapacity;
    }

    private void Shoot()
    {
        triggerSet = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit rayHit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out rayHit))
        {
            targetPoint = rayHit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 targetDisplacement = targetPoint - muzzle.position;

        float y = Random.Range(-projectileSpread, projectileSpread);
        float z = Random.Range(-projectileSpread, projectileSpread);

        direction = targetDisplacement + new Vector3(0, y, z);

        GameObject clone =
            Instantiate(projectile, muzzle.position, Quaternion.identity);
        clone.transform.forward = direction.normalized;
        clone.GetComponent<Rigidbody>().AddForce(direction * muzzleVelocity, ForceMode.Impulse);
        clone.GetComponent<Projectile>().damage = damage;
        clone.GetComponent<Projectile>().parentWeapon = transform.gameObject;

        muzzleFlash.Play();
        sound.Play();

        shotsRemaining--;
        Debug.Log (shotsRemaining);

        if(allowInvoke){
            Invoke("TriggerReset", 60f / rateOfFire);
            allowInvoke = false;
        }
        
    }

    private void TriggerReset()
    {
        triggerSet = true;
        allowInvoke = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WeaponInputs();
    }
}
