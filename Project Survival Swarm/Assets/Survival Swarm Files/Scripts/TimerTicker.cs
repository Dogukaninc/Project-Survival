using System;
using UnityEngine;

namespace Survival_Swarm_Files.Scripts
{
    public class TimerTicker
    {
        public void WaveStartCountDown(ref float time, Action action, ref bool isTimerCounting)//TODO buraya waveler arası bool bir kontrolcü ile check edilen bir timer yaz
        {
            if (isTimerCounting)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    action?.Invoke();
                    isTimerCounting = false;
                    Debug.Log($"<color=cyan>Sayac bitti ve Wave başlıyor. Degerler ==> Timer:{time} Bool:{isTimerCounting}</color>");
                    return;
                }
            }
        }
    }
}