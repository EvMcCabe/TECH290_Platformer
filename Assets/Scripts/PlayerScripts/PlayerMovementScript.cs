using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //declare variables
    public float playerSpeed = 20f;
    public float rotationSpeed = 15f;
    void Start()
    {
        //empty for now
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized; // Normalize to keep consistent speed

        if (moveDirection != Vector3.zero)
        {
            // Move the player
            transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.World);

            // Rotate player to face movement direction smoothly
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void RotatePlayerToCamera()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Keep rotation horizontal
        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

}