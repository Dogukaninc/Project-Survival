using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera mainCamera;

    Ray ray;
    RaycastHit hitInfo;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
        transform.position = Physics.Raycast(ray, out hitInfo) ? hitInfo.point : ray.GetPoint(100);
    }
}
