using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if it was hit by the ML Agent
        if (collision.gameObject.CompareTag("MLAgent"))
        {
            Respawn1();
        }
    }

    void Respawn1()
    {
        // Move to respawn point and reset velocity if Rigidbody exists
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("VR object respawned.");
    }
}
