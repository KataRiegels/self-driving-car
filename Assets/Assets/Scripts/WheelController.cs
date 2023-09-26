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

    private void FixedUpdate() {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space)) {
            currentBreakForce = breakingForce;
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

    }

    void UpdateWheel(WheelCollider col, Transform trans) {
        
    Vector3 position;
    Quaternion rotation;
    col.GetWorldPose(out position, out rotation);

    // Modify the rotation quaternion here, for example:
    rotation *= Quaternion.Euler(0, 90.00001f, 0);  // replace with your required rotation offset

    // Set Wheel transform state.
    trans.position = position;
    trans.rotation = rotation;
}

}
