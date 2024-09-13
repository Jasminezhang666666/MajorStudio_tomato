using UnityEngine;

public class RottenTomato : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public bool IsFollowingMouse { get; private set; } = true;

    // Offset to position the RottenTomato downward relative to the Hand
    public Vector3 positionOffset = new Vector3(0, -0.5f, 0); // Adjust the -0.5f to your desired value

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void StartFollowingMouse()
    {
        IsFollowingMouse = true;
    }

    public void StopFollowingMouse()
    {
        IsFollowingMouse = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log($"Trigger detected with Hand: {other.gameObject.name}");

            // Make this RottenTomato a child of the Hand
            Transform handTransform = other.transform;
            transform.SetParent(handTransform);

            // Set position relative to the Hand to maintain current position
            transform.localPosition = positionOffset; // Apply the offset
            Debug.Log("RottenTomato is now a child of Hand and positioned at: " + transform.localPosition);

            // Stop following the mouse
            StopFollowingMouse();

            // Notify Player to stop updating this Tomato
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.StopMouseFollowForTomato(this);
            }

            StopMouseFollow();
        }
    }

    private void StopMouseFollow()
    {
        // Disable the Rigidbody2D to stop movement
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Ensure no movement
            rb.isKinematic = true; // Make it not affected by physics
            Debug.Log("Rigidbody2D properties adjusted.");
        }

        // Optionally disable the collider if further collisions are not needed
        if (col != null)
        {
            col.enabled = false;
            Debug.Log("Collider2D disabled.");
        }
    }
}
