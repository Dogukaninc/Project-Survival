using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;
    public AmmoWidget ammoWidget;
    public bool isReloading;

    GameObject magazineHand;

    void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <= 0)
            {
                isReloading = true;
                rigController.SetTrigger("reload_weapon");
            }

            if (weapon.isFiring)
            {
                ammoWidget.Refresh(weapon.ammoCount);
            }
        }


    }

    void OnAnimationEvent(string eventName)
    {
        Debug.Log(eventName);

        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;

            case "drop_magazine":
                DropMagazine();
                break;

            case "refill_magazine":
                RefillMagazine();
                break;

            case "attach_magazine":
                AttachMagazine();
                break;


            default:
                break;
        }
    }

    void DetachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true); //burdaki true olayýna bak ne pozisyonu bu
        weapon.magazine.SetActive(false);

    }

    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);

    }

    //Tabanca için ayrý bir metod oluþturulabilir

    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }

    void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");//Animasyonu bir kere oynattýktan sonra bir sonraki sefer baþtan oynamasý için

        ammoWidget.Refresh(weapon.ammoCount);
        isReloading = false;//Aslýnda reload'dan sonra hala ir kaç frame silah doðru konumda olmadýðý için yetersiz bir mantýk
        //Ancak þimdilik iþ görüyor
    }


}
