using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        isGrounded = IsGrounded();
        playerRigidbody.velocity = new Vector3(moveSpeed, playerRigidbody.velocity.y, playerRigidbody.velocity.z);

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded) {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
            }
        }
    }

    bool IsGrounded() {
        RaycastHit hit;
        float raycastDistance = 0.5f;
        int mask = 1 << LayerMask.NameToLayer("Ground");

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, mask)) {
            return true;
        }
        return false;
    }
}
