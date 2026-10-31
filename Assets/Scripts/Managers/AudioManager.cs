using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip BGM")]
    public AudioClip[] BGMs;
    private int _lastBGMIndex;

    [Header("Audio Clip SFX")]
    public AudioClip CirclePop;
    public AudioClip GoldCirclePop;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlayRandomBGM();
        }
    }

    private void PlayRandomBGM()
    {
        Debug.Log("Random BGM Go");
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, BGMs.Length);
        }
        while (randomIndex == _lastBGMIndex && BGMs.Length > 1);

        _lastBGMIndex = randomIndex;

        musicSource.clip = BGMs[randomIndex];
        musicSource.Play();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(sfx);
    }
}
