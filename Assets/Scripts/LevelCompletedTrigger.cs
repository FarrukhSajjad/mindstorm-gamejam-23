using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedTrigger : MonoBehaviour
{
    public bool onlyOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (onlyOnce) return;
            LevelManager.Instance.confettiExplosion1.SetActive(true);
            LevelManager.Instance.confettiExplosion2.SetActive(true);
            AudioManager.instance.levelCompleteSound.Play();
            Invoke(nameof(DelayInLevelComplete), 1f);
            Debug.Log("HH111");
            onlyOnce = true;
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
        UIManager.Instance.gameplayPanel.SetActive(false);
        onlyOnce = true;
    }
}