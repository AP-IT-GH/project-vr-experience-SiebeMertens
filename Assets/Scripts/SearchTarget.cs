using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class SearchTarget : Agent
{
    public Transform target;

    // public Transform trackableObject;
    
    public float moveSpeed = 2f;
    public float turnSpeed = 150f;
    public Transform plane;
    public float minSpawnDistance = 1.5f;
    
    // Fixed spawn positions
    private Vector3 agentSpawnPosition = new Vector3(1, 0f, -1);
    private Vector3 targetSpawnPosition = new Vector3(-1, 0.5f, 7);
    
    private Rigidbody rb;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset physics
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Reset animation if applicable
        AgentAnimatorController animController = GetComponent<AgentAnimatorController>();
        if (animController != null)
        {
            animController.ResetAnimationState();
        }

        // Apply static positions
        transform.localPosition = agentSpawnPosition;

        // Position target if available
        if (target != null)
        {
            target.localPosition = targetSpawnPosition;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // No need to add manual observations — ray sensor handles it
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get continuous actions for movement and rotation
        float moveAmount = actions.ContinuousActions[0]; // -1 to 1
        float turnAmount = actions.ContinuousActions[1]; // -1 to 1
        
        // Apply movement (forward/backward)
        if (Mathf.Abs(moveAmount) > 0.05f) // Small deadzone
        {
            rb.MovePosition(transform.position + transform.forward * moveAmount * moveSpeed * Time.fixedDeltaTime);
        }
        
        // Apply rotation (left/right)
        if (Mathf.Abs(turnAmount) > 0.05f) // Small deadzone
        {
            transform.Rotate(Vector3.up, turnAmount * turnSpeed * Time.fixedDeltaTime);
        }

        // Small time penalty to encourage efficiency
        AddReward(-0.001f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            AddReward(2.0f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            AddReward(-0.01f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        
        // Default to no movement
        continuousActionsOut[0] = 0f; // Forward/backward
        continuousActionsOut[1] = 0f; // Turn
        
        // Forward/backward movement
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            continuousActionsOut[0] = 1.0f; // Full forward
            Debug.Log("Forward input detected");
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            continuousActionsOut[0] = -1.0f; // Full backward
            Debug.Log("Backward input detected");
        }
        
        // Turning left/right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            continuousActionsOut[1] = -1.0f; // Full left
            Debug.Log("Left turn input detected");
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            continuousActionsOut[1] = 1.0f; // Full right
            Debug.Log("Right turn input detected");
        }
    }
}   