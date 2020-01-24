using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing; // whether score should increase or not

    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore")) {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    void Update()
    {
        if (scoreIncreasing) {
            scoreCount += pointsPerSecond * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.Round(scoreCount);
            highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
        }

        if (scoreCount > highScoreCount) {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }
    }

    public void addScore(int amount) {
        scoreCount += amount;
    }
}
