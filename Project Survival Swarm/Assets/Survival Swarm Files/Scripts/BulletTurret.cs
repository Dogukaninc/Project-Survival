using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.AddForce(transform.forward*5,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(5);
        }
    }
}
