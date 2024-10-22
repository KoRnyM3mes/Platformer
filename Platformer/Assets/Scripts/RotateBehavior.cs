using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : MonoBehaviour
{ 
    // Rotation speed can be adjusted from the Unity Editor
    public float rotationSpeed = 30f; // degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the platform around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
