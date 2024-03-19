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

        //Mouse'u baþlangýçta oyun ekranýna kitle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("InputX", Horizontal);
        animator.SetFloat("InputY", Vertical);
    }

}
