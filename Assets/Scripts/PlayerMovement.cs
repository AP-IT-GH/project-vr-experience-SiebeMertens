using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Voorkomt dat de speler omvalt
    }

    void Update()
    {
        // Input ophalen
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Beweging toepassen in vaste tijdstappen
        Vector3 moveDirection = transform.forward * input.z + transform.right * input.x;
        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
