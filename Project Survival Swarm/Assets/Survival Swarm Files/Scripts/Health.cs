using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float MaxHealth;

    void Start()
    {
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Öldüm!!!");
            Destroy(gameObject);
        }
    }
}