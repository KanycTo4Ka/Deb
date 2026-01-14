using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 100f)] float defaultMoveSpeed = 10f;
    [HideInInspector]
    public float curMoveSpeed = 0f;
    [SerializeField]
    [Range(1f, 100f)] float jumpForce = 5;
    [SerializeField] CharacterController controller;

    [SerializeField] float gravity = -9.81f;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    bool grounded = true;

    public UnityEvent speedChange;

    private void Start()
    {
        curMoveSpeed = defaultMoveSpeed;
        speedChange.Invoke();
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
        speedChange.Invoke();
    }

    public void modifyMoveSpeed(float amount)
    {
        curMoveSpeed = amount * curMoveSpeed;
        speedChange.Invoke();
    }

    public float getMoveSpeed()
    {
        return curMoveSpeed;
    }
}
