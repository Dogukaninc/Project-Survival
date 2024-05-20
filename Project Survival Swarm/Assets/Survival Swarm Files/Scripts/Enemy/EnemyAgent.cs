using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public Transform mainTarget;
    public float viewRange;
    public float attackRange;
    public string playerTag = "Player";
    public string obstacleTag = "Obstacle";

    private NavMeshAgent navMeshAgent;
    private EnemySensor sensor;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<EnemySensor>();
        stateMachine = new EnemyStateMachine();
        stateMachine.ChangeState(this, new EnemyNavigatingState());
    }

    void Update()
    {
        stateMachine.Update(this);
    }

    public void MoveTo(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    public GameObject Detect()
    {
        return sensor.Detect();
    }
}
