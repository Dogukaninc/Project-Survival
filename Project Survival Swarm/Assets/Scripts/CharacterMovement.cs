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

        //Mouse'u baþlangýçta oyun ekranýna kitle
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

    //    //Mouse'u çevirdiðim yöne dönecek
    //    float cameraRotation = cam.transform.rotation.eulerAngles.y;
    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), Time.deltaTime * turningSpeed);

    //}
}
