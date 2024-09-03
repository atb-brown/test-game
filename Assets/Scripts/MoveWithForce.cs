using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour
{
    private float forceAmount = 10f; // Amount of force to apply
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();

        // If the Rigidbody component is not found, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Ensure the object is affected by physics
        rb.isKinematic = false;
    }

    void FixedUpdate()
    {
        // Get input from the WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 from the input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force to the Rigidbody
        rb.AddForce(movement * forceAmount);
    }
}
