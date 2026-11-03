using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [Header("___________AUDIO SOURCE___________")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("___________AUDIO CLIPS___________")]
    public AudioClip MainTheme;
    public AudioClip Death;
    public AudioClip Kill;
    public AudioClip Fireball;



    public static AudioManager Instance;

    public void PlayFireball()
    {
        SFXSource.clip = Fireball;
        SFXSource.Play();
    }

    public void MusicSound(float _volume)
    {
        MusicSource.volume = _volume;
        Saves.SaveNum("Music", MusicSource.volume);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        if (MusicSource != null)
        {
            MusicSource.volume = Saves.ChangeSound();
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MusicSource.clip = MainTheme;
            MusicSource.Play();
        }
    }
}
