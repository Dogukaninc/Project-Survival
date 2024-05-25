using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    [SerializeField] private PoolTestPrefab _prefabToPool;
    private ObjectPool<PoolTestPrefab> _pool;
    [SerializeField] private Transform _poolSpawnTransform;
    private int spawnCount = 0;

    void Start()
    {
        //InvokeRepeating(nameof(SpawnObject), 0.2f, .2f);
        ObjectPooler(_prefabToPool);
    }

    private void ObjectPooler(PoolTestPrefab _objectToPool)
    {
        _pool = new ObjectPool<PoolTestPrefab>(() => { return Instantiate(_objectToPool); },
            _object =>
            {
                _object.gameObject.SetActive(true);
                _object.transform.position = _poolSpawnTransform.position;
                _object.transform.SetParent(_poolSpawnTransform, true);
            }, _object => { _object.gameObject.SetActive(false); }, _object => { Destroy(_object.gameObject); }, false, 10, 20);
    }

    private void Update()
    {
        ShootPooledObject();
    }

    private void CreateObjectToPool()//Create fonksiyonunda objenin pool'una burdaki pool'u atayacağız
    {
        //Instatiate buraya taşınacak
        
    }
    
    private void ShootPooledObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolTestPrefab pooledObejct = _pool.Get();
            Rigidbody _rb = pooledObejct.transform.AddComponent<Rigidbody>();
            _rb.AddForce(Vector3.up, ForceMode.Impulse);
            
        }
    }

    // private void KillPooledObjects(GameObject _gameObject)
    // {
    //     _pool.Release(_gameObject);
    // }

}