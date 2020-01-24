using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPoints : MonoBehaviour
{
    public int scoreToGive;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") {
            scoreManager.addScore(scoreToGive);
            gameObject.SetActive(false); // disable coin object
        }
    }
}
