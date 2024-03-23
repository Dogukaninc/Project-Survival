using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class CharacterAiming : MonoBehaviour
{
    [SerializeField] private float turningSpeed;
    //[SerializeField] private float aimDuration = .3f;
    Camera cam;

    RaycastWeapon weapon;

    void Start()
    {
        cam = Camera.main;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    void FixedUpdate()
    {
        float cameraRotation = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), Time.fixedDeltaTime * turningSpeed);
    }

    private void LateUpdate()
    {
        //weapon.UpdateWeapon(Time.deltaTime);
        //Çalýþmýyor
        /*
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
        */
    }
}
