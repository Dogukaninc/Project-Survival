using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //[SerializeField] private TreeSO treeObject;
    [HideInInspector] public float currentHealth;
    private float maxHealth;
    public float MaxHealth => maxHealth;

    public enum ParentType
    {
        Player,
        Other
    }

    public ParentType parentType;

    private void Start()
    {
        maxHealth = currentHealth;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     TakeDamage(10);
        // }
        if (parentType == ParentType.Player)
        {
            if (currentHealth <= 0)
            {
                GameStateHandler.Instance.GameOver();
                Debug.Log("Öldüm!!!");
            }
        }
        else
        {
            if (currentHealth <= 0)
            {
                Debug.Log(" Hedef Öldü !!!");
            }
        }

    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Oyuncu öldüðünde yapýlacak iþlemler burada
        //Destroy(gameObject);
    }

}
