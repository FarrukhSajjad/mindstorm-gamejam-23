using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Image> gridImages = new List<Image>();

    public Transform gridContent;

    public Transform eggGridContent;

    public GameObject levelCompletedPanel, levelFailedPanel, gamecompletedPanel, tutorialPanel, settingsPanel, bomberTutorialPanel, handGesture, gameplayPanel;

    public VariableJoystick variableJoystick;

    [Space(10)]
    [Header("Buttons")]
    [SerializeField]
    private Button nextLevelButton;


    public static UIManager Instance;

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

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            handGesture.SetActive(false);
        }
    }

    public void OnNextLevelButtonPressed()
    {
        AudioManager.instance.PlayClickSfx();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnRestartButtonPressed()
    {
        AudioManager.instance.PlayClickSfx();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnResetGameButtonPressed()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        AudioManager.instance.PlayClickSfx();
    }

    public void OnLetsGoButtonPressed()
    {
        //NewPlayerController.Instance.gameObject.GetComponent<NewPlayerController>().enabled = true;
        Level.Instance.isTutorialLevel = false;

        AudioManager.instance.PlayClickSfx();
    }

    public void OnSettingsButtonPressed()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;

        AudioManager.instance.PlayClickSfx();
    }

    public void OnSettingsClosedButtonPressed()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;

        AudioManager.instance.PlayClickSfx();
    }

}
