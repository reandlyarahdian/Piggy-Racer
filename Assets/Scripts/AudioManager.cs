using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public List<AudioClip> backgroundMusic;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDict;
    private Dictionary<string, AudioClip> bgmDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("BGM");
    }

    void InitializeDictionary()
    {
        sfxDict = new Dictionary<string, AudioClip>();
        bgmDict = new Dictionary<string, AudioClip>();
        foreach (var clip in backgroundMusic)
        {
            if (!bgmDict.ContainsKey(clip.name))
            {
                bgmDict.Add(clip.name, clip);
            }
        }
        foreach (var clip in sfxClips)
        {
            if (!sfxDict.ContainsKey(clip.name))
            {
                sfxDict.Add(clip.name, clip);
            }
        }
    }

    public void PlayMusic(string clipName)
    {
        if (musicSource && bgmDict.TryGetValue(clipName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string clipName)
    {
        if (sfxDict.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + clipName);
        }
    }

    [Header("Mixer")]
    public AudioMixer audioMixer;

    public void SetBGMVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void ResetVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(1) * 20);
        audioMixer.SetFloat("BGM", Mathf.Log10(1) * 20);
    }
}
