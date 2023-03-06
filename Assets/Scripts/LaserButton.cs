using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButton : MonoBehaviour
{
    public Animation activateableAnim;

    public bool justOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (justOnce) return;
        if (other.gameObject.tag != "Player") return;

        Debug.Log(other.gameObject.name, other.gameObject);

        Debug.Log("HH1");

        activateableAnim.Play();

        AudioManager.instance.PlayGateOpenSound();

        justOnce = true;
    }
}
