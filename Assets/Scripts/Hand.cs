using UnityEngine;

public class Hand : MonoBehaviour
{
    public float minSpeed = 1f; // Min speed for downward movement
    public float maxSpeed = 3f; // Max speed for downward movement
    private float speed;
    private bool movingDown = true;
    private HandManager handManager;
    private bool hasTouchedTable = false;
    private ScoreManager scoreManager;

    private Rigidbody2D rb;

    public bool hasTomato = false; // if the hand is holding a tomato
    public GameObject freshTomatoPrefab;

    // Audio references for good and bad sounds
    public AudioClip goodSound;
    public AudioClip badSound;
    private AudioSource audioSource;

    public void Initialize(HandManager manager)
    {
        handManager = manager;
        speed = Random.Range(minSpeed, maxSpeed);
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if it doesn't exist
        }
    }

    void Update()
    {
        if (movingDown)
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else // move up
        {
            rb.velocity = new Vector2(0, speed);
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Table"))
        {
            movingDown = false; // Start moving upwards after touching the table
            hasTouchedTable = true; // The hand has touched the table

            if (!hasTomato)
            {
                print("FRESHTOMATO ADDED");

                // Spawn freshTomato as child when hand touches the table
                GameObject freshTomato = Instantiate(freshTomatoPrefab, transform.position, Quaternion.identity);
                freshTomato.transform.SetParent(transform);

                // Adjust position based on whether the hand is Human or Alien
                if (this is HumanHand)
                {
                    freshTomato.transform.localPosition = new Vector3(-0.5f, -2.5f, 0);
                }
                else if (this is AlienHand)
                {
                    freshTomato.transform.localPosition = new Vector3(0, -2.1f, 0);
                }

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

            // Adjust position based on whether the hand is Human or Alien
            if (this is HumanHand)
            {
                tomato.transform.localPosition = new Vector3(-0.5f, -2.5f, 0);

                // HumanHand hit a RottenTomato
                scoreManager.AddScore(-10);

                // Play bad sound here
                PlaySound(badSound);
            }
            else if (this is AlienHand)
            {
                tomato.transform.localPosition = new Vector3(0, -2.1f, 0);

                // AlienHand hit a RottenTomato
                scoreManager.AddScore(5);
                // Play good sound here
                PlaySound(goodSound);
            }

            hasTomato = true;
            return true;
        }

        print("HAS a TOMATO already");
        return false; // Tomato was not added because the hand already has one
    }

    // Method to play a sound
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
