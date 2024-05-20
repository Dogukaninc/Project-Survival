using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationEvents : MonoBehaviour
{
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;

    void Start()
    {
        animationEvents.MeleeAnimationEvent.AddListener(OnAnimationEventMelee);
    }

    void OnAnimationEventMelee(string eventName)
    {
        Debug.Log(eventName);
        if (eventName == "attack_melee")
        {
            MeleeWeapon meleeWeapon = activeWeapon.GetActiveMelee();
            meleeWeapon.GiveDamage(10);
            Debug.Log("Event Çalýþtý ve Hasar Verildi");
        }
    }
}
