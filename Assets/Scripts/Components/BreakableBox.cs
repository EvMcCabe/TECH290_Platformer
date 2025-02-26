using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object that hit the box is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject); // Destroy the box
        }
    }
}
