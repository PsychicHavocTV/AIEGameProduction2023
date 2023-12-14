using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [Tooltip("Reference to the target camera. usually the player's camera.")]
    public GameObject targetCamera;

    private ObjectsInView m_camObjects; // Reference to target camera's ObjectsInView script.
    private Collider m_thisCollider; // Reference to the object's collider.

    private void Start()
    {
        m_thisCollider = GetComponent<Collider>(); // Get reference to the collider.
        if (m_thisCollider == null)
            m_thisCollider = GetComponentInChildren<Collider>();

        m_camObjects = targetCamera.GetComponentInChildren<ObjectsInView>(); // Get reference to camera's ObjectsInView script.
    }

    private void OnDisable()
    {
        if (m_camObjects == null) // Return if camera does not have the ObjectsInView script.
            return;

        m_camObjects.RemoveObject(gameObject); // Remove itself when disabled.
    }

    private void Update()
    {
        DetectObject();
    }

    /// <summary>
    /// Test whether object is within camera frustrum and add/remove itself from the camera's list.
    /// </summary>
    private void DetectObject()
    {
        if (m_camObjects == null) // Return if camera does not have the ObjectsInView script.
            return;

        Camera cam = targetCamera.GetComponent<Camera>();
        if (cam == null)
            cam = targetCamera.GetComponentInChildren<Camera>();

        // If in camera view add to List, otherwise remove from list.
        if (CheckInView(cam)) // Test whether object's bounds is within camera frustrum.
        {
            m_camObjects.AddObject(gameObject); // Add itself to target camera's object list.
        }
        else
        {
            m_camObjects.RemoveObject(gameObject); // Remove itself from target camera's object list.
        }
    }

    /// <summary>
    /// Checks whether the object is within the camera view or not.
    /// </summary>
    /// <param name="cam">A reference to the target camera.</param>
    /// <returns></returns>
    private bool CheckInView(Camera cam)
    {
        var bounds = m_thisCollider.bounds;

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
            ObjectDetector i = hit.transform.GetComponent<ObjectDetector>();
            if (i == null) // Not in hit object.
            {
                i = hit.transform.GetComponentInParent<ObjectDetector>();
                if (i == null) // Not in hit object parent.
                {
                    i = hit.transform.GetComponentInChildren<ObjectDetector>();
                    if (i == null) // Not in hit object children.
                    {
                        return false; // Is not a key object.
                    }
                }
            }
            // Is an key object.

            if (i != this) // Is not the right object.
                return false;
        }

        return true;
    }
}
