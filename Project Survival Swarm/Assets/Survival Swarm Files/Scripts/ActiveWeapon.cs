using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class ActiveWeapon : MonoBehaviour
{
    RaycastWeapon weapon;
    public UnityEngine.Animations.Rigging.Rig handIk;
    public Transform weaponParent;
    public Animator rigController;

    public Transform crossHairTarget;

    public Transform weaponRightGrip;
    public Transform weaponLeftGrip;


    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }

    }

    void Update()
    {

        if (weapon)
        {
            if (Input.GetMouseButton(0))
            {
                weapon.StartFiring();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFiring();
            }

            weapon.UpdateBullets(Time.deltaTime);

            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
        }

        //Silahý kýnýna koy
        if (Input.GetKeyDown(KeyCode.X))
        {
            bool isHolstered = rigController.GetBool("holster_weapon");
            rigController.SetBool("holster_weapon", !isHolstered);
        }

    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        Debug.Log("Silah kuþandým");
        rigController.Play("equip_" + weapon.weaponName);

    }

}
