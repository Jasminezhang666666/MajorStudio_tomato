using UnityEngine;

public class RottenTomato : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public bool IsFollowingMouse { get; private set; } = true;

    // Offset to position the RottenTomato downward relative to the Hand
    public Vector3 positionOffset = new Vector3(0, -0.5f, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void StartFollowingMouse()
    {
        IsFollowingMouse = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log($"Trigger detected with Hand: {other.gameObject.name}");

            // Attach the RottenTomato to the hand
            Transform handTransform = other.transform;
            transform.SetParent(handTransform);

            // Set position relative to the Hand
            transform.localPosition = positionOffset;
            Debug.Log("RottenTomato is now a child of Hand and positioned at: " + transform.localPosition);

            // Stop following the mouse
            StopFollowingMouse();

            // Notify Player to stop following this tomato
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.StopMouseFollow(); // Tell the player script to reset GrabbingTomato
            }
        }
    }

    private void StopFollowingMouse()
    {
        IsFollowingMouse = false;

        // Disable physics and collider once attached to the hand
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Ensure no movement
            rb.isKinematic = true; // Make it not affected by physics
            Debug.Log("Rigidbody2D properties adjusted.");
        }

        if (col != null)
        {
            col.enabled = false; // Disable further collisions
            Debug.Log("Collider2D disabled.");
        }
    }
}
