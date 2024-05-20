using System;
using UnityEngine;

namespace Survival_Swarm_Files.Scripts
{
    public class TimerTicker
    {
        public void CustomCountdown(float time, float maxValue, Action action)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                action?.Invoke();
                Debug.Log("Sayac bitti");
                //time = maxValue;
            }
        }
    }
}