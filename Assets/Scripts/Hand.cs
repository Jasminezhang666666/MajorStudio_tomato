using UnityEngine;

public class Hand : MonoBehaviour
{
    public float minSpeed = 1f; // Minimum speed for downward movement
    public float maxSpeed = 3f; // Maximum speed for downward movement
    private float speed;
    private bool movingDown = true; // Keeps track of movement direction
    private HandManager handManager;
    private bool hasTouchedTable = false;

    private Rigidbody2D rb;

    public bool hasTomato = false; // Indicates if the hand is holding a tomato
    public GameObject freshTomatoPrefab; // FreshTomato prefab to spawn when hand touches the table

    public void Initialize(HandManager manager)
    {
        handManager = manager;
        speed = Random.Range(minSpeed, maxSpeed); // Assign random speed
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false; // Ensure it's not kinematic initially
    }

    void Update()
    {
        if (movingDown)
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else
        {
            rb.velocity = new Vector2(0, speed); // Move up
        }

        // Destroy the hand after it has touched the table and moved off the top of the screen
        if (hasTouchedTable && transform.position.y > 5f)
        {
            if (handManager != null)
            {
                handManager.RemoveHand(gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Table"))
        {
            movingDown = false; // Start moving upwards after touching the table
            hasTouchedTable = true; // The hand has touched the table

            // Ensure the Rigidbody2D is not kinematic when bouncing
            rb.isKinematic = false;

            if (!hasTomato)
            {
                // Spawn freshTomato as child when hand touches the table
                GameObject freshTomato = Instantiate(freshTomatoPrefab, transform.position, Quaternion.identity);
                freshTomato.transform.SetParent(transform);
                freshTomato.transform.localPosition = new Vector3(0, -0.5f, 0); // Adjust position if necessary

                hasTomato = true; // Hand now holds a tomato
            }

        }
    }

    // Method to add a RottenTomato as a child if it doesn't have one
    public bool AddTomato(GameObject tomato)
    {
        if (!hasTomato)
        {
            tomato.transform.SetParent(transform);
            tomato.transform.localPosition = new Vector3(0, -0.5f, 0); // Adjust position as needed
            hasTomato = true;
            return true; // Successfully added the tomato
        }
        return false; // Tomato was not added because the hand already has one
    }
}
