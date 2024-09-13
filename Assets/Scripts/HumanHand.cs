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
            Debug.Log("HumanHand hit a RottenTomato");
            scoreManager.AddScore(-10);
        }

        if(!hasTomato)
        {
            scoreManager.AddScore(5);
        }

        base.OnTriggerEnter2D(collision);
    }

}
