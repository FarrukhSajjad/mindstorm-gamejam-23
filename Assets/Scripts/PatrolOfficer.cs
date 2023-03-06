using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolOfficer : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5f;
    public float detectionRadius = 5f;
    public Material detectionMaterial;

    private Vector3 nextPosition;
    private bool movingToEnd = true;
    private GameObject player;
    private GameObject detectionSphere;

    private bool enableBlast = false;

    void Start()
    {
        nextPosition = endPoint.position;

        // Create a sphere to represent the detection radius
        detectionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        detectionSphere.transform.localScale = new Vector3(detectionRadius * 2f, detectionRadius * 2f, detectionRadius * 2f);
        detectionSphere.transform.position = transform.position;
        detectionSphere.GetComponent<MeshRenderer>().material = detectionMaterial;
        detectionSphere.GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if (player == null)
        {
            // If the player hasn't been found yet, try to find it
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
        {
            // If the player is within the detection radius, move towards the player
            nextPosition = player.transform.position;
            movingToEnd = false;
            if (!enableBlast)
            {
                Debug.Log("Blast here");
                enableBlast = true;
                Instantiate(LevelManager.Instance.blastPrefab, this.gameObject.transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
                player.gameObject.SetActive(false);

                Invoke(nameof(InvokeLevelFailed), 1f);
            }
        }
        else
        {
            // Otherwise, continue patrolling between the two points
            if (movingToEnd)
            {
                nextPosition = endPoint.position;
            }
            else
            {
                nextPosition = startPoint.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            if (transform.position == nextPosition)
            {
                movingToEnd = !movingToEnd;
            }
        }

        transform.LookAt(nextPosition);

        // Update the position of the detection sphere
        detectionSphere.transform.position = transform.position;
    }

    void OnDestroy()
    {
        // Destroy the detection sphere when the script is destroyed
        Destroy(detectionSphere);
    }

    private void InvokeLevelFailed()
    {
        UIManager.Instance.levelFailedPanel.SetActive(true);
        UIManager.Instance.gameplayPanel.SetActive(false);
    }
}
