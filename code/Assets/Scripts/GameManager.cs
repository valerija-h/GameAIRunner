using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MainPlayer player;
    private Vector3 playerStartPoint;

    private ScoreManager scoreManager;

    void Start()
    {
        player = FindObjectOfType<MainPlayer>();
        scoreManager = FindObjectOfType<ScoreManager>();

        playerStartPoint = player.transform.position;
    }

    void Update()
    {
        
    }

    public void RestartGame() {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo() {
        scoreManager.scoreIncreasing = false;
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = playerStartPoint;
        player.gameObject.SetActive(true);

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
    }
}
