using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime;
    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public bool IsTimeUp()
    {
        return currentTime <= 0;
    }

    public void ResetTimer()
    {
        currentTime = countdownTime;
    }
}
