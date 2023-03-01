using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButton : MonoBehaviour
{
    public Animation activateableAnim;

    private void OnTriggerEnter(Collider other)
    {
        activateableAnim.Play();
    }
}
