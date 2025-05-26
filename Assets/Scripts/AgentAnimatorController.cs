using UnityEngine;

public class AgentAnimatorController : MonoBehaviour
{
public Transform target;
    public float triggerDistance = 1.6f;
    private Animator animator;
    private bool canTriggerAnimation = true;
    private float cooldownTimer = 0f;
    public float animationCooldown = 2.0f; // Time before animation can be triggered again
    
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject!");
        }
        
        if (target == null)
        {
            // Try to find target from the SearchTarget script if not directly assigned
            SearchTarget searchTarget = GetComponent<SearchTarget>();
            if (searchTarget != null)
            {
                target = searchTarget.target;
            }
            else
            {
                Debug.LogError("Target not assigned and couldn't find SearchTarget script!");
            }
        }
    }
    
    void Update()
    {
        // Handle cooldown timer
        if (!canTriggerAnimation)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                canTriggerAnimation = true;
            }
        }
        
        // Check if we can trigger the animation
        if (canTriggerAnimation && target != null && animator != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            
            if (distance < triggerDistance)
            {
                animator.SetTrigger("HitTarget");
                canTriggerAnimation = false;
                cooldownTimer = animationCooldown;
                Debug.Log("Animation triggered: HitTarget");
            }
        }
    }
    
    // Public method to reset the animation state (call this from SearchTarget.OnEpisodeBegin)
    public void ResetAnimationState()
    {
        canTriggerAnimation = true;
        cooldownTimer = 0f;
    }
}
