using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject humanHandPrefab; // Prefab of the human hand
    public GameObject alienHandPrefab; // Prefab of the alien hand
    public Transform spawnArea; // The area where hands will be spawned
    public float spawnRate = 1f; // Time between spawns
    public int maxHands = 5; // Maximum number of hands on screen
    public float horizontalBuffer = 1f; // Minimum horizontal distance between hands

    private List<GameObject> activeHands = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnHands());
    }

    IEnumerator SpawnHands()
    {
        while (true)
        {
            if (activeHands.Count < maxHands)
            {
                Vector3 spawnPosition = Vector3.zero; // Ensure spawnPosition is initialized
                bool positionFound = false;

                // Trying to find a valid spawn position
                for (int attempts = 0; attempts < 10; attempts++)
                {
                    spawnPosition = new Vector3(
                        Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                        Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2),
                        0
                    );

                    if (!IsOverlappingHorizontally(spawnPosition))
                    {
                        positionFound = true;
                        break;
                    }
                }

                // If a valid position is found, instantiate a hand
                if (positionFound)
                {
                    GameObject handPrefab = Random.Range(0f, 1f) < 0.5f ? humanHandPrefab : alienHandPrefab;
                    GameObject newHand = Instantiate(handPrefab, spawnPosition, Quaternion.identity);
                    newHand.GetComponent<Hand>().Initialize(this);
                    activeHands.Add(newHand);
                }
            }

            yield return new WaitForSeconds(spawnRate); // Wait before spawning the next hand
        }
    }

    private bool IsOverlappingHorizontally(Vector3 spawnPosition)
    {
        // Check if there are any other hands in horizontal proximity
        foreach (GameObject hand in activeHands)
        {
            float distance = Mathf.Abs(hand.transform.position.x - spawnPosition.x);

            // Check if hands are overlapping horizontally
            if (distance < horizontalBuffer)
            {
                return true;
            }
        }

        return false; // No horizontal overlapping hands found
    }

    public void RemoveHand(GameObject hand)
    {
        if (activeHands.Contains(hand))
        {
            activeHands.Remove(hand);
        }
    }
}
