using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButton : MonoBehaviour
{
    public Animation activateableAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        Debug.Log(other.gameObject.name, other.gameObject);

        Debug.Log("HH1");

        activateableAnim.Play();

        AudioManager.instance.PlayGateOpenSound();
    }
}
