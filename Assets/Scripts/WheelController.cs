using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider rearRight;
    [SerializeField] WheelCollider rearLeft;
    
    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform rearRightTransform;
    [SerializeField] Transform rearLeftTransform;

    

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;
    public float currentTurnAngle = 0f;
    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    //Access the RB used for keeping track of speed.
    private Rigidbody rb;

    void Start() {
    rb = GetComponent<Rigidbody>();
}

    private void FixedUpdate() {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space)) {
            currentBreakForce = breakingForce;
            // TODO!!! Change material of the backlights () when the car is breaking
        }

        else {
            currentBreakForce = 0f;
        }

        rearRight.motorTorque = currentAcceleration;
        rearLeft.motorTorque = currentAcceleration;
        //frontRight.motorTorque = currentAcceleration;
        //frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        rearRight.brakeTorque = currentBreakForce;
        rearLeft.brakeTorque = currentBreakForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(rearLeft, rearLeftTransform);
        UpdateWheel(rearRight, rearRightTransform);

        Debug.Log("Current Speed: " + CurrentSpeed);

    }
    // Method for rotating and turning wheels accordingly
    void UpdateWheel(WheelCollider col, Transform trans) {
        
    Vector3 position;
    Quaternion rotation;
    col.GetWorldPose(out position, out rotation);

    rotation *= Quaternion.Euler(0, 90.00001f, 0);  // Changed the rotation of y-axis to 90f to match the wheel prefab offset.

    // Set Wheel transform state.
    trans.position = position;
    trans.rotation = rotation;
    }

    // Float for tracking kilometers per hour.
    public float CurrentSpeed {
        get {
            float speed = rb.velocity.magnitude * 3.6f;
            return (speed < 0.01f) ? 0f : speed;  // if speed is less than 0.01 km/h, return 0
        }
    }

}
