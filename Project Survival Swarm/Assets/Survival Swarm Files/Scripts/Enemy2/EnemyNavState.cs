using UnityEngine;
using UnityEngine.AI;

public class EnemyNavState : EnemyBaseState
{
    private readonly NavMeshAgent agent;
    private readonly Transform mainTarget;

    public EnemyNavState(Enemy enemy, NavMeshAgent agent, Transform mainTarget) : base(enemy)
    {
        this.agent = agent;
        this.mainTarget = mainTarget;
    }

    public override void Enter()
    {
        enemy.PlayAnimation(Enemy.WalkHash);
        agent.SetDestination(mainTarget.position);
    }

    public override void Update()
    {
        if (enemy.PlayerInDetectionRange())
        {
            enemy.stateMachine.ChangeState(new EnemyChaseState(enemy));
        }
    }

    public override void Exit()
    {
    }
}
