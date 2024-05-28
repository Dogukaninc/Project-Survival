using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(-3f, 3f)] public float pitch = 1f;
        public bool loop;
        public bool playAwake;
    }

    public List<Sound> sounds;

    private Dictionary<string, AudioSource> soundDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        soundDictionary = new Dictionary<string, AudioSource>();

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;
            source.playOnAwake = s.playAwake;
            soundDictionary[s.name] = source;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            AudioSource source = soundDictionary[soundName];
            if (source.isPlaying)
            {
                source.Stop(); // Ayn� sesi tekrar �almak i�in �nce durdur
            }
            source.Play();
        }
        else
        {
            Debug.Log("Ses: " + soundName + " bulunamad�!");
        }
    }

    public void Stop(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            soundDictionary[soundName].Stop();
        }
        else
        {
            Debug.Log("Ses: " + soundName + " bulunamad�!");
        }
    }

    public void SetVolume(string soundName, float volume)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            soundDictionary[soundName].volume = volume;
        }
        else
        {
            Debug.Log("Ses: " + soundName + " bulunamad�!");
        }
    }
    public void SetPitch(string soundName, float pitch)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            soundDictionary[soundName].pitch = pitch;
        }
        else
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
        }
    }
}
