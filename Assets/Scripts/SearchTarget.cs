using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class SearchTarget : Agent
{
    public Transform target;
    public float moveSpeed = 2f;
    public float turnSpeed = 150f;
    public Transform plane;
    public float minSpawnDistance = 1.5f;
    private Rigidbody rb;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        rb.linearVelocity = Vector3.zero;

        Vector3 agentPos = Vector3.zero;
        Vector3 targetPos = Vector3.zero;

        float planeSizeX = plane.localScale.x * 5f; // Unity Plane is 10x10 units by default
        float planeSizeZ = plane.localScale.z * 5f;

        // Sample positions until they're far enough apart
        int attempts = 0;
        do
        {
            agentPos = new Vector3(
                Random.Range(-planeSizeX, planeSizeX),
                0.5f,
                Random.Range(-planeSizeZ, planeSizeZ)
            );

            targetPos = new Vector3(
                Random.Range(-planeSizeX, planeSizeX),
                0.5f,
                Random.Range(-planeSizeZ, planeSizeZ)
            );

            attempts++;
            if (attempts > 50) break; // safety net
        }
        while (Vector3.Distance(agentPos, targetPos) < minSpawnDistance);

        // Apply positions
        transform.localPosition = agentPos;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        if (target != null)
        {
            target.localPosition = targetPos;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // No need to add manual observations — ray sensor handles it
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("Action received: " + actions);
        int action = actions.DiscreteActions[0];

        switch (action)
        {
            case 0: // Do nothing
                break;
            case 1: // Move forward
                rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                break;
            case 2: // Turn left
                transform.Rotate(Vector3.up, -turnSpeed * Time.fixedDeltaTime);
                break;
            case 3: // Turn right
                transform.Rotate(Vector3.up, turnSpeed * Time.fixedDeltaTime);
                break;
        }

        // Small time penalty to encourage efficiency
        AddReward(-0.001f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            AddReward(1.0f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            AddReward(-0.1f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        discreteActionsOut[0] = 0; // default: no action

        if (Input.GetKey(KeyCode.Z))
        {
            discreteActionsOut[0] = 1; // Move forward
            Debug.Log("Z key (forward) held");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 2; // Turn left
            Debug.Log("Q key (left) held");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3; // Turn right
            Debug.Log("D key (right) held");
        }
    }
}
