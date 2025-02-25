using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float acceleration = 10f; // Smooth movement acceleration
    public float deceleration = 15f; // Smooth stopping
    public float rotationSpeed = 10f;
    public float jumpForce = 10f;
    public float gravityMultiplier = 2f; // Custom gravity for better falling
    public Transform cameraTransform;
    public LayerMask groundLayer;

    public BallAndChain ballAndChain;
    public Transform ball;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;
    private Vector3 currentVelocity; // For smooth movement

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found! Add one to the player.");
        }
        rb.freezeRotation = true; // Prevent unwanted rotation
    }

    private void Update()
    {
        CheckGround();
        ProcessInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyGravity();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 targetDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

        if (targetDirection != Vector3.zero)
        {
            moveDirection = Vector3.Lerp(moveDirection, targetDirection, acceleration * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    void ApplyMovement()
    {
        Vector3 targetVelocity = moveDirection * playerSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

        // Check ball distance
        float distance = Vector3.Distance(transform.position, ball.position);
        if (distance > ballAndChain.maxRadius)
        {
            Vector3 directionBack = (ball.position - transform.position).normalized;
            transform.position = ball.position - directionBack * ballAndChain.maxRadius;
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded) // Only apply gravity when not grounded
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    void CheckGround()
    {
        Vector3 origin = transform.position + Vector3.up * 0.2f;
        float sphereRadius = 2.5f;
        float castDistance = 6f;

        isGrounded = Physics.SphereCast(origin, sphereRadius, Vector3.down, out _, castDistance, groundLayer);
    }
}
