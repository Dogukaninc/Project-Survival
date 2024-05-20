using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState(EnemyAgent agent);
    public abstract void UpdateState(EnemyAgent agent);
    public abstract void ExitState(EnemyAgent agent);
}
