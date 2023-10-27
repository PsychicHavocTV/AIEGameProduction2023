using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float mouseSensitivity = 1.0f;

    private Vector2 mouseDelta;

    private Vector2 rotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void Update()
    {
        mouseDelta = Mouse.current.delta.ReadValue();

        rotation.x += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        rotation.y -= mouseDelta.y * mouseSensitivity * Time.deltaTime;

        if (rotation.x >= 360.0f)
            rotation.x -= 360.0f;
        else if (rotation.x < 0.0f)
            rotation.x += 360.0f;

        rotation.y = Mathf.Clamp(rotation.y, -89.0f, 89.0f);
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0.0f);

        transform.position = offset + target.position;
    }
}
