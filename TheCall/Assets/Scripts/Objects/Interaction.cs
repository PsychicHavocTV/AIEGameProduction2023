using Fungus;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the target camera. usually the player's camera.")]
    private GameObject m_targetCamera;

    [SerializeField, Tooltip("A reference to the trigger collider.")]
    private Collider m_triggerCollider;

    [SerializeField, Tooltip("The distance the player has to be to be able to interact with the object.")]
    private float m_interactionDistance = 4.0f;

    [Header("UI References")]
    [SerializeField, Tooltip("A reference to the interactions parent within the player's UI.")]
    private GameObject m_interactionsParent;

    [SerializeField, Tooltip("A reference to the interaction prefab.")]
    private GameObject m_interactionPrefab;

    [SerializeField, Tooltip("The offset from the object the interaction object UI will appear at.")]
    private Vector3 m_offset = Vector3.zero;

    public bool Interactable
    {
        get => m_interactable;
    }
    private bool m_interactable = false; // Whether it can be interacted with.

    public bool Interacted
    {
        get => m_interacted;
        set => m_interacted = value;
    }
    private bool m_interacted = false; // Whether it has been interacted with or not.

    private Vector2 m_interactionObjectSize; // The original size of the prefab.
    private GameObject m_uiInstance = null; // The instance of the prefab.

    private void Start()
    {
        m_interactionObjectSize = m_interactionPrefab.transform.localScale;
    }

    private void Update()
    {
        Camera cam = m_targetCamera.GetComponent<Camera>();
        if (cam == null)
            cam = m_targetCamera.GetComponentInChildren<Camera>();

        // If in camera view.
        bool distanceToCamera = Vector3.Distance(cam.transform.position, transform.position) <= m_interactionDistance;
        bool canUse = CheckInView(cam) && distanceToCamera; // Player can only interact when it's within their view and they are close enough.
        m_interactable = canUse;

        // Handle UI.
        if (canUse && m_uiInstance == null) // Instantiate UI element when the condition is met.
        {
            m_uiInstance = Instantiate(m_interactionPrefab, m_interactionsParent.transform);
        }
        if (!canUse && m_uiInstance != null) // Destroy UI element when the condition isn't met.
        {
            Destroy(m_uiInstance);
        }

        if (m_uiInstance != null)
        {
            // Adjust position by world position.
            Vector3 rootPos = transform.position + m_offset;
            Vector3 uiPos = cam.WorldToScreenPoint(rootPos);
            Vector3 newPos = new Vector3(uiPos.x, uiPos.y, 0.0f);
            m_uiInstance.transform.position = Vector3.Lerp(m_uiInstance.transform.position, newPos, Time.deltaTime * 64.0f);

            // Adjust size to distance.
            Vector2 uiSize = new Vector2(m_interactionObjectSize.x * uiPos.z, m_interactionObjectSize.y * uiPos.z);
            uiSize.x = Mathf.Clamp(uiSize.x, 0.0f, m_interactionObjectSize.x);
            uiSize.y = Mathf.Clamp(uiSize.y, 0.0f, m_interactionObjectSize.y);
            m_uiInstance.transform.localScale = uiSize;
        }
    }

    private void LateUpdate()
    {
        if (m_interacted)
            m_interacted = false; // Set this to fault at the end of frame to make sure any functions using it have been handled.
    }

    /// <summary>
    /// Checks whether the object is within the camera view or not.
    /// </summary>
    /// <param name="cam">A reference to the target camera.</param>
    /// <returns></returns>
    private bool CheckInView(Camera cam)
    {
        var bounds = m_triggerCollider.bounds;

        Vector3 pointOnScreen = cam.WorldToScreenPoint(bounds.center);
        if (pointOnScreen.z < 0) // Is behind the camera.
        {
            return false;
        }

        if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
            (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height)) // Is off screen.
        {
            return false;
        }

        RaycastHit hit;
        if (Physics.Linecast(cam.transform.position, bounds.center, out hit))
        {
            Interaction i = hit.transform.GetComponent<Interaction>();
            if (i == null) // Not in hit object.
            {
                i = hit.transform.GetComponentInParent<Interaction>();
                if (i == null) // Not in hit object parent.
                {
                    i = hit.transform.GetComponentInChildren<Interaction>();
                    if (i == null) // Not in hit object children.
                    {
                        return false;
                    }
                }
            }
            // Is an interactable.

            if (i != this) // Is not the right object.
                return false;
        }

        return true;
    }
}
