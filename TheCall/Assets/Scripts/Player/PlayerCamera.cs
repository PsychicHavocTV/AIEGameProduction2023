using Fungus;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to target, usually the player.")]
    private Transform target;

    [SerializeField, Tooltip("Reference to the player's controller.")]
    private PlayerController playerController;

    [SerializeField, Tooltip("Reference to the player's objectives.")]
    private PlayerObjectives playerObjectives;

    [SerializeField, Tooltip("The camera's offset from the target.")]
    private Vector3 offset;

    [SerializeField, Tooltip("How fast the camera moves.")]
    private float cameraSensitivity = 1.0f;

    private Vector2 m_input; // Contains player input for camera look.
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

        DoPhotoCheck();
    }

    private void LateUpdate() // Update camera after player movement.
    {
        // Apply camera rotation.
        transform.rotation = Quaternion.Euler(m_rotation.y, m_rotation.x, 0.0f);

        // Apply target position and offset to camera.
        transform.position = offset + target.position;
    }

    private void DoPhotoCheck()
    {
        if (playerController == null)
            return;

        if (playerController.takingPhoto)
        {
            GameObject lookingAt = GetComponent<ObjectsInView>().objects[0];
            if (lookingAt != null)
            {
                float area = GetComponent<AreaCompute>().Area;
                KeyObjectDescriptor descriptor = lookingAt.GetComponent<KeyObjectDescriptor>();
                if (area >= descriptor.objectThreshold)
                {
                    // GOOD!
                    if (playerObjectives.CurrentObjective == descriptor)
                    {
                        Debug.Log("Found " + descriptor.objectName + "!");
                        playerObjectives.objectiveComplete = true;
                    }
                }
            }
        }
    }

    // Input System messages.
    private void OnLook(InputValue value)
    {
        m_input = value.Get<Vector2>(); // Get mouse/gamepad input.
    }
}
