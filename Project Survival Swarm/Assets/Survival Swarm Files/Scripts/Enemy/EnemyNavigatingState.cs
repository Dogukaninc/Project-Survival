using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigatingState : EnemyState
{
    public override void EnterState(EnemyAgent agent)
    {
        Debug.Log("Enter Navigate Stata");
        agent.MoveTo(agent.mainTarget.position);
    }

    public override void UpdateState(EnemyAgent agent)
    {
        GameObject detectedObject = agent.Detect();
        if (detectedObject != null)
        {
            if (detectedObject.CompareTag(agent.playerTag))
            {
                agent.stateMachine.ChangeState(agent, new EnemyChasingState());
                return;
            }
            else if (detectedObject.CompareTag(agent.obstacleTag))
            {
                agent.stateMachine.ChangeState(agent, new EnemyAttackingState());
                return;
            }
        }
    }

    public override void ExitState(EnemyAgent agent)
    {
        Debug.Log("Exit Navigate State");
    }
}
