using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rottenTomatoPrefab; // The RottenTomato prefab to spawn
    private GameObject currentRottenTomato; // The RottenTomato currently following the mouse
    public bool GrabbingTomato { get; private set; } = false; // Boolean to track if a RottenTomato is following the mouse

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
        if (currentRottenTomato != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Check if the RottenTomato is still following the mouse
            if (currentRottenTomato.GetComponent<RottenTomato>().IsFollowingMouse)
            {
                currentRottenTomato.transform.position = mousePosition;
            }
        }
    }

    void RottenTomatoSpawning()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            if (currentRottenTomato != null)
            {
                Destroy(currentRottenTomato);
                GrabbingTomato = false; // Update the boolean when the current RottenTomato is destroyed
            }
            SpawnRottenTomato();
        }
    }

    void SpawnRottenTomato()
    {
        currentRottenTomato = Instantiate(rottenTomatoPrefab);
        // Ensure the RottenTomato starts following the mouse
        currentRottenTomato.GetComponent<RottenTomato>().StartFollowingMouse();
        GrabbingTomato = true; // Update the boolean when a new RottenTomato starts following the mouse
    }

    public void StopMouseFollowForTomato(RottenTomato tomato)
    {
        if (currentRottenTomato == tomato)
        {
            currentRottenTomato = null; // Remove the reference to stop updating its position
            GrabbingTomato = false; // Update the boolean when the RottenTomato stops following the mouse
        }
    }
}
