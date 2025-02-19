using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float rotationSpeed = 15f;
    public float jumpForce = 5f; // Adjust jump height
    public Transform cameraTransform; // Assign manually in Inspector
    public LayerMask groundLayer; // Assign "Ground" layer in Inspector

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found! Add one to the player.");
        }
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Keep movement horizontal (ignore camera tilt)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Compute move direction relative to the camera
        Vector3 moveDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

        if (moveDirection != Vector3.zero)
        {
            // Move the player
            transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.World);

            // Rotate player to face movement direction smoothly
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        // Check if the player is touching the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // Debugging (optional)
        Debug.DrawRay(transform.position, Vector3.down * 1.1f, isGrounded ? Color.green : Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
