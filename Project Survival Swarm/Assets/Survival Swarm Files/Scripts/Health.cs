using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //[SerializeField] private TreeSO treeObject;
    [HideInInspector] public float currentHealth;
    public float health = 100f;

    void Start()
    {
       // currentHealth = treeObject.healthValue;
    }

    void Update()
    {


    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Oyuncu öldüðünde yapýlacak iþlemler burada
        //Destroy(gameObject);
    }

}
