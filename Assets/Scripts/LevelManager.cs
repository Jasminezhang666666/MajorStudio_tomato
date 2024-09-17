using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float levelTime = 15f; // Time limit
    public int scoreLimit = 20; // Score limit to pass
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;

    private float timeRemaining;
    private bool levelEnded = false;

    private ScoreManager scoreManager;
    private int levelNumber;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeRemaining = levelTime;

        // reset the level number to 1
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            levelNumber = 1;
            PlayerPrefs.SetInt("LevelNumber", levelNumber); // Save this reset value
            PlayerPrefs.Save();
        }
        else
        {
            // Load the current level number from PlayerPrefs
            levelNumber = PlayerPrefs.GetInt("LevelNumber", 1); // Default to level 1 if no value exists
        }

        UpdateLevelDisplay();
    }


    void Update()
    {
        if (!levelEnded)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                EndLevel();
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
        if (levelEnded) return;

        levelEnded = true;
        Debug.Log("Level Ended!");

        int currentScore = scoreManager.GetScore();

        // If the player's score is lower than the limit, go to BadEnd
        if (currentScore < scoreLimit)
        {
            SceneManager.LoadScene("BadEnd");
        }
        else
        {
            // If the current level is Level_3 and score is above the limit, go to HappyEnd
            if (levelNumber == 3)
            {
                SceneManager.LoadScene("HappyEND");
            }
            else
            {
                // Otherwise, load the next level
                LoadNextLevel();
            }
        }
    }

    private void LoadNextLevel()
    {
        // Increment level number
        levelNumber++;

        // Save the new level number in PlayerPrefs
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
        PlayerPrefs.Save(); // Ensure the value is saved

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
