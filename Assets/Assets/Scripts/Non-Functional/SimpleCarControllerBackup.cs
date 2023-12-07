using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarControllerBackup : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 60f;            // Variable for Maximum speed of the vehicle
    public float steeringSensitivity = 6f;  // Variable for the steering (Faster currentSpeed = lesser Sensitivity see: Handle steering section)
    public float brakeForce = 50f;          // Variable for simulating brakes
    public float dragFactor = 3f;           // Variable for simulating friction
    private float currentSpeed = 0f;        // Variable for the vehicles current Speed (Used for calculating steering angle)

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Handle acceleration and deceleration
        currentSpeed += verticalInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Apply drag (simulated friction) when there's no acceleration input
        if (Mathf.Abs(verticalInput) < 0.1f)
        {
            float drag = dragFactor * Time.deltaTime;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, drag);
        }

        // Handle steering
        float steeringFactor = steeringSensitivity * Mathf.Exp(-Mathf.Abs(currentSpeed) / maxSpeed);
        float steeringAngle = horizontalInput * steeringFactor;
        transform.Rotate(0, steeringAngle * currentSpeed * Time.deltaTime, 0);

        // Handle braking
        if (Input.GetKey(KeyCode.Space))
        {
            float brake = brakeForce * Time.deltaTime;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brake);
        }

        // Update the car's position
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Print values
        Debug.Log("Current Speed: " + currentSpeed + ", Steering Factor: " + steeringFactor + ", Steering Angle: " + steeringAngle);
    }
}
