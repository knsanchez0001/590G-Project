using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    // Weapon List
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;

    // Which weapon is equipped
    private bool isPrimaryEquipped;
    private bool switching;

    // Start is called before the first frame update
    void Start()
    {
        primaryWeapon.SetActive(true);
        secondaryWeapon.SetActive(false);
        isPrimaryEquipped = true;
        switching = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !switching)
        {
            switching = true;
            if (isPrimaryEquipped)
            {
                primaryWeapon.SetActive(false);
                Invoke("switchWeapon", 1.0f);
                // secondaryWeapon.SetActive(true);
                // isPrimaryEquipped = false;
            }
            else
            {
                secondaryWeapon.SetActive(false);
                Invoke("switchWeapon", 1.0f);
                // primaryWeapon.SetActive(true);
                // isPrimaryEquipped = true;
            }
        }
    }

    private void switchWeapon()
    {
        if (isPrimaryEquipped)
        {
            secondaryWeapon.SetActive(true);
            isPrimaryEquipped = false;
            switching = false;
        }
        else
        {
            primaryWeapon.SetActive(true);
            isPrimaryEquipped = true;
            switching = false;
        }
    }
}
