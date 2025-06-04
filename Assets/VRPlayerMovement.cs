using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class VRPlayerMovement : MonoBehaviour
{
    public InputActionProperty moveInput; // Link deze in de Inspector aan "XRI LeftHand/RightHand - Move"
    public Transform head; // Link de Main Camera (je VR hoofd) hieraan
    public float speed = 1.0f;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y);

        // Draai de richting mee met waar de speler naar kijkt
        Vector3 headYaw = new Vector3(head.forward.x, 0, head.forward.z).normalized;
        Quaternion headRotation = Quaternion.LookRotation(headYaw);

        Vector3 movement = headRotation * direction;
        characterController.Move(movement * speed * Time.deltaTime);
    }
}
