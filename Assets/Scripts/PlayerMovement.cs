using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)] float moveSpeed = 10;
    [SerializeField]
    [Range(1, 100)] float jumpForce = 5;
    [SerializeField] CharacterController controller;

    [SerializeField] float gravity = -9.81f;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    bool grounded = true;

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        direction = new Vector3(movementVector.x, 0, movementVector.y).normalized;
    }

    private void OnJump()
    {
        if (grounded)
            velocity.y = Mathf.Sqrt(jumpForce * -3.0f * gravity);
    }

    void LateUpdate()
    {
        grounded = controller.isGrounded;
        controller.Move(transform.TransformDirection(direction) * (moveSpeed * Time.deltaTime));

        if (grounded && velocity.y < 0) velocity.y = 0;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
