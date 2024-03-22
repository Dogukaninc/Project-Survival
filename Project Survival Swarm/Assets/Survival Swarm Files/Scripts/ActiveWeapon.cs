using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }

    public Animator rigController;
    public Transform[] weaponSlots;

    public Transform crossHairTarget;
    public Cinemachine.CinemachineFreeLook playerCamera;

    RaycastWeapon[] equipped_weapons = new RaycastWeapon[2];
    int activeWeaponIndex;

    bool isHolstered = false;

    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }

    }
    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= equipped_weapons.Length)// Out of bounds hatas� almamak i�in
        {
            return null;
        }
        return equipped_weapons[index];
    }

    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);

        if (weapon && !isHolstered)
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

        //Silah� k�n�na koy
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }

    }

    public void Equip(RaycastWeapon newWeapon)
    {

        Debug.Log("Silah ku�and�m");
        //Eger ayn� t�rden bir silah al�yorsak elimizdekini yok etmemiz laz�m. Ancak farkl� t�rden bir silah al�yorsak elimizdeki yok olmamal�(Secondary-Primary)

        int weaponSlotIndex = (int)newWeapon.weaponSlot;//PRimary ya da secondary silah se�imi i�in slot indexini al�yor
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.recoil.playerCam = playerCamera;
        weapon.recoil.rigController = rigController;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        equipped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

    }

    private void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));

    }

    IEnumerator SwitchWeapon(int holsterIndex, int activeIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activeIndex));
        activeWeaponIndex = activeIndex;

    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }

    }

    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;
        }
    }
}
