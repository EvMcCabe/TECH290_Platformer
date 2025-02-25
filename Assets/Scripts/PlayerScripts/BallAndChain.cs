using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAndChain : MonoBehaviour
{
    public float maxRadius = 5f; // Max distance the player can move from the ball
    public Transform player;
    public Transform ballHoldPoint; // Position where the ball should be held
    public float throwForce = 20f;
    public float aimSlowdownFactor = 0.3f; // Slow motion effect when aiming
    public KeyCode throwKey = KeyCode.F;

    private Rigidbody rb;
    private bool isHeld = false;
    private bool isAiming = false;
    private bool isThrown = false; // To track if the ball has been thrown

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Initially, the ball should not move (static)
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (!isHeld && distance > maxRadius)
        {
            Vector3 directionBack = (transform.position - player.position).normalized;
            player.position = transform.position - directionBack * maxRadius;
        }

        HandleThrowInput();

        if (isHeld)
        {
            // Keep the ball at the hold point
            transform.position = ballHoldPoint.position;
        }
        else if (isThrown)
        {
            // Check if the ball is stationary after being thrown and landed
            if (rb.velocity.magnitude < 0.1f) // Threshold for when the ball has stopped moving
            {
                rb.isKinematic = true; // Set ball to kinematic to stop rolling
                isThrown = false; // Reset the thrown state
            }
        }
    }

    public void PickUp()
    {
        isHeld = true;
        isAiming = false;
        rb.isKinematic = true; // Ball should not be affected by physics
        transform.parent = player; // Attach ball to player
        transform.position = ballHoldPoint.position; // Move ball to hold point
    }

    public void Drop()
    {
        isHeld = false;
        isAiming = false;
        rb.isKinematic = true; // Make it static on the ground
        transform.parent = null; // Detach from the player
    }

    private void HandleThrowInput()
    {
        if (Input.GetKeyDown(throwKey))
        {
            if (isHeld)
            {
                // Check if Shift is held down with the throw key
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    DropInFront();
                }
                else
                {
                    StartAiming();
                }
            }
            else
            {
                PickUp();
            }
        }

        if (Input.GetKeyUp(throwKey) && isAiming)
        {
            Throw();
        }
    }

    private void StartAiming()
    {
        isAiming = true;
        Time.timeScale = aimSlowdownFactor; // Enable slow motion
    }

    private void Throw()
    {
        isHeld = false;
        isAiming = false;
        isThrown = true; // Mark that the ball has been thrown
        Time.timeScale = 1f; // Restore normal time

        transform.parent = null; // Detach from the player
        rb.isKinematic = false; // Enable physics for the throw
        rb.velocity = player.forward * throwForce; // Throw in facing direction
    }

    private void DropInFront()
    {
        // Drop the ball directly in front of the player with no momentum
        isHeld = false;
        isAiming = false;
        rb.isKinematic = false; // Enable physics to allow the drop
        transform.parent = null; // Detach from the player

        // Set position directly in front of the player
        Vector3 dropPosition = player.position + player.forward * 2f; // Adjust 2f to the desired drop distance
        transform.position = dropPosition;
        rb.velocity = Vector3.zero; // Set velocity to zero so it doesn't have momentum
        rb.angularVelocity = Vector3.zero; // Stop any spinning from previous throws
    }
}
