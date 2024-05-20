using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private EnemyState currentState;

    public void ChangeState(EnemyAgent agent, EnemyState newState)
    {
        currentState?.ExitState(agent);
        currentState = newState;
        currentState.EnterState(agent);
    }

    public void Update(EnemyAgent agent)
    {
        currentState?.UpdateState(agent);
    }
}
