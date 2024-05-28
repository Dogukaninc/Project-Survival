using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform mainTarget;
    public float detectionRange = 7f;
    public float attackRange = 2.1f;
    public LayerMask playerLayer;
    public Animator animator;

    public NavMeshAgent agent;
    public StateMachine stateMachine;

    public static readonly int WalkHash = Animator.StringToHash("WalkFWD");
    public static readonly int RunHash = Animator.StringToHash("RunFWD");
    public static readonly int AttackHash = Animator.StringToHash("Attack01");
    public static readonly int IdleHash = Animator.StringToHash("IdleNormal");


    public bool canAttack;
    public bool canIdle;

    void Start()
    {
        GameObject targetObject = GameObject.Find("Target");
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            this.player = playerObject.transform;
        }
        if (targetObject != null)
        {
            this.mainTarget = targetObject.transform;
        }

        this.agent = this.GetComponent<NavMeshAgent>();
        this.stateMachine = new StateMachine();

        this.stateMachine.ChangeState(new EnemyNavState(this, agent, mainTarget));

        this.PlayAnimation(WalkHash);

    }

    void Update()
    {
        stateMachine.Update();
    }

    public bool PlayerInDetectionRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);
        return hitColliders.Length > 0;
    }

    public bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    public void PlayAnimation(int animationHash)
    {
        animator.CrossFade(animationHash, 0.1f);
    }
    public void SetTriggerAnimation(int animationHash)
    {
        animator.SetTrigger(animationHash);
    }
    public void PerformAttack()
    {
        PlayAnimation(AttackHash);
        DealDamage();
        StartCoroutine(IdleDelay());
        StartCoroutine(AttackDealy());
    }
    IEnumerator IdleDelay()
    {
        yield return new WaitForSeconds(1);
        canIdle = true;
    }
    IEnumerator AttackDealy()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }

    private void DealDamage()
    {

        Debug.Log("Player damaged!");
    }
}
