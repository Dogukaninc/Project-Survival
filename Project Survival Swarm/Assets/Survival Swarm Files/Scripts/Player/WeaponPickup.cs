using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public Firearm weaponPrefab;
    public MeleeWeapon meleePrefab;

    public enum PickupType { Firearm, Melee }
    public PickupType typeOfPickup;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();//Oyuncunun activeweapon scriptini cagiriyor

        if (activeWeapon)
        {

            if (typeOfPickup == PickupType.Firearm)
            {
                Firearm firearm = Instantiate(weaponPrefab);
                activeWeapon.Equip(firearm, null);
            }
            else if (typeOfPickup == PickupType.Melee)
            {
                MeleeWeapon melee = Instantiate(meleePrefab);
                activeWeapon.Equip(null, melee);
            }

        }

    }

}
