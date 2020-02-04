using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class PickPoints : MonoBehaviour
{
    public RunnerPlayer runnerPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") {
            runnerPlayer.hitPoints(gameObject);
        }
    }
}
