using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class CharacterAiming : MonoBehaviour
{
    public float turningSpeed = 15f;
    public float aimDuration = .3f;
    public bool isAiming;
    public Transform cameraLookAt;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    Camera cam;
    Animator animator;
    ActiveWeapon activeWeapon;

    int isAimingParam = Animator.StringToHash("isAiming");


    void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    void Update()
    {
        isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam, isAiming);

        var weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            weapon.recoil.recoilModifier = isAiming ? 0.3f : 1.0f;
        }
    }

    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        float cameraRotation = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), Time.fixedDeltaTime * turningSpeed);
    }

}
