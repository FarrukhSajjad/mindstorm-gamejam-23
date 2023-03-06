using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CameraFollow cameraFollow;

    public GameObject[] levels;
    public GameObject currentLevel;
    public int currentLevelToLoad;
    public GameObject playerToSpawnInLevel;
    public GameObject eggCollectionCompletedParticles;

    public GameObject blastPrefab;

    public GameObject confettiExplosion1;
    public GameObject confettiExplosion2;



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

        Debug.Log("Current Level To Load: " + currentLevelToLoad);

        if(currentLevelToLoad < levels.Length)
        {
            currentLevel = Instantiate(levels[currentLevelToLoad]);
        }
        else
        {
            UIManager.Instance.gamecompletedPanel.SetActive(true);
        }

    }

    public void OnLevelCompletedEvent()
    {
        currentLevelToLoad++;
        PlayerPrefs.SetInt(PlayerPrefsHelper.LevelToLoad, currentLevelToLoad++);
        Debug.Log("HH666");
    }

    public void OnLevelFailedEvent()
    {
        UIManager.Instance.levelFailedPanel.SetActive(true);
        UIManager.Instance.gameplayPanel.SetActive(false);
        AudioManager.instance.levelFailedSound.Play();
    }
}