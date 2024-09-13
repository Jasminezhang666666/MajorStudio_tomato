using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rottenTomatoPrefab; // The RottenTomato prefab to spawn
    private GameObject currentRottenTomato; // The RottenTomato currently following the mouse
    public bool GrabbingTomato = false; // Whether the player is currently grabbing a tomato

    void Start()
    {
        SpawnRottenTomato();
    }

    void Update()
    {
        RottenTomatoMovement();
        RottenTomatoSpawning();
    }

    void RottenTomatoMovement()
    {
        if (GrabbingTomato && currentRottenTomato != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Move the current RottenTomato to the mouse position
            currentRottenTomato.transform.position = mousePosition;
        }
    }

    void RottenTomatoSpawning()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            // Only spawn a new RottenTomato if we're not already grabbing one
            if (!GrabbingTomato)
            {
                SpawnRottenTomato();
            }
        }
    }

    void SpawnRottenTomato()
    {
        // Spawn a new tomato and set it to follow the mouse
        currentRottenTomato = Instantiate(rottenTomatoPrefab);
        GrabbingTomato = true; // Set GrabbingTomato to true since we're grabbing a new tomato
        currentRottenTomato.GetComponent<RottenTomato>().StartFollowingMouse();
    }

    // Called by the RottenTomato when it collides with a hand
    public void StopMouseFollow()
    {
        GrabbingTomato = false; // Reset GrabbingTomato to false once the tomato is attached to the hand
        currentRottenTomato = null; // Clear reference to the current tomato
    }
}
