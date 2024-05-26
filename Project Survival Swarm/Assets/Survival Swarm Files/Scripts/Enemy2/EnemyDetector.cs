using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public Transform player;
    public float detectionRange;
    public LayerMask playerLayer;

    public bool PlayerInDetectionRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);
        return hitColliders.Length > 0;
    }
}
