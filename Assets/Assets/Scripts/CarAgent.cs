using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
    private SimpleCarController carController;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    [System.Serializable]
    public class Rewards
    {
        public float greenWall = 0.1f;
        public float redWall = -0.01f;
    }
    public Rewards reward = new Rewards();
    private void Start()
    {
        carController = GetComponent<SimpleCarController>();
        
        // Store the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public override void OnEpisodeBegin()
    {   // Reset the environment for the next episode here
        //Debug.Log("Episode began, resetting agent");  // Check reset
        
        // Reset the car's position and rotation to it's initial state
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Reset the car's speed to 0
        carController.ResetSpeed();

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(carController.currentSpeed / carController.maxSpeed);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float accelerationInput = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
        float steeringInput = Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);

        // Use the inputs of our car controller script
        carController.ApplyAcceleration(accelerationInput);
        carController.ApplySteering(steeringInput);

        // Add a small penalty for every action taken (every time step)
        AddReward(-0.001f);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float accelerationInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        actionsOut.ContinuousActions.Array[0] = accelerationInput;
        actionsOut.ContinuousActions.Array[1] = steeringInput;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("GreenWall"))
        {
            Debug.Log("Collided with GreenWall");
            AddReward(reward.greenWall);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("RedWall"))
        {
            Debug.Log("Collided with RedWall");
            AddReward(reward.redWall);
            EndEpisode();

        }
    }
}