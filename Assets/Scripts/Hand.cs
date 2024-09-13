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

    public void Initialize(HandManager manager)
    {
        handManager = manager;
        speed = Random.Range(minSpeed, maxSpeed); // Assign random speed
        rb = GetComponent<Rigidbody2D>();
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
            hasTouchedTable = true; // the hand has touched the table
        }
    }
}
