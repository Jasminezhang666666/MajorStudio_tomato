using UnityEngine;

public class AlienHand : Hand
{
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("RottenTomato"))
        {
            scoreManager.AddScore(5);
        }
    }
}
