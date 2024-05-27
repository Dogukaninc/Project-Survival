using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.PlayAnimation(Enemy.RunHash);
    }

    public override void Update()
    {
        if (enemy.PlayerInAttackRange())
        {
            enemy.stateMachine.ChangeState(new EnemyAttackState(enemy));
        }
        else if (!enemy.PlayerInDetectionRange())
        {
            enemy.stateMachine.ChangeState(new EnemyNavState(enemy, enemy.agent, enemy.mainTarget));
        }
        else
        {
            enemy.agent.SetDestination(enemy.player.position);
        }
    }

    public override void Exit()
    {
    }
}
