using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public float viewRange;
    public LayerMask detectionLayer;
    private Collider[] hitColliders;

    void Start()
    {
        hitColliders = new Collider[5]; //max algýlama sayýsý 
    }

    public GameObject Detect()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, viewRange, hitColliders, detectionLayer);

        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i] != null)
            {
                return hitColliders[i].gameObject;
            }
        }

        return null;
    }
}
