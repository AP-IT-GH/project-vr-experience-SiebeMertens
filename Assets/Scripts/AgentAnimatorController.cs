using UnityEngine;

public class AgentAnimatorController : MonoBehaviour
{
    public Transform target;
    public float visionAngle = 60f; // Field of view angle in degrees
    public float visionDistance = 3f; // How far the agent can see
    public float attackDistance = 1.5f; // Distance required to trigger attack animation
    public LayerMask obstacleLayer; // Set this in inspector to detect obstacles
    private Animator animator;
    private bool canTriggerAnimation = true;
    public AudioSource chaseMusic;
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
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            
            // Only trigger if both conditions are met: visible AND within attack distance
            if (IsTargetVisible() && distanceToTarget <= attackDistance)
            {
                animator.SetTrigger("HitTarget");
                canTriggerAnimation = false;
                cooldownTimer = animationCooldown;
                Debug.Log($"Animation triggered: HitTarget - Target is visible and within attack range ({distanceToTarget:F2} units)");
            }
        }
    }
    
    // Check if target is visible to the agent
    private bool IsTargetVisible()
    {
        if (target == null) {if(chaseMusic.isPlaying){
            chaseMusic.Stop();
            } return false;}
        
        // Direction to target
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        
        // Get distance to target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        
        // Check if target is too far for vision
        if (distanceToTarget > visionDistance)
        {
            if(chaseMusic.isPlaying){
            chaseMusic.Stop();
            }
            return false;
        }
        
        // Check if target is within field of view
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        if (angle > visionAngle / 2f)
        {
            // Draw debug ray in red to show target is outside vision angle
            Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.red, 0.1f);
            if(chaseMusic.isPlaying){
            chaseMusic.Stop();
            }
            return false;
        }
        
        // Check for obstacles between agent and target
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget, obstacleLayer))
        {
            // Something is blocking the view
            if (hit.transform != target)
            {
                // Draw debug ray in yellow to show vision is blocked
                Debug.DrawRay(transform.position, directionToTarget * hit.distance, Color.yellow, 0.1f);
                if(chaseMusic.isPlaying){
                chaseMusic.Stop();
                }
                return false;
            }
        }
        
        // Target is visible - draw debug ray in green and start chase music
        Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.green, 0.1f);
        if(chaseMusic.isPlaying){
        chaseMusic.Stop();
        }
        return true;
    }
    
    // Public method to reset the animation state (call this from SearchTarget.OnEpisodeBegin)
    public void ResetAnimationState()
    {
        canTriggerAnimation = true;
        cooldownTimer = 0f;
    }
}
