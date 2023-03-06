using UnityEngine;

public class ElectricityButton : MonoBehaviour
{
    public GameObject electricFieldToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PatrolOfficer>() != null)
        {
            electricFieldToEnable.SetActive(true);
            AudioManager.instance.lightingSound.Play();

        }


        if(other.gameObject.GetComponent<NewPlayerController>() != null)
        {
            electricFieldToEnable.SetActive(true);
            AudioManager.instance.lightingSound.Play();
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PatrolOfficer>() != null)
        {
            electricFieldToEnable.SetActive(false);
            AudioManager.instance.lightingSound.Stop();
        }


        if (other.gameObject.GetComponent<NewPlayerController>() != null)
        {
            electricFieldToEnable.SetActive(false);
            AudioManager.instance.lightingSound.Stop();
        }

    }
}
