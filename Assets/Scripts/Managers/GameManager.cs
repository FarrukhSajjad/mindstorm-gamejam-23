using System.Collections.Generic;
using UnityEngine;

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

        Application.targetFrameRate = 120;
    }

    private int startIndex;

    public void TestIfThereAreThreeSameObjectsConsecutively()
    {
        // Assuming your list of GameObjects is called 'GameManager.Instance.inventory'
        List<GameObject> gameObjects = GameManager.Instance.inventory;

        int consecutiveCount = 1;
        startIndex = -1;
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

            // Move the remaining GameObjects to the start of the list
            for (int i = startIndex; i < gameObjects.Count - 2; i++)
            {
                gameObjects[i] = gameObjects[i + 3];
            }

            Invoke(nameof(RemoveItemsFromInventory), 1f);

            if (Level.Instance.balloonsInThisLevel.Count == 0 && inventory.Count == 0)
            {
                Level.Instance.activateableAnim.Play();

                AudioManager.instance.PlayGateOpenSound();
            }
        }

        if (Level.Instance.balloonsInThisLevel.Count > 0 && inventory.Count == 5)
        {
            UIManager.Instance.levelFailedPanel.SetActive(true);
        }

    }

    private void RemoveItemsFromInventory()
    {
        for (int i = startIndex; i < startIndex + 3; i++)
        {
            Destroy(UIManager.Instance.eggGridContent.transform.GetChild(i).GetChild(1).gameObject);
        }

    }

}
