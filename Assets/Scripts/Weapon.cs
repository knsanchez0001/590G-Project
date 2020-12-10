using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public int totalCapacity;

    public int damage;

    public float range;

    public bool fullAuto;

    public int shotsRemaining;

    public int totalShotsRemaining;

    public bool triggerPulled;

    public bool triggerSet;

    public bool reloading;

    public Camera fpsCam;

    public Transform muzzle;

    public bool allowInvoke = true;

    public ParticleSystem muzzleFlash;

    public AudioSource sound;

    public AudioClip shootSound;

    public AudioClip reloadSound;

    public TextMeshProUGUI text;

    Vector3 direction;

    private void Awake()
    {
        shotsRemaining = magazineCapacity;
        totalShotsRemaining = totalCapacity;
        triggerSet = true;
        text = GameObject.Find("Ammo Count").GetComponent<TextMeshProUGUI>();
    }

    private void WeaponInputs()
    {
        triggerPulled =
            fullAuto
                ? Input.GetKey(KeyCode.Mouse0)
                : Input.GetKeyDown(KeyCode.Mouse0);

        // if (
        //     Input.GetKeyDown(KeyCode.R) && shotsRemaining < magazineCapacity && !reloading ||
        //     triggerSet && !reloading && shotsRemaining == 0
        // )
        if (
            !reloading &&
            triggerSet &&
            totalShotsRemaining > 0 &&
            (
            Input.GetKeyDown(KeyCode.R) && shotsRemaining < magazineCapacity ||
            shotsRemaining == 0
            )
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
        sound.clip = reloadSound;
        sound.Play();
        Invoke("Reloaded", reloadTime);
    }

    private void Reloaded()
    {
        reloading = false;
        sound.clip = shootSound;
        int temp = shotsRemaining;
        shotsRemaining = shotsRemaining + Mathf.Min(magazineCapacity - shotsRemaining, totalShotsRemaining);
        totalShotsRemaining = Mathf.Max(totalShotsRemaining - magazineCapacity + temp, 0);
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        sound.Play();

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

        float x = Random.Range(-projectileSpread, projectileSpread);
        float y = Random.Range(-projectileSpread, projectileSpread);

        direction = targetDisplacement.normalized;

        GameObject clone =
            Instantiate(projectile, muzzle.position, Quaternion.identity);
        clone.transform.forward = direction;
        clone
            .GetComponent<Rigidbody>()
            .AddForce(direction * muzzleVelocity + new Vector3(x, y, 0),
            ForceMode.Impulse);
        clone.GetComponent<Projectile>().damage = damage;

        shotsRemaining--;

        if (allowInvoke)
        {
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
        text.SetText(shotsRemaining + "/" + totalShotsRemaining);

        WeaponInputs();
    }
}
