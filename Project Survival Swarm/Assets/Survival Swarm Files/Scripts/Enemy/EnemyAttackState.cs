using System.Collections;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private readonly Enemy enemy;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.canAttack = true;
        enemy.canIdle = false;
    }

    public void Update()
    {
        if (!enemy.PlayerInAttackRange())
        {
            enemy.stateMachine.ChangeState(new EnemyChaseState(enemy));
        }
        else if (!enemy.canAttack && enemy.PlayerInAttackRange() && enemy.canIdle)
        {
            enemy.player.gameObject.GetComponent<Health>().currentHealth -= 5;
            enemy.PlayAnimation(Enemy.IdleHash);
            Debug.Log("<color=yellow> Range'deyim ama sald�ram�yorum </color>");
            enemy.canIdle = false;
        }
        else if (enemy.canAttack && enemy.PlayerInAttackRange() && !enemy.canIdle)
        {
            AudioManager.Instance.Play("MonsterAttack");
            enemy.PerformAttack();
            Debug.Log("<color=blue> Range'deyim Sald�rd�m </color>");
            enemy.canAttack = false;
        }
    }


    public void Exit()
    {
    }
}
