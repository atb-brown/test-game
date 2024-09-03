using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    private float forceAmount = 350.0f; // Amount of force to apply
    private Rigidbody rb;
    private int groundedFactor;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();

        // If the Rigidbody component is not found, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        groundedFactor = 0;

        // Ensure the object is affected by physics
        rb.isKinematic = false;
    }

    // FixedUpdate is called 0 to many times per frame with the physics engine.
    void FixedUpdate()
    {
        // Get input from the spacebar
        float jumpMovement = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;

        if (jumpMovement > 0.0f && groundedFactor > 0) {
            // Create a Vector3 from the input
            Vector3 movement = new Vector3(0.0f, jumpMovement, 0.0f);

            float sizeCoefficient = rb.GetComponent<Collider>().bounds.size.x / 2.0f;

            // Apply force to the Rigidbody
            rb.AddForce(movement * forceAmount * sizeCoefficient);
            groundedFactor = 0;
        } else if (rb.velocity.y == 0.0f) {
            groundedFactor++;
        }

        if (rb.velocity.y > 0.0f) {
            groundedFactor = 0;
        }
    }
}
