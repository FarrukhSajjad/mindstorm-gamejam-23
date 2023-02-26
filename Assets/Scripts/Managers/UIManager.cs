using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Image> gridImages = new List<Image>();

    public Transform gridContent;


    // Create a new list to store the removed game objects
    public List<Image> removedObjects = new List<Image>();


    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
}
