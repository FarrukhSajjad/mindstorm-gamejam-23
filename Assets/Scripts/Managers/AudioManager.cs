using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backGroundMusic;
    public AudioSource buttonClickSound;
    public AudioSource eggCollectSound;
    public AudioSource gateOpenSound;
    public AudioSource playerDeathSound;
    public AudioSource eggCollectionComplete;

    public static AudioManager instance;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Sound))
            PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 1);


        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Music))
            PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 1);


        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Vibration))
            PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration, 1);


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }

       
    }

    private void Start()
    {
        Invoke(nameof(PlayBGMusic), 0.1f);
    }

    public void PlayClickSfx()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Sound) == 1)
        {
            buttonClickSound.Play();
        }
    }

    public void PlayBGMusic()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Music) == 1)
        {
            backGroundMusic.Play();
        }

    }

    public void PlayEggCollectSound()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Sound) == 1)
        {
            eggCollectSound.Play();
        }
    }

    public void PlayGateOpenSound()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Sound) == 1)
        {
            gateOpenSound.Play();
        }
    }
}
