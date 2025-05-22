using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    public AudioSource music;
    public AudioSource sound;

    public AudioClip musicClip;
    public AudioClip SoundClip;
    public AudioClip winnClip;

    private int musicInt;
    private int soundInt;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        if (musicClip != null) {
            music.clip = musicClip;
            sound.clip = SoundClip;
        }

        sound.volume = PlayerPrefs.GetFloat("S", 1);
        music.volume = PlayerPrefs.GetFloat("M", 1);

        if (PlayerPrefs.HasKey("M1")) {
            if (PlayerPrefs.GetInt("M1") == 1) {
                music.Play();
            }
            else {
                sound.Play();
            }
        }
        else {
            music.Play();
        }
    }

    public void SetMusic(float value) {
        sound.Stop();
        if (!music.isPlaying)
            music.Play();
        PlayerPrefs.SetFloat("M", value);
        PlayerPrefs.SetFloat("M1", 1);
        PlayerPrefs.SetFloat("S1", 0);
        music.volume = value;
    }

    public void SetSound(float value) {
        music.Stop();
        if (!sound.isPlaying) {
            sound.Play();
        }
        PlayerPrefs.SetFloat("S", value);
        PlayerPrefs.SetFloat("M1", 0);
        PlayerPrefs.SetFloat("S1", 1);
        sound.volume = value;
    }
}
