using UnityEngine;

public class ChainRenderer : MonoBehaviour
{
    public Transform ball; // Assign the ball object in the Inspector
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Two points: Player & Ball
    }

    void Update()
    {
        if (ball != null)
        {
            lineRenderer.SetPosition(0, transform.position); // Player position
            lineRenderer.SetPosition(1, ball.position); // Ball position
        }
    }
}
