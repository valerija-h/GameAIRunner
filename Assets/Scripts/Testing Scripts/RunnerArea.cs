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
    public GameObject checkpoint;
    public Text reward; // score

    public override void ResetArea()
    {
        // remove all points ?
        RemoveAllPoints();
        // reset player ?
        PlacePlayer();
        // spawn all points ?
        SpawnAllPoints();
    }

    // remove points
    private void RemoveAllPoints() {
        Destroy(GameObject.FindGameObjectWithTag("Points"));
    }

    // spawn points
    private void SpawnAllPoints() {
        GameObject newPoints = Instantiate(points);
    }

    // spawn player
    private void PlacePlayer() {
        runnerAgent.transform.position = new Vector3(-10f, 2f, 0f);
    }

    // in update, update score
    private void Update()
    {
        reward.text = "Score: " + runnerAgent.GetCumulativeReward().ToString();
    }
}
