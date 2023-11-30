using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Enable/Disable player input.")]
    public bool canMove = true;

    public bool takingPhoto = false;

    [SerializeField]
    private HideController hidingController;

    [SerializeField]
    private GameObject cameraFlash;

    [SerializeField, Tooltip("Reference to the player's camera.")]
    private Transform playerCamera;

    [SerializeField, Tooltip("How fast the player can walk.")]
    private float walkSpeed = 8.0f;
    [SerializeField, Tooltip("How fast the player can run.")]
    private float runSpeed = 16.0f;

    private CharacterController m_controller; // Character Controller component.

    private Vector2 m_input; // Moving input.
    private bool m_isRunning = false; // Run button input.
    private bool m_photoInput = false; // Taking photo input.

    private Vector3 m_velocity = Vector3.zero; // Velocity (Gravity)
    private float m_moveSpeed = 0.0f; // Move speed.

    private void Start()
    {
        //GameManager.Instance.GamePaused = true;
        m_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        DoPlayerMovement();
        DoGravity();

        // Rotate player body towards camera direction.
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, playerCamera.localEulerAngles.y, transform.localEulerAngles.z);

        if (m_photoInput)
        {
            m_photoInput = false;
            TakePhoto();
        }
    }

    private void DoPlayerMovement()
    {
        if (hidingController.isHidden == false && hidingController.isHiding == false)
        {
            // Change move speed whether running or not.
            if (m_isRunning)
                m_moveSpeed = runSpeed;
            else
                m_moveSpeed = walkSpeed;

            // Player movement.
            Vector3 move = Vector3.zero;
            if (canMove) // Move player only if player input is enabled.
            {
                move = m_input.x * playerCamera.right + m_input.y * playerCamera.forward; // Get movement direction relative to camera direction.
                move.y = 0;
            }
            m_controller.Move(move.normalized * m_moveSpeed * Time.deltaTime); // Apply player movement.
        }
    }

    private void DoGravity()
    {
        if (hidingController.isHidden == false && hidingController.isHiding == false && hidingController.exitingHiding == false)
        {
            if (m_controller.isGrounded) // Is player touching ground.
            {
                m_velocity.y = -1.0f; // Push player out of ground.
            }
            else
            {
                m_velocity.y += Physics.gravity.y * Time.deltaTime; // Gravity.
            }
            m_controller.Move(m_velocity * Time.deltaTime); // Apply player velocity.
        }
    }

    // Input System messages.
    private void OnMove(InputValue value)
    {
        m_input = value.Get<Vector2>(); // Get movement input.
    }

    private void OnRun(InputValue value)
    {
        m_isRunning = value.isPressed; // Is running button pressed.
    }

    private void OnPhoto(InputValue value)
    {
        m_photoInput = value.Get<float>() >= 0.5f; // Is photo button pressed.
    }

    private void OnInteract(InputValue value)
    {
        // Statues
        if (GameManager.Instance.atStatue == true)
        {
            GameManager.Instance.interactWithStatue = true;
            Debug.Log("Game Saved.");
        }


        // Hiding
        if (hidingController.isHidden == true || hidingController.isHiding == true)
        {
            hidingController.exitingHiding = true;
            hidingController.ExitHidingSpot(hidingController.hidingSpots[hidingController.currentSpotIndex]);
            hidingController.hidingSpots[hidingController.currentSpotIndex].spotOccupied = false;
            hidingController.isHidden = false;
            hidingController.isHiding = false;
            hidingController.exitingHiding = false;
            //hidingController.hidingSpots[hidingController.currentSpotIndex].hidingSpotIndex = 99;
            Debug.Log("Player is no longer hiding.");
        }
        else if (hidingController.canHide == true)
        {
            if (hidingController.isHiding == false && hidingController.isHidden == false && hidingController.exitingHiding == false)
            {
                hidingController.isHiding = true;
                hidingController.EnterHidingSpot(hidingController.hidingSpots[hidingController.currentSpotIndex]);
                hidingController.hidingSpots[hidingController.currentSpotIndex].spotOccupied = true;
                hidingController.isHiding = false;
                hidingController.isHidden = true;
                Debug.Log("Player is hiding.");
            }
        }
    }

    // WENDIGO TESTING ONLY
    private void TakePhoto()
    {
        StartCoroutine(CameraTakePhoto());
    }

    private IEnumerator CameraTakePhoto()
    {
        takingPhoto = true;
        Debug.Log("Taking Photo!!");
        cameraFlash.GetComponent<Animator>().SetTrigger("Flash");
        yield return new WaitForNextFrameUnit();
        takingPhoto = false;
        StopCoroutine(CameraTakePhoto());
    }
}
