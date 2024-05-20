using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyState
{
    private GameObject target;

    public override void EnterState(EnemyAgent agent)
    {
        Debug.Log("Enter Attack State");
        target = agent.Detect();
        Attack(agent);
    }

    public override void UpdateState(EnemyAgent agent)
    {
        if (target == null || Vector3.Distance(agent.transform.position, target.transform.position) > agent.attackRange)
        {
            GameObject detectedObject = agent.Detect();
            if (detectedObject == null || (!detectedObject.CompareTag(agent.playerTag) && !detectedObject.CompareTag(agent.obstacleTag)))
            {
                agent.stateMachine.ChangeState(agent, new EnemyNavigatingState());
                return;
            }
            else if (detectedObject.CompareTag(agent.playerTag))
            {
                agent.stateMachine.ChangeState(agent, new EnemyChasingState());
                return;
            }
        }
        else
        {
            Attack(agent);
        }
    }

    public override void ExitState(EnemyAgent agent)
    {
        Debug.Log("Exit Attack state");
    }

    private void Attack(EnemyAgent agent)
    {
        Debug.Log(target.tag+ "'a Saldýrýyorum...");
        //saldýrý mekaniði 
    }
}
