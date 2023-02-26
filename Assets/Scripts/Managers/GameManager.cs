using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>(5);

    public static GameManager Instance;

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
    public void TestIfThereAreThreeSameObjectsConsecutively()
    {
        // Assuming your list of GameObjects is called 'GameManager.Instance.inventory'
        List<GameObject> gameObjects = GameManager.Instance.inventory;

        int consecutiveCount = 1;
        int startIndex = -1;
        for (int i = 1; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].tag == gameObjects[i - 1].tag)
            {
                consecutiveCount++;
                if (consecutiveCount == 3)
                {
                    startIndex = i - 2;
                    break;
                }
            }
            else
            {
                consecutiveCount = 1;
            }
        }

        if (startIndex != -1)
        {
            
            // Remove the three consecutive GameObjects
            gameObjects.RemoveRange(startIndex, 3);

            //Remove the consecutive Images
            UIManager.Instance.gridImages.RemoveRange(startIndex, 3);

            // Remove the Images from the scene
            for (int i = startIndex; i < startIndex + 3; i++)
            {
                Destroy(UIManager.Instance.gridContent.transform.GetChild(i).gameObject);
            }



            // Move the remaining GameObjects to the start of the list
            for (int i = startIndex; i < gameObjects.Count - 2; i++)
            {
                gameObjects[i] = gameObjects[i + 3];
                UIManager.Instance.gridImages[i] = UIManager.Instance.gridImages[i + 3];
            }

            // Remove the last three GameObjects from the list
            //gameObjects.RemoveRange(gameObjects.Count - 3, 3);

            Debug.Log("Three consecutive matching objects found and removed!");

            if (Level.Instance.balloonsInThisLevel.Count == 0 && inventory.Count == 0)
            {
                Debug.Log("Level Passed");
                Level.Instance.activateableAnim.Play();
            }
        }

        if (Level.Instance.balloonsInThisLevel.Count > 0 && inventory.Count == 5)
        {
            Debug.Log("LEVEL FAILED");
        }



    }

}
