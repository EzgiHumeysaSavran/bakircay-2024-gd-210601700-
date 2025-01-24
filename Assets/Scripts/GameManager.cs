using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText; // Skor metni i�in TextMeshPro UI
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne yeniden y�klendi�inde yok edilmez
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // Sahne yeniden y�klendi�inde ScoreText'i yeniden bul
        FindScoreText();
        UpdateScore();
    }

    private void FindScoreText()
    {
        // Sahnedeki ScoreText objesini bul ve ba�la
        GameObject scoreTextObject = GameObject.Find("ScoreText"); // ScoreText GameObject'in ad�
        if (scoreTextObject != null)
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("ScoreText object not found in the scene.");
        }
    }
}
