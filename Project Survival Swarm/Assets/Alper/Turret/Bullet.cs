using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 7f;

    private void Start()
    {
        Invoke("DestroyObject", destroyTime);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
