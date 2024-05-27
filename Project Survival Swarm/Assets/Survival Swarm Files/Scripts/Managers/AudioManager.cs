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
        public int poolSize = 5;
        public bool loop;
        [HideInInspector]
        public Queue<AudioSource> audioSources = new Queue<AudioSource>();
    }

    public Sound[] sounds;
    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    public AudioSource announcementSource;
    public AudioClip announcementClip;

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

        InitializeSoundPool();
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void InitializeSoundPool()
    {
        foreach (var sound in sounds)
        {
            soundDictionary[sound.name] = sound;
            for (int i = 0; i < sound.poolSize; i++)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = sound.clip;
                audioSource.loop = sound.loop;
                sound.audioSources.Enqueue(audioSource);
            }
        }
    }

    public void PlaySound(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            Sound sound = soundDictionary[name];
            if (sound.audioSources.Count > 0)
            {
                AudioSource audioSource = sound.audioSources.Dequeue();
                audioSource.Play();
                StartCoroutine(ReturnToPoolAfterPlaying(audioSource, sound));
            }
        }
    }

    private IEnumerator ReturnToPoolAfterPlaying(AudioSource source, Sound sound)
    {
        yield return new WaitWhile(() => source.isPlaying);
        sound.audioSources.Enqueue(source);
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
        if (announcementSource != null && announcementClip != null)
        {
            announcementSource.clip = announcementClip;
            announcementSource.Play();
        }
    }
}
