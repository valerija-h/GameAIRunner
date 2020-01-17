using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public int life;
    public Text scoreText;
    public Text lifeText;

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        lifeText.text = "Lives: " + life.ToString();
    }

    // add to the life
    public void changeLife(int num)
    {
        life += num;
        lifeText.text = "Lives: " + life.ToString();
    }

    // add to the current score
    public void changeScore(int num)
    {
        score += num;
        scoreText.text = "Score: " + score.ToString();
    }
}
