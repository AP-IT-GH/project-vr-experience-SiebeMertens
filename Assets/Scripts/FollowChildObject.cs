using UnityEngine;

public class FollowChildObject : MonoBehaviour
{
    [Tooltip("The child Transform to follow")]
    public Transform childToFollow;

    [Tooltip("If true, parent will also rotate to match the child")]
    public bool followRotation = false;

    void Fixedpdate()
    {
        if (childToFollow == null) return;

        // Update parent's position to match the child
        transform.position = childToFollow.position;

        // Optional: match rotation
        if (followRotation)
        {
            transform.rotation = childToFollow.rotation;
        }
    }
}


