using UnityEngine;

public class HumanHand : Hand
{
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RottenTomato"))
        {
            /*
            // HumanHand hit a RottenTomato
            scoreManager.AddScore(-10);

            // Play bad sound here
            PlaySound(badSound);
            */
        }

        base.OnTriggerEnter2D(collision);
    }
}
