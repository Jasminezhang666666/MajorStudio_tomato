using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI TextMeshPro for score display
    private int score = 0;

    void Start()
    {
        UpdateScoreDisplay();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = $"Score: {score}";
    }
}
