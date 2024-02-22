using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _effectTakeMushroom;
    [SerializeField] private AudioClip _effectJump;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this )
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _musicSource.loop = true;
        _musicSource.clip = _backgroundMusic;
        _musicSource.playOnAwake = false;
        _effectsSource.playOnAwake = false;

        PlayBackgroundMusic(_backgroundMusic);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlayMushroomPickup()
    {
        PlayEffect(_effectTakeMushroom);
    }

    public void PlayJump()
    {
        PlayEffect(_effectJump);
    }

    public void PlayEffect(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
