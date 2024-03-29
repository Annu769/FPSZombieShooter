using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private int slectedWeapon = 0;
    private void Start()
    {
        SlectWeapon();
    }
    private void Update()
    {
        int previousWeapon = slectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (slectedWeapon < transform.childCount - 1)
                slectedWeapon = 0;
            else
                slectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (slectedWeapon >= 0)
                slectedWeapon = transform.childCount - 1;
            else
                slectedWeapon--;
        }
        if (previousWeapon != slectedWeapon)
        {
            SlectWeapon();
        }

    }

    private void SlectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == slectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
