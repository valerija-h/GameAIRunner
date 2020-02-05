using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAgents;

public class RunnerPlayer : Agent
{
    private RunnerArea runnerArea;
    private RayPerception rayPerception;

    private GameObject currentPlatform;
    private GameObject previousPlatform;
    private bool justLanded = false;

    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;
    public Vector3 playerStartPos;

    public Brain playerBrain;
    public Brain learningBrain;
    public RunnerAcademy academy;

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
        string[] detectableObjects = { "KillBox", "Points", "Platform", "Checkpoint", "Obstacles" }; // the tags it can see

        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
    }


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        runnerArea = FindObjectOfType<RunnerArea>();
        rayPerception = GetComponent<RayPerception3D>();
        playerStartPos = this.transform.position;
    }

    public void hitPoints(GameObject point) {
        AddReward(0.3f);
        point.SetActive(false); // disable coin object
    }

    public void OnCollisionEnter(Collision collision)
    {
        string currentTag = collision.gameObject.transform.tag;
        if (currentTag == "Checkpoint")
        {
            reachedCheckpoint = true;
            AddReward(1f);
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            if (currentScene == 1 || currentScene == 2) // for level one
            {  
                if (SceneStats.agentOption == true)
                {
                    SceneManager.LoadScene(5); // load the tennis agent
                }
                else
                {
                    SceneManager.LoadScene(3); // load the tennis tutorial
                }
            }
            else {  // for level two
                if (SceneStats.agentOption == true)
                {
                    SceneManager.LoadScene(10); // load the kart agent
                }
                else
                {
                    SceneManager.LoadScene(8); // load the kart tutorial
                }
            }
            Done(); // old agent
        }
        else if (currentTag == "Obstacles")
        {
            AddReward(-0.7f);
            AgentReset();
        }
        else if (currentTag == "KillBox") {
            AddReward(-0.7f);
            AgentReset();
        }
    }

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
                    AddReward(-0.05f);
                }
                else {
                    AddReward(0.2f);
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
