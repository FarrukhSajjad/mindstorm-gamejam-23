using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectricityButton : MonoBehaviour
{
    public GameObject electricFieldToEnable;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HH@", this.gameObject);
        if (other.gameObject.GetComponent<PatrolOfficer>() == null) return;
        electricFieldToEnable.SetActive(true);
        AudioManager.instance.lightingSound.Play();
        Debug.Log("DDDD");
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("HH@");

        if (other.gameObject.GetComponent<PatrolOfficer>() == null) return;
        electricFieldToEnable.SetActive(false);
        AudioManager.instance.lightingSound.Stop();
        Debug.Log("DD2DD");
    }
}
