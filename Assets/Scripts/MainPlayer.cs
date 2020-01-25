using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;
    public int pointValue;  // how many values a point gives

    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;

    private GameManager gameManager;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        gameManager = FindObjectOfType<GameManager>();
        jumpTimeCounter = jumpTime;
        stoppedJumping = true;
    }

    void Update()
    {
        isGrounded = IsGrounded();
        playerRigidbody.velocity = new Vector3(moveSpeed, playerRigidbody.velocity.y, playerRigidbody.velocity.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
                stoppedJumping = false;
            }
        }

        // new jump scripts
        if (Input.GetKey(KeyCode.Space) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (isGrounded)
        {
            jumpTimeCounter = jumpTime;
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float raycastDistance = 1f;
        int mask = 1 << LayerMask.NameToLayer("Ground");

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, mask))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "KillBox")
        {
            gameManager.RestartGame();
        }
    }
}
