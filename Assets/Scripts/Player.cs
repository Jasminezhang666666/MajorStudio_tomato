using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rottenTomatoPrefab;
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
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) // RIGHT/LEFT mouse button clicked
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
        // new tomato, follow the mouse
        currentRottenTomato = Instantiate(rottenTomatoPrefab);
        GrabbingTomato = true; 
        currentRottenTomato.GetComponent<RottenTomato>().StartFollowingMouse();
    }

    // Called by the RottenTomato when it collides with a hand
    public void StopMouseFollow()
    {
        GrabbingTomato = false; //the tomato is attached to the hand
        currentRottenTomato = null;
    }
}
