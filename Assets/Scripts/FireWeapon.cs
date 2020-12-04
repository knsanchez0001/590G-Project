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

    public bool fullAuto;

    int shotsFired;

    public int shotsRemaining;

    public bool shooting;

    public bool reloading;

    public bool triggerSet;

    public Camera fpsCam;

    public Transform muzzle;

    public RaycastHit rayHit;

    public LayerMask layerMask;

    public GameObject projectileImpactGraphic;

    public ParticleSystem muzzleFlash;

    public AudioSource sound;

    public GameObject bullet;

    public float muzzleVelocity;

    private void Awake()
    {
        shotsRemaining = magazineCapacity;
        triggerSet = true;
    }

    private void WeaponInputs()
    {
        shooting =
            fullAuto
                ? Input.GetKey(KeyCode.Mouse0)
                : Input.GetKeyDown(KeyCode.Mouse0);

        if (
            Input.GetKeyDown(KeyCode.R) &&
            shotsRemaining < magazineCapacity &&
            !reloading ||
            shotsRemaining == 0
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
        muzzleFlash.Play();
        sound.Play();

        shotsRemaining--;
        triggerSet = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out rayHit, ~layerMask))
        {
            // Instantiate(projectileImpactGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            Debug.Log(rayHit.collider.name);
            targetPoint = rayHit.point;
            // if (rayHit.collider.CompareTag("Enemy"))
            // {
            // rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            // }
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 targetDisplacement = targetPoint - muzzle.position;

        float y = Random.Range(-projectileSpread, projectileSpread);
        float z = Random.Range(-projectileSpread, projectileSpread);

        Vector3 direction = targetDisplacement + new Vector3(0, y, z);

        GameObject newBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);

        newBullet.transform.forward = direction.normalized;

        newBullet.GetComponent<Rigidbody>().AddForce(direction * muzzleVelocity, ForceMode.Impulse);

        // newBullet.

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
