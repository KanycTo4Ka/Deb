using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)] float defaultMoveSpeed = 10;
    float curMoveSpeed;
    [SerializeField]
    [Range(1, 100)] float jumpForce = 5;
    [SerializeField] CharacterController controller;

    [SerializeField] float gravity = -9.81f;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    bool grounded = true;

    private void Start()
    {
        curMoveSpeed = defaultMoveSpeed;
    }

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
        controller.Move(transform.TransformDirection(direction) * (curMoveSpeed * Time.deltaTime));

        if (grounded && velocity.y < 0) velocity.y = 0;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void setToDefault()
    {
        curMoveSpeed = defaultMoveSpeed;
    }

    public void modifyMoveSpeed(float amount)
    {
        curMoveSpeed = amount * curMoveSpeed;
    }

    public float getMoveSpeed()
    {
        return curMoveSpeed;
    }
}
