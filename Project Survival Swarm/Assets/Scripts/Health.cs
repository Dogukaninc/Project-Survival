using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    public event Action OnTakeDamage;
    public event Action OnDie;
    private bool isDefense; // savunma yapýldýðýnda can gitmemesi için kullanýlabilir. 

    public bool IsDead => currentHealth == 0;

    private void Start()
    {
        currentHealth = maxHealth;
        isDefense = false;
    }

    public void SetDefence(bool isDefense)
    {
        this.isDefense = isDefense;
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0) { return; }
        //if (isDefance == true) { return; }
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        OnTakeDamage?.Invoke();

        if (currentHealth == 0)
        {
            OnDie?.Invoke();
        }

        Debug.Log(gameObject.name + " Health: " + currentHealth);

    }
}
