using UnityEngine;
using UnityEngine.SceneManagement; // Import SceneManager
using TMPro; // Import TextMeshPro

public class LevelManager : MonoBehaviour
{
    public float levelTime = 15f; // Time limit for each level
    public int scoreLimit = 20; // Score limit to pass to the next level
    public TextMeshProUGUI timeText; // UI TextMeshPro for time display
    public TextMeshProUGUI scoreText; // UI TextMeshPro for score display
    public TextMeshProUGUI levelText; // UI TextMeshPro for level display

    private float timeRemaining;
    private bool levelEnded = false;

    private ScoreManager scoreManager;
    private int levelNumber = 1;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeRemaining = levelTime;
        UpdateLevelDisplay();
    }

    void Update()
    {
        if (!levelEnded)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                // Ensure that level only ends when both conditions are met
                if (scoreManager.GetScore() >= scoreLimit)
                {
                    EndLevel();
                }
            }
            else
            {
                UpdateTimeDisplay();
            }

            // Update score display
            scoreText.text = $"Score: {scoreManager.GetScore()}";
        }
    }

    private void EndLevel()
    {
        if (levelEnded) return; // Avoid multiple triggers

        levelEnded = true;
        Debug.Log("Level Ended!");

        // Load the next level scene
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        // Increment level number
        levelNumber++;

        // Build the scene name based on the new level number
        string nextLevelName = $"Level_{levelNumber}";

        // Check if the scene exists
        if (Application.CanStreamedLevelBeLoaded(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError($"Scene {nextLevelName} cannot be loaded. Check if the scene name is correct.");
        }
    }

    private void UpdateTimeDisplay()
    {
        timeText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}";
    }

    private void UpdateLevelDisplay()
    {
        levelText.text = $"Level: {levelNumber}";
    }
}
