using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    private GameObject target;

    public override void EnterState(EnemyAgent agent)
    {
        Debug.Log("Enter Chase State");
        target = agent.Detect();
    }

    public override void UpdateState(EnemyAgent agent)
    {
        GameObject detectedObject = agent.Detect();
        if (detectedObject == null || !detectedObject.CompareTag(agent.playerTag))
        {
            agent.stateMachine.ChangeState(agent, new EnemyNavigatingState());
            return;
        }

        if (Vector3.Distance(agent.transform.position, target.transform.position) <= agent.attackRange)
        {
            agent.stateMachine.ChangeState(agent, new EnemyAttackingState());
            return;
        }

        agent.MoveTo(target.transform.position);
    }

    public override void ExitState(EnemyAgent agent)
    {
        Debug.Log("Exit Chase State");
    }
}
