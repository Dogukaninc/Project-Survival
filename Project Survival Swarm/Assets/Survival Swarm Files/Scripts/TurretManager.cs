using System;
using System.Collections;
using System.Collections.Generic;
using Survival_Swarm_Files.Scripts;
using Unity.Mathematics;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public LayerMask turretTargetLayer;
    public float attackSpeed;
    public GameObject bullet;
    public Transform attackpoint;

    private TimerTicker timer = new TimerTicker();
    private bool canAttack;

    private Action shooting;

    [SerializeField] private float attackRate;
    private float attackMax;

    private void OnEnable()
    {
        shooting += ShootingTurret;
    }

    private void OnDisable()
    {
        shooting -= ShootingTurret;
    }

    void Start()
    {
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, turretTargetLayer);
        if (colliders.Length > 0)
        {
            if (colliders[0].tag == "Enemy")
            {
                canAttack = true;
                RotateTowards(colliders[0].transform);
                Debug.Log("Alanda Düşman Tespit ETTİM");
            }
        }
        else
        {
            canAttack = false;
            Debug.Log("Alanda Düşman YOK");
        }

        if (canAttack)
        {
            timer.BulletInterval(ref attackRate, attackMax, shooting);
        }
    }

    private void ShootingTurret()
    {
        GameObject bullet = Instantiate(this.bullet, attackpoint);
    }

    private void TurretTargeter()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 50);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 10);
    }
}