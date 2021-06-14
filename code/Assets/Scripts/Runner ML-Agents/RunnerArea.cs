using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;
using TMPro;

public class RunnerArea : Area
{
    public RunnerPlayer runnerAgent; // player
    public GameObject points; // all the points in the scene
    public Text reward; // score

    public override void ResetArea()
    {
        PlacePlayer();
        RespawnAllPoints();
    }

    // spawn points
    private void RespawnAllPoints() {
        foreach (Transform child in points.transform)
            child.gameObject.SetActive(true);
    }

    // spawn player
    private void PlacePlayer() {
        runnerAgent.transform.position = runnerAgent.playerStartPos;
    }

    // in update, update score
    private void Update()
    {
        reward.text = "Score: " + System.Math.Round(runnerAgent.GetCumulativeReward(), 3).ToString();
    }
}
