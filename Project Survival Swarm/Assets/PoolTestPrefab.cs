using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolTestPrefab : MonoBehaviour
{
    private IObjectPool<PoolTestPrefab> _pool;
    public IObjectPool<PoolTestPrefab> Pool { set => _pool = value; }

    [SerializeField] private float delay;
    
    private void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(delay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _pool.Release(this);
    }

}