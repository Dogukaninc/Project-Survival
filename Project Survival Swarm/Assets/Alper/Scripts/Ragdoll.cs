using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Animator animator;
    NavMeshAgent agent;
    public float delay = 10f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactiveRagdoll();
    }

    public void DeactiveRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;

    }

    public void ActiveRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        agent.enabled = false;
        animator.enabled = false;
        Destroy(gameObject, delay);
    }
}
