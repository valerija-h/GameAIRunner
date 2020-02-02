using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RunnerAcademy : Academy
{
    private RunnerArea[] runnerAreas;

    public override void AcademyReset()
    {
        // populate runner areas
        if (runnerAreas == null) {
            runnerAreas = FindObjectsOfType<RunnerArea>();
        }

        // set up areas
        foreach (RunnerArea runnerArea in runnerAreas) {
            runnerArea.ResetArea();
        }
    }
}
