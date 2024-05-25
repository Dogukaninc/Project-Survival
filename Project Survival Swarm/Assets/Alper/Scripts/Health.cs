using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentealth;
    Ragdoll ragdoll;
    SkinnedMeshRenderer SkinnedMeshRenderer;

    public float blinkInt;
    public float blinkDur;
    float blinkTimer;

    private void Start()
    {
        currentealth = maxHealth;
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ragdoll = GetComponent<Ragdoll>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitbox = rigidBody.gameObject.AddComponent<HitBox>();
            hitbox.health = this;
        }
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        currentealth -= damage;
        if (currentealth < 0)
        {
            Die();
        }
        blinkTimer = blinkDur;
    }

    private void Die()
    {
        ragdoll.ActiveRagdoll();
    }
    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDur);
        float intensty = (lerp * blinkInt) + 1.0f;
        SkinnedMeshRenderer.material.color = Color.white * intensty;
    }
}
