using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private TreeSO treeObject;
    [HideInInspector] public float currentHealth;

    void Start()
    {
        currentHealth = treeObject.healthValue;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    //private void TakeDamage(float damage)//Hasar alabilen her nesne icin global bir metod olusturulabilir
    //{
    //    currentHealth -= damage;
    //}

}
