using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rottenTomatoPrefab; // The RottenTomato prefab to spawn
    private GameObject currentRottenTomato; // The RottenTomato currently following the mouse

    void Start()
    {
        SpawnRottenTomato();
    }

    void Update()
    {
        HandleRottenTomatoMovement();
        HandleRottenTomatoSpawning();
    }

    void HandleRottenTomatoMovement()
    {
        if (currentRottenTomato != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Make sure it's on the 2D plane

            // Check if the RottenTomato is still following the mouse
            if (currentRottenTomato.GetComponent<RottenTomato>().IsFollowingMouse)
            {
                currentRottenTomato.transform.position = mousePosition;
            }
        }
    }

    void HandleRottenTomatoSpawning()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            if (currentRottenTomato != null)
            {
                Destroy(currentRottenTomato);
            }
            SpawnRottenTomato();
        }
    }

    void SpawnRottenTomato()
    {
        currentRottenTomato = Instantiate(rottenTomatoPrefab);
        // Ensure the RottenTomato starts following the mouse
        currentRottenTomato.GetComponent<RottenTomato>().StartFollowingMouse();
    }

    public void StopMouseFollowForTomato(RottenTomato tomato)
    {
        if (currentRottenTomato == tomato)
        {
            currentRottenTomato = null; // Remove the reference to stop updating its position
        }
    }
}
