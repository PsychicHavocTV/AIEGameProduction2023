using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;

    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private float walkSpeed = 8.0f;
    [SerializeField]
    private float runSpeed = 16.0f;

    private Vector2 m_input;
    private bool m_isRunning = false;

    private Vector3 m_velocity = Vector3.zero;
    private float m_moveSpeed = 0.0f;

    void Update()
    {
        if (m_isRunning)
            m_moveSpeed = runSpeed;
        else
            m_moveSpeed = walkSpeed;

        Vector3 move = Vector3.zero;
        if (canMove)
        {
            move = m_input.x * playerCamera.right + m_input.y * playerCamera.forward;
            move.y = 0;
        }
        controller.Move(move.normalized * m_moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            m_velocity.y = -1.0f;
        }
        else
        {
            m_velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(m_velocity * Time.deltaTime);

        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, playerCamera.localEulerAngles.y, transform.localEulerAngles.z);
    }

    void OnMove(InputValue value)
    {
        m_input = value.Get<Vector2>();
    }

    void OnRun(InputValue value)
    {
        m_isRunning = value.isPressed;
    }
}
