using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer cubeRenderer;

    void Start()
    {
        // Get the Renderer component attached to the Cube
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Check if the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Color Changed");
            // Change the cube's color to a random color
            cubeRenderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
