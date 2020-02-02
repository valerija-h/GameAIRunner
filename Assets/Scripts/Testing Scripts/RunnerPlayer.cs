using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RunnerPlayer : Agent
{
    private RunnerArea runnerArea;
    private RayPerception rayPerception;
    private GameObject checkpoint;

    private GameObject platforms;
    private GameObject points;

    private GameObject currentPlatform;
    private GameObject previousPlatform;
    private bool justLanded = false;

    bool reachedCheckpoint;

    // called when made a decision - jump or not
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // convert actions to axis values
        float jump = vectorAction[0]; //check if spacebar pressed

        if (jump == 1f) {
            if (isGrounded)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
            }
        }

        AddReward(-0.01f / agentParameters.maxStep); // tiny negative reward every step - he does anything
    }

    public override void AgentReset()
    {
        // reset any player stats
        reachedCheckpoint = false;
        runnerArea.ResetArea();
        points = GameObject.Find("Points");
    }

    // how it percieves/observes its environment
    public override void CollectObservations()
    {
        // has the chekpoint been reached
        AddVectorObs(reachedCheckpoint);
        AddVectorObs(this.transform.position);

        // raycasts (sight we only need ray angles 0 i guess, tags is what they can see)
        float rayDistance = 20f;
        float[] rayAngles = { 20f, 60f, 90f, 120f, 180f }; // the angle it sees
        string[] detectableObjects = { "KillBox", "Points", "Platform", "Checkpoint" }; // the tags it can see

        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        runnerArea = FindObjectOfType<RunnerArea>();
        rayPerception = GetComponent<RayPerception3D>();
        checkpoint = runnerArea.checkpoint;
        platforms = GameObject.Find("Platforms");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Checkpoint"))
        {
            reachedCheckpoint = true;
            AddReward(10f);
        }
        else if (collision.transform.CompareTag("Points")) {
            AddReward(1f);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("KillBox"))
        {
            AddReward(-5f);
            this.transform.position = new Vector3(-10f, 2f, 0f);
        }
    }

    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;

    void Update()
    {
        isGrounded = IsGrounded();
        playerRigidbody.velocity = new Vector3(moveSpeed, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float raycastDistance = 1f;
        int mask = 1 << LayerMask.NameToLayer("Ground");

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, mask))
        {
            currentPlatform = hit.transform.gameObject;
           
           if (!justLanded) {
                justLanded = true;
                Debug.Log("Landed");
                if (currentPlatform == previousPlatform) {
                    AddReward(-1f);
                }
                else {
                    AddReward(1f);
                }
                previousPlatform = currentPlatform;
            }

            return true;
        }
        currentPlatform = null;
        justLanded = false;
        return false;
    }
}
