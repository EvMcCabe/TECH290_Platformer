using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public BallAndChain ballAndChain; // Reference to the ball
    public Transform holdPosition; // Position where the ball hovers when held
    public float throwForce = 20f;
    public float tossForce = 10f;
    public float aimingSlowdown = 0.3f; // Bullet time effect
    public float holdMovementSpeedMultiplier = 0.7f; // Player moves slower when holding the ball
    public KeyCode throwKey = KeyCode.F;

    private bool isHolding = false;
    private bool isAiming = false;
    private Rigidbody ballRb;
    private PlayerMovementScript playerMovement;
    private Vector3 aimDirection;

    private void Start()
    {
        ballRb = ballAndChain.GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovementScript>();

        if (ballRb == null)
        {
            Debug.LogError("No Rigidbody found on BallAndChain!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            if (!isHolding)
            {
                PickUpBall();
            }
            else
            {
                StartAiming();
            }
        }

        if (Input.GetKeyUp(throwKey) && isAiming)
        {
            ThrowBall();
        }

        if (isHolding)
        {
            ballAndChain.transform.position = holdPosition.position; // Keep ball in front of player
        }
    }

    void PickUpBall()
    {
        isHolding = true;
        ballRb.isKinematic = true; // Disable physics while holding
        ballRb.velocity = Vector3.zero;
        playerMovement.playerSpeed *= holdMovementSpeedMultiplier; // Slow movement when holding
    }

    void StartAiming()
    {
        isAiming = true;
        Time.timeScale = aimingSlowdown; // Bullet time effect
        aimDirection = transform.forward; // Start aiming in player's direction
    }

    void ThrowBall()
    {
        isHolding = false;
        isAiming = false;
        Time.timeScale = 1f; // Reset time to normal

        ballRb.isKinematic = false; // Enable physics
        ballRb.velocity = Vector3.zero;

        Vector3 throwDirection = transform.forward;
        float appliedForce = isAiming ? throwForce : tossForce; // If aimed, throw harder

        ballRb.AddForce(throwDirection * appliedForce, ForceMode.Impulse);

        playerMovement.playerSpeed /= holdMovementSpeedMultiplier; // Restore normal movement
    }
}
