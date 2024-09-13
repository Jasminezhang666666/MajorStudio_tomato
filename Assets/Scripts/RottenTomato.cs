using UnityEngine;

public class RottenTomato : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public bool IsFollowingMouse { get; private set; } = true;

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
            Hand hand = other.GetComponent<Hand>();

            // Check if the hand already has a tomato
            if (hand != null && !hand.hasTomato)
            {
                Debug.Log($"Trigger detected with Hand: {other.gameObject.name}");

                // Try to add the tomato to the hand
                if (hand.AddTomato(gameObject))
                {
                    // Stop following the mouse
                    StopFollowingMouse();

                    // Notify the player to stop following this tomato
                    Player player = FindObjectOfType<Player>();
                    if (player != null)
                    {
                        player.StopMouseFollow();
                    }
                }
            }
        }
    }

    private void StopFollowingMouse()
    {
        IsFollowingMouse = false;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        if (col != null)
        {
            col.enabled = false;
        }
    }
}
