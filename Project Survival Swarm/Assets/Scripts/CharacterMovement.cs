using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //CharacterController characterController;
    //Camera cam;
    Animator animator;

    //[SerializeField] private float turningSpeed;
    void Start()
    {

        //characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //cam = Camera.main;

        //Mouse'u ba�lang��ta oyun ekran�na kitle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        //Rotation();
    }

    private void Movement()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("InputX", Horizontal);
        animator.SetFloat("InputY", Vertical);

        //Vector3 direction = transform.right * Horizontal + transform.forward * Vertical;
        //characterController.Move();
    }

    //private void Rotation()
    //{

    //    //Mouse'u �evirdi�im y�ne d�necek
    //    float cameraRotation = cam.transform.rotation.eulerAngles.y;
    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), Time.deltaTime * turningSpeed);

    //}
}
