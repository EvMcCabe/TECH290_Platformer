using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 20f; //declare in editor
    public Vector3 collectibleRotation = new Vector3(0, 0, 1); //make new vector for z-axis rotation

    // Start is called before the first frame update
    void Start()
    {
        //empty for now
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Collectible"))
        {
             transform.Rotate(collectibleRotation * rotationSpeed * Time.deltaTime); //rotation only along z-axis
        }
        else 
        {
             transform.Rotate(new Vector3(50, 75, 90) * Time.deltaTime); //random rotation
        }
       
    }
}
