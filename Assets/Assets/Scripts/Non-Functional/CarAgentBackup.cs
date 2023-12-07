//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

//[RequiredComponent(typeof(BoxCollider))]
//[RequiredComponent(typeof(DecisionRequester))]
public class CarAgentBackup : Agent
{
    [System.Serializable]
    public class RewardInfo
    {
        public float barrier    = -1.0f;
        public float obstacle   = -0.1f;
        public float redWall    = -0.005f;
        public float greenWall  = 0.01f;
        public float goal       = 1.0f;
    }
    public RewardInfo rwd = new RewardInfo();
    private Vector3 recall_position;
    public bool doEpisodes = true; 
    private Quaternion recall_rotation;
    private Bounds bnd;

    public override void Initialize()
    {
        recall_position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //recall_rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);

    }

    public override void OnEpisodeBegin()
    {
        // missing Reset currentSpeed
        this.transform.position = recall_position;
        this.transform.rotation = recall_rotation;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }     

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
