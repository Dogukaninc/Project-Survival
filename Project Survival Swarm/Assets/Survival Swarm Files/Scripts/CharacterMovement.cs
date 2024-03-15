using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    public float jumpTime;
    public bool isJumping;

    void Start()
    {

        animator = GetComponent<Animator>();

        //Mouse'u ba�lang��ta oyun ekran�na kitle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("InputX", Horizontal);
        animator.SetFloat("InputY", Vertical);
    }

    private void Sprint()
    {

    }

    private void Jump()
    {
        //E�er karakter yerdeyse ve �nceki z�plama eylemi bittiyse z�playabilsin
       
    }
}
