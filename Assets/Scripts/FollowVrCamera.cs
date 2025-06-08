using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform vrCamera; // Reference to the VR camera (HMD)
    public CapsuleCollider bodyCollider;

    void Update()
    {
        Vector3 pos = vrCamera.position;
        pos.y = transform.position.y; // Keep collider base level
        bodyCollider.transform.position = pos;
    }
}


