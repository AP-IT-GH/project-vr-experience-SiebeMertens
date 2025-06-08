using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;
    private int deathCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if it was hit by the ML Agent
        if (collision.gameObject.CompareTag("MLAgent"))
        {
            Debug.Log("You got respawned");
            Respawn1();
        }
    }

    void Respawn1()
    {
        // Increment death counter
        deathCount++;
        
        // Check if we've reached the maximum number of deaths
        if (deathCount >= 3)
        {
            Debug.Log("Game over: Maximum deaths reached!");
            SceneManager.LoadScene("LoseOutroScene");
            return;
        }
        
        // Move to respawn point and reset velocity if Rigidbody exists
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log($"VR object respawned. Death {deathCount}/3");
    }
}
