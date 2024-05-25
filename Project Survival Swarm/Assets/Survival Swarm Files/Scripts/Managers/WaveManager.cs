using System;
using Survival_Swarm_Files.Scripts;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //TODO tüm timerları bir pool'a koy işin bitiyorsa bir timerla o timer'ı pool'a gönder.
    // Herhangi bir timer'a ihtiyacın varsa pool'dan çek

    private readonly TimerTicker waveCountDownController = new TimerTicker();

    [SerializeField] private TextMeshProUGUI waveCountDownText;
    [SerializeField] private TextMeshProUGUI waveStateText;

    [Header(" Wave Manager Settings ")] [SerializeField]
    private float timerValue;

    private float defaultWaveTimerValue;

    private bool canWaveStart;
    private Action setWaveStart;

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
        timerValue = defaultWaveTimerValue;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timerValue / 60);
        int seconds = Mathf.FloorToInt(timerValue % 60);
        waveCountDownText.text = $"{minutes:00}:{seconds:00}";
    }
}