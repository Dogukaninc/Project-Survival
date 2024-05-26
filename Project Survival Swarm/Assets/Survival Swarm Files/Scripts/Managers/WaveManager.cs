using System;
using System.Collections.Generic;
using Survival_Swarm_Files.Scripts;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    private readonly TimerTicker waveCountDownController = new TimerTicker();

    [SerializeField] private TextMeshProUGUI waveCountDownText;
    [SerializeField] private TextMeshProUGUI waveStateText;

    [Header(" Wave Manager Settings ")] [SerializeField]
    private float timerValue;
    private float defaultWaveTimerValue;

    private bool canWaveStart;
    private Action setWaveStart;

    public List<EnemySpawner> EnemySpawners = new List<EnemySpawner>();
    
    private void OnEnable()
    {
        setWaveStart += StartWave;
    }

    private void OnDisable()
    {
        setWaveStart -= StartWave;
    }

    void Start()
    {
        defaultWaveTimerValue = timerValue;
        UpdateTimerText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            canWaveStart = true;
        }

        if (canWaveStart)
        {
            waveCountDownController.WaveStartCountDown(ref timerValue, setWaveStart, ref canWaveStart);
            UpdateTimerText();
        }

        WaveStateTextSetter();
    }

    private void WaveStateTextSetter()
    {
        if (canWaveStart)
        {
            waveStateText.text = "Be Ready !!!";
        }
        else
        {
            waveStateText.text = "Let Them Come !!! (Press: H )";
        }
    }

    private void StartWave()
    {
        Debug.Log("WAVE BAŞLADI");
        StartSpawners();
        timerValue = defaultWaveTimerValue;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timerValue / 60);
        int seconds = Mathf.FloorToInt(timerValue % 60);
        waveCountDownText.text = $"{minutes:00}:{seconds:00}";
    }

    private void StartSpawners()
    {
        foreach (var spawner in EnemySpawners)
        {
            spawner.startSpawner?.Invoke();;
        }
        
    }
    
}