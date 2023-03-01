using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backGroundMusic;
    public AudioSource buttonClickSound;
    public AudioSource eggCollectSound;

    public static AudioManager instance;

    private void Awake()
    {
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
        PlayBGMusic();
    }

    public void PlayClickSfx()
    {
        if(PlayerPrefs.GetInt(PlayerPrefsHelper.Sound) == 1)
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
}
