using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> balloonsInThisLevel = new List<GameObject>();

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

    public void UpdateBalloonInhisLevel(GameObject balloonToRemove)
    {
        balloonsInThisLevel.Remove(balloonToRemove);
    }
}
