using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    
    private Vector3 target;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA or PointB is not assigned in the Inspector!");
            enabled = false;  // Disable script if points are missing
            return;
        }

        target = pointB.position; // Start moving towards pointB
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If we reach the target, swap to the other point
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // Attach player to platform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // Detach player from platform
        }
    }
}
