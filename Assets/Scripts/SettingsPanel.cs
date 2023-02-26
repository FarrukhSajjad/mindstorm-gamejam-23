using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Button soundsButton;

    [SerializeField]
    private Button musicButton;

    [SerializeField]
    private Button vibrationButton;

    [SerializeField]
    private Button resetButton;

    [SerializeField]
    private GameObject soundsCheck, musicCheck, vibrationCheck;

    private void Start()
    {
        soundsButton.onClick.AddListener((OnSoundsButtonPressed));
        musicButton.onClick.AddListener((OnMusicButtonPressed));
        vibrationButton.onClick.AddListener((OnVibrationButtonPressed));
        resetButton.onClick.AddListener((OnResetButtonPressed));

        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Sound))
            PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 1);


        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Music))
            PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 1);


        if (!PlayerPrefs.HasKey(PlayerPrefsHelper.Vibration))
            PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration, 1);


        CheckPrefsStates(PlayerPrefs.GetInt(PlayerPrefsHelper.Sound), soundsCheck);
        CheckPrefsStates(PlayerPrefs.GetInt(PlayerPrefsHelper.Music), musicCheck);
        CheckPrefsStates(PlayerPrefs.GetInt(PlayerPrefsHelper.Vibration), vibrationCheck);

    }

    private void OnSoundsButtonPressed()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Sound) == 1)
        {
            //turn off sound
            PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 0);

            //deactivate the checkMark
            soundsCheck.SetActive(false);
        }
        else
        {
            //turn on sound
            PlayerPrefs.SetInt(PlayerPrefsHelper.Sound, 1);

            //activate the checkMark
            soundsCheck.SetActive(true);
        }
    }

    private void OnMusicButtonPressed()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Music) == 1)
        {
            //turn off sound
            PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 0);

            //deactivate the checkMark
            musicCheck.SetActive(false);
        }
        else
        {
            //turn on sound
            PlayerPrefs.SetInt(PlayerPrefsHelper.Music, 1);

            //activate the checkMark
            musicCheck.SetActive(true);
        }
    }

    private void OnVibrationButtonPressed()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsHelper.Vibration) == 1)
        {
            //turn off sound
            PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration, 0);

            //deactivate the checkMark
            vibrationCheck.SetActive(false);
        }
        else
        {
            //turn on sound
            PlayerPrefs.SetInt(PlayerPrefsHelper.Vibration, 1);

            //activate the checkMark
            vibrationCheck.SetActive(true);
        }
    }

    private void OnResetButtonPressed()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsHelper.Sound);
        PlayerPrefs.DeleteKey(PlayerPrefsHelper.Music);
        PlayerPrefs.DeleteKey(PlayerPrefsHelper.Vibration);

        soundsCheck.SetActive(true);
        musicCheck.SetActive(true);
        vibrationCheck.SetActive(true);
    }

    private void CheckPrefsStates(int stateToCheck, GameObject checkmarkToToggle)
    {
        if (stateToCheck == 0)
        {
            checkmarkToToggle.SetActive(false);
        }
        else
        {
            checkmarkToToggle.SetActive(true);
        }
    }
}
