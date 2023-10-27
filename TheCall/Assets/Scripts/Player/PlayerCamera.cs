using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to target, usually the player.")]
    private Transform target;

    [SerializeField, Tooltip("The camera's offset from the target.")]
    private Vector3 offset;

    [SerializeField, Tooltip("How fast the camera moves.")]
    private float cameraSensitivity = 1.0f;

    private Vector2 m_input;

    private Vector2 m_rotation; // Camera's rotation.

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor into window.
    }

    private void Update()
    {
        // Calculate rotation based on input.
        m_rotation.x += m_input.x * cameraSensitivity * Time.deltaTime;
        m_rotation.y -= m_input.y * cameraSensitivity * Time.deltaTime;

        // Wrap camera yaw between 0-360 degrees, (avoids floating point errors)
        if (m_rotation.x >= 360.0f)
            m_rotation.x -= 360.0f;
        else if (m_rotation.x < 0.0f)
            m_rotation.x += 360.0f;

        // Clamp camera pitch within range, (Prevents camera flipping)
        m_rotation.y = Mathf.Clamp(m_rotation.y, -89.0f, 89.0f);
    }

    private void LateUpdate() // Update camera after player movement.
    {
        // Apply camera rotation.
        transform.rotation = Quaternion.Euler(m_rotation.y, m_rotation.x, 0.0f);

        // Apply target position and offset to camera.
        transform.position = offset + target.position;
    }

    // Input System messages.
    private void OnLook(InputValue value)
    {
        m_input = value.Get<Vector2>(); // Get mouse/gamepad input.
    }
}
