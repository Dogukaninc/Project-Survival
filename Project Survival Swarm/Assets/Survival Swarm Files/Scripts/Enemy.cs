using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemySO enemySO;
    public EnemySO EnemySO => enemySO;
    
    
    [Header(" Enemy Properties ")] 
    private float movementSpeed;
    private float damagePower;
    
    [Space(10)]
    private Animator animator;
    
    void Start()
    {
        movementSpeed = EnemySO.speed;
        damagePower = EnemySO.power;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damagePoint)
    {
        throw new System.NotImplementedException();
    }
}