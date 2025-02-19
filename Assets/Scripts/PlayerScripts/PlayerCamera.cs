using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; // Assign the player's transform
    public Vector3 offset = new Vector3(0, 2, -5); // Camera offset behind the player
    public float sensitivity = 3f; // Mouse sensitivity
    public float rotationSmoothTime = 0.1f; // Smooth rotation time

    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;

    float yaw = 0f; // Horizontal rotation
    float pitch = 0f; // Vertical rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the screen center
        Cursor.visible = false; // Hides the cursor
    }

    void LateUpdate()
    {
        if (!player) return; // Avoid errors if no player is assigned

        // Get mouse input
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Limit up/down rotation

        // Smooth rotation
        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Apply rotation
        transform.eulerAngles = currentRotation;

        // Position the camera behind the player
        transform.position = player.position + transform.rotation * offset;

        // **Make player face the camera direction when moving forward**
        if (Input.GetKey(KeyCode.W)) // Change to match your forward movement key
        {
            Vector3 forwardDirection = transform.forward;
            forwardDirection.y = 0; // Keep the player upright (ignore vertical tilt)
            player.forward = forwardDirection; // Set the player's forward direction
        }
    }
}
