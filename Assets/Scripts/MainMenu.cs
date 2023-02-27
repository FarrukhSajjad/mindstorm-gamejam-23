using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private GameObject settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        settingsButton.onClick.AddListener((OnSettingsButtonPressed));
    }

    private void OnSettingsButtonPressed()
    {
        settingsPanel.SetActive(true);
    }
}
