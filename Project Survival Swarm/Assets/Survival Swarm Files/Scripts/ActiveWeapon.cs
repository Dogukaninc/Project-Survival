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
    public CharacterAiming characterAiming;
    public AmmoWidget ammoWidget;

    public bool isChangingWeapon = false;

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

    public bool IsFiring()//Ate� falan ederken elimizde silah var m� yok mu onu anlamak i�in
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFiring;
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
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
        bool notSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("notSprinting");//Animator'un 2 indisli layer'�ndaki notSprinting'i checkliyor. notSprinting default animation state'in ad� !!!
        if (weapon && !isHolstered && notSprinting)
        {
            weapon.UpdateWeapon(Time.deltaTime);
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
        weapon.recoil.characterAiming = characterAiming;
        weapon.recoil.rigController = rigController;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        equipped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

        ammoWidget.Refresh(weapon.ammoCount);

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

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;

    }

    IEnumerator HolsterWeapon(int index)
    {
        isChangingWeapon = true;
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
        isChangingWeapon = false;

    }

    IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapon = true;
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
        isChangingWeapon = false;

    }
}
