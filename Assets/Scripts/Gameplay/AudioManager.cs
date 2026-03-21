using UnityEngine;

// ReSharper disable InconsistentNaming
public class AudioManager : MonoBehaviour {
    [Header("Volume settings")]
    [SerializeField, Range(0, 1)] private float musicVolume = 0.8f;
    [SerializeField, Range(0, 1)] private float soundVolume = 0.8f;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Musics")]
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public AudioClip gameOverMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip[] shootSounds;
    [SerializeField] private AudioClip[] bigShootSounds;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip upgradeSound;
    [SerializeField] private AudioClip loseLifeSound;

    public static AudioManager Instance { get; private set; }

    void Awake() {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (musicSource) musicSource.volume = musicVolume;
        if (sfxSource) sfxSource.volume = soundVolume;
    }

    void Start() {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip) {
        if (!clip) return;
        StopMusic();
        musicSource.loop = true;
        musicSource.clip = clip;
        musicSource.Play();
    }

    private void PlaySFX(AudioClip clip, float volume=1f) {
        if (clip) sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayRandomShoot() {
        int index = Random.Range(0, shootSounds.Length);
        PlaySFX(shootSounds[index], 0.7f);
    }

    public void PlayRandomBigShoot() {
        int index = Random.Range(0, bigShootSounds.Length);
        PlaySFX(bigShootSounds[index], 0.8f);
    }

    public void PlayExplosion() {
        sfxSource.pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(explosionSound, 0.9f);
        sfxSource.pitch = 1;
    }

    public void PlaySuperForm() {
        PlaySFX(upgradeSound, 0.9f);
    }

    public void PlayLoseLife() {
        PlaySFX(loseLifeSound, 0.9f);
    }

    private void StopMusic() => musicSource.Stop();
        
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
}
