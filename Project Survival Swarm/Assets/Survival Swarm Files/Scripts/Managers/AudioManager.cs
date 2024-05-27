using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource PlayeraudioSource;
    public AudioSource EnemyaudioSource;
    public AudioSource FoodstepaudioSource;
    public AudioSource musicSource;
    public AudioSource enemySource;
    
    public AudioClip backgroundMusic;
    public AudioClip EnemyMusicClip;
    public AudioClip footStepsClip;
    public AudioClip announcementClip;

    public AudioClip machineGunSound;
    public AudioClip pistolSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void PlaySound(AudioClip clip)
    {
        if (PlayeraudioSource != null)
        {
            PlayeraudioSource.clip = clip;
            PlayeraudioSource.Play();
        }
    }
    
    public void PlayFootStepsSound(AudioClip clip)
    {
        if (FoodstepaudioSource != null)
        {
            FoodstepaudioSource.clip = clip;
            FoodstepaudioSource.Play();
        }
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayAnnouncement()
    {
        if (musicSource != null && announcementClip != null)
        {
            musicSource.clip = announcementClip;
            musicSource.Play();
        }
    }

    public void PlayMachineGunSound()
    {
        PlaySound(machineGunSound);
        //AudioManager.Instance.PlayMachineGunSound();
    }

    public void PlayPistolSound()
    {
        PlaySound(pistolSound);
    }
    
    public void PlayFootStepsSound()
    {
        PlayFootStepsSound(footStepsClip);
    }
}
