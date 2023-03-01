using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public bool isTutorialLevel;
    public bool isBomberTutorial;

    public List<GameObject> balloonsInThisLevel = new List<GameObject>();

    public Animation activateableAnim;

    public Transform playerSpawnPoint;

    public static Level Instance;

    private void Awake()
    {
        if(Instance == null)
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
        Instantiate(LevelManager.Instance.playerToSpawnInLevel, playerSpawnPoint.position, Quaternion.identity);
        //LevelManager.Instance.cameraFollow.enabled = true;
        //Invoke(nameof(SetChaseSpeed), 1f);
        

        if (this.isTutorialLevel)
        {
            UIManager.Instance.tutorialPanel.SetActive(true);
        }

        if(this.isBomberTutorial)
        {
            UIManager.Instance.bomberTutorialPanel.SetActive(true);
        }
    }

    public void UpdateBalloonInhisLevel(GameObject balloonToRemove)
    {
        balloonsInThisLevel.Remove(balloonToRemove);
    }

    private void SetChaseSpeed()
    {
        //LevelManager.Instance.cameraFollow.chaseSpeed = 0.5f;
    }
}
