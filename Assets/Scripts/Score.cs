using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        // update score and destroy self
        FindObjectOfType<GameManager>().changeScore(scoreValue);
        Destroy(gameObject);
    }
}
