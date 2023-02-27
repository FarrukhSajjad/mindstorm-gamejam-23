using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CameraFollow cameraFollow;

    public GameObject[] levels;
    public GameObject currentLevel;
    public int currentLevelToLoad;
    public GameObject playerToSpawnInLevel;


    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey(PlayerPrefsHelper.LevelToLoad))
            PlayerPrefs.SetInt(PlayerPrefsHelper.LevelToLoad, 0);


        InstantiateLevel();
    }

    public void InstantiateLevel()
    {
        currentLevelToLoad = PlayerPrefs.GetInt(PlayerPrefsHelper.LevelToLoad);

        if(currentLevelToLoad < levels.Length)
        {
            currentLevel = Instantiate(levels[currentLevelToLoad]);
        }
        else
        {
            Debug.Log("Game Completed");
            UIManager.Instance.gamecompletedPanel.SetActive(true);
        }

    }

    public void OnLevelCompletedEvent()
    {
        currentLevelToLoad++;
        Debug.Log("Next level : " + currentLevelToLoad);
        PlayerPrefs.SetInt(PlayerPrefsHelper.LevelToLoad, currentLevelToLoad++);
    }
}
