using System;
using System.Collections;
using System.Collections.Generic;
using Survival_Swarm_Files.Scripts;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private TimerTicker timerTicker;

    [SerializeField] private TextMeshProUGUI waveCountDownText;
    [SerializeField] private TextMeshProUGUI waveStartedText;

    [Header(" Wave Manager Settings ")] [SerializeField]
    private float timerValue;

    private Action onWaveStarted;

    private void OnEnable()
    {
        onWaveStarted += StartWave;
    }

    private void OnDisable()
    {
        onWaveStarted -= StartWave;
    }

    void Start()
    {
        timerTicker = new TimerTicker();

    }

    void Update()
    {
        timerTicker.CustomCountdown(timerValue, timerValue, onWaveStarted);
        waveCountDownText.text = timerValue.ToString();

    }

    private void StartWave()
    {
        //waveStartedText.text = text;
        Debug.Log("Asdasdsa yarraaa");
    }
}