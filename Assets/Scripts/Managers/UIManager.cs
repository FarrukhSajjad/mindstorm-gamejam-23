using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Image> gridImages = new List<Image>();

    public Transform gridContent;

    public GameObject controllerPanel;

    public Transform eggGridContent;

    public GameObject levelCompletedPanel, levelFailedPanel, gamecompletedPanel;

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
        if(Input.touchCount > 0)
        {
            controllerPanel.SetActive(false);
        }
    }

    public void OnNextLevelButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnResetGameButtonPressed()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
