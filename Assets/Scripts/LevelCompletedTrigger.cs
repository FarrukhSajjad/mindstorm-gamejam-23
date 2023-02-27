using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.OnLevelCompletedEvent();
            UIManager.Instance.levelCompletedPanel.SetActive(true);
        }
    }
}