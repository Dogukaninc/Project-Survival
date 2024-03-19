using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public SphereCollider detectionRange;
    public Transform turretHead;
    public float rotationSpeed = 5.0f;
    public float fireRate = 1.0f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private List<GameObject> targets = new List<GameObject>();
    private float fireTimer = 0.0f;

    void Start()
    {
        detectionRange = GetComponent<SphereCollider>();
    }

    void Update()
    {
        GameObject nearestTarget = GetNearestTarget();
        if (nearestTarget)
        {
            RotateTurretHead(nearestTarget.transform.position);
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Fire(nearestTarget);
                fireTimer = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Remove(other.gameObject);
        }
    }

    GameObject GetNearestTarget()
    {
        GameObject nearest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;

                nearest = target;
            }
        }

        return nearest;
    }

    void RotateTurretHead(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - turretHead.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation *= Quaternion.Euler(0, 90, 0); //Taretin Yönü Düzgün Olmasý Ýçin 
        turretHead.rotation = Quaternion.Lerp(turretHead.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void Fire(GameObject target)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        Vector3 direction = target.transform.position - firePoint.position;
        rb.velocity = direction.normalized * bulletSpeed;
    }
}
