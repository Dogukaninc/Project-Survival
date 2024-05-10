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
        Secondary = 1,
        Melee = 2
    }

    public Animator rigController;
    public Transform[] weaponSlots;

    public Transform crossHairTarget;
    public CharacterAiming characterAiming;
    public AmmoWidget ammoWidget;

    public bool isChangingWeapon = false;

    Firearm[] equipped_weapons = new Firearm[2];
    MeleeWeapon[] equipped_melees = new MeleeWeapon[3];

    int activeWeaponIndex;

    bool isHolstered = false;
    private void OnEnable()
    {
        GameStateHandler.Instance.onGamePause += () => this.enabled = false;
        GameStateHandler.Instance.onGameContinue += () => this.enabled = true;

    }
    private void OnDisable()
    {
        GameStateHandler.Instance.onGamePause -= () => this.enabled = false;
        GameStateHandler.Instance.onGameContinue -= () => this.enabled = true;

    }
    void Start()
    {
        Firearm existingWeapon = GetComponentInChildren<Firearm>();
        MeleeWeapon existingMelee = GetComponentInChildren<MeleeWeapon>();
        if (existingWeapon || existingMelee)
        {
            Equip(existingWeapon, existingMelee);
        }
    }

    public bool IsFiring()//Ateþ falan ederken elimizde silah var mý yok mu onu anlamak için
    {
        Firearm currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFiring;//Eldeki silah varsa ates etmesini true donduruyor
    }

    public MeleeWeapon GetActiveMelee()
    {
        return GetMelee(activeWeaponIndex);
    }
    public Firearm GetActiveWeapon()
    {
        return GetFirearm(activeWeaponIndex);
    }

    Firearm GetFirearm(int index)
    {
        if (index < 0 || index >= equipped_weapons.Length)// Out of bounds hatasý almamak için
        {
            return null;
        }
        return equipped_weapons[index];
    }

    MeleeWeapon GetMelee(int index)
    {
        if (index < 0 || index >= equipped_melees.Length)
        {
            return null;
        }
        return equipped_melees[index];
    }

    void Update()
    {
        var weapon = GetFirearm(activeWeaponIndex);
        var melee = GetMelee(activeWeaponIndex);
        bool notSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("notSprinting");//Animator'un 2 indisli layer'ýndaki notSprinting'i checkliyor. notSprinting default animation state'in adý !!!

        //Buraya atesli silahsa ates et, eger melee ise savur mantigi ekle
        if (weapon && !isHolstered && notSprinting && !melee)//Silah uygnsa ateþ et
        {
            weapon.UpdateWeapon(Time.deltaTime);
        }
        else if (!weapon && !isHolstered && notSprinting && melee)
        {
            melee.SwingMelee();
        }

        //Silahý kýnýna koy
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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(WeaponSlot.Melee);
        }

    }

    public void Equip(Firearm newFirearm, MeleeWeapon newMelee)
    {
        //Burada eðer alýnan silah firearm ise firearm deðilse melee olacak þelilde alýnanýn türüne bakýcaz
        Debug.Log($"Kusandigim Silah:");
        //Eger ayný türden bir silah alýyorsak elimizdekini yok etmemiz lazým. Ancak farklý türden bir silah alýyorsak elimizdeki yok olmamalý(Secondary-Primary)
        if (newFirearm != null)
        {
            int weaponSlotIndex = (int)newFirearm.weaponSlot;//Primary ya da secondary silah seçimi için slot indexini alýyor
            var weapon = GetFirearm(weaponSlotIndex);
            if (weapon)
            {
                Destroy(weapon.gameObject);
            }

            weapon = newFirearm;
            weapon.raycastDestination = crossHairTarget;
            weapon.recoil.characterAiming = characterAiming;
            weapon.recoil.rigController = rigController;
            weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);//Silahý nereye parentlayýp kuþanacak onu seciyor
            equipped_weapons[weaponSlotIndex] = weapon;

            SetActiveWeapon(newFirearm.weaponSlot);

            ammoWidget.Refresh(weapon.ammoCount);
        }

        if (newMelee != null)
        {
            int meleeSlotIndex = (int)newMelee.meleeSlot;
            var melee = GetMelee(meleeSlotIndex);
            if (melee)
            {
                Destroy(melee.gameObject);
            }
            melee = newMelee;
            //melee.raycastDestination = crossHairTarget;
            //melee.recoil.characterAiming = characterAiming;
            melee.rigController = rigController;
            melee.transform.SetParent(weaponSlots[meleeSlotIndex], false);//Silahý nereye parentlayýp kuþanacak onu seciyor
            equipped_melees[meleeSlotIndex] = melee;

            SetActiveWeapon(melee.meleeSlot);
            //ammoWidget.Refresh(weapon.ammoCount); Mermi miktarýný gösteren ikon sonsuz sembolü olacak
        }

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
        var weapon = GetFirearm(index);
        var melee = GetMelee(index);
        if (weapon && !melee)
        {
            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        else if (!weapon && melee)
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
        var weapon = GetFirearm(index);
        var melee = GetMelee(index);

        if (weapon && !melee)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;
        }
        else if (melee && !weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + melee.meleeName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;
        }


        isChangingWeapon = false;

    }
}
