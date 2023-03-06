using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(DelayInLevelComplete), 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<NewPlayerController>().enabled = false;
            other.gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    private void DelayInLevelComplete()
    {
        LevelManager.Instance.OnLevelCompletedEvent();
        UIManager.Instance.levelCompletedPanel.SetActive(true);
    }
}