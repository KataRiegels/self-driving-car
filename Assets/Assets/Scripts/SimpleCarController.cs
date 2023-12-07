using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 60f;
    public float steeringSensitivity = 6f;
    public float brakeForce = 50f;
    public float dragFactor = 3f;
    
    // Make currentSpeed a public property
    public float currentSpeed { get; private set; }

    void FixedUpdate()
    {
        // Apply drag (simulated friction) when there's minimal acceleration input
        if (Mathf.Abs(currentSpeed) < 0.1f)
        {
            float drag = dragFactor * Time.deltaTime;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, drag);
        }

        // Update the car's position
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void ApplyAcceleration(float input)
    {
        // Accelerating forward
        if (input > 0)
        {
            currentSpeed += input * acceleration * Time.deltaTime;
        }
        // Braking or reversing
        else if (input < 0)
        {
            // If the car is moving forward, apply brakes
            if (currentSpeed > 0)
            {
                float brake = brakeForce * Time.deltaTime;
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brake);
            }
            // If the car is already moving backward, accelerate in reverse
            else
            {
                currentSpeed += input * acceleration * Time.deltaTime;
            }
        }
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
    }

    public void ApplySteering(float input)
    {
        float steeringFactor = steeringSensitivity * Mathf.Exp(-Mathf.Abs(currentSpeed) / maxSpeed);
        float steeringAngle = input * steeringFactor;
        transform.Rotate(0, steeringAngle * currentSpeed * Time.deltaTime, 0);
    }

    public void ResetSpeed()
    {
        currentSpeed = 0;
    }
}
