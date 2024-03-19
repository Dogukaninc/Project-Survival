using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    public Transform Player;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;

    float timer = 0;

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            float sqDistance = (Player.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                if (agent.enabled != false)
                {
                    agent.destination = Player.position;
                }

            }
        }
        if (agent.enabled != false)
        {
            agent.destination = Player.position;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
