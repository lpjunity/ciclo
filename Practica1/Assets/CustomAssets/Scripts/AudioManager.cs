using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [SerializeField] private AudioClip _backgroundMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayEffect(AudioClip audioClip)
    {
        _effectsSource.PlayOneShot(audioClip);
    }

    public void PlayMusic()
    {
        _musicSource.clip = _backgroundMusic;
        _musicSource.loop = true;
        _musicSource.Play();
    }


}
