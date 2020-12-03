using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    public int damage;

    public int magazineCapacity;

    public float rateOfFire;

    public float projectileSpread;

    public float range;

    public float reloadTime;

    public bool isFullAuto;

    int shotsFired;

    public int shotsRemaining;

    public bool shooting;

    public bool reloading;

    public bool triggerSet;

    public Camera fpsCam;

    public Transform muzzle;

    public RaycastHit rayHit;

    public LayerMask whatIsEnemy;

    public GameObject projectileImpactGraphic;
    public ParticleSystem muzzleFlash;

    public AudioSource sound;


    private void Awake(){
        shotsRemaining = magazineCapacity;
        triggerSet = true;
    }
    private void WeaponInputs()
    {
        shooting =
            isFullAuto
                ? Input.GetKey(KeyCode.Mouse0)
                : Input.GetKeyDown(KeyCode.Mouse0);

        if (
            Input.GetKeyDown(KeyCode.R) &&
            shotsRemaining < magazineCapacity &&
            !reloading || shotsRemaining == 0
        )
        {
            Reload();
        }

        if (triggerSet && shooting && !reloading && shotsRemaining > 0)
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
        // Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);
        muzzleFlash.Play();
        sound.Play();

        shotsRemaining--;
        triggerSet = false;

        float x = Random.Range(-projectileSpread, projectileSpread);
        float y = Random.Range(-projectileSpread, projectileSpread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        if (
            Physics
                .Raycast(fpsCam.transform.position,
                fpsCam.transform.forward,
                out rayHit,
                range,
                whatIsEnemy)
        )
        {
            Instantiate(projectileImpactGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            Debug.Log(rayHit.collider.name);

            // if (rayHit.collider.CompareTag("Enemy"))
            // {
                // rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            // }
        }

        Invoke("TriggerReset", 60f / rateOfFire);
    }

    private void TriggerReset()
    {
        triggerSet = true;
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
