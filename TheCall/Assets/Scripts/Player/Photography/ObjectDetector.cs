using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [Tooltip("Reference to the target camera. usually the player's camera.")]
    public GameObject targetCamera;

    private ObjectsInView m_camObjects; // Reference to target camera's ObjectsInView script.
    private Collider m_thisCollider; // Reference to the object's collider.

    private Plane[] m_cameraFrustrum; // Contains calculated frustrum planes from the target camera.

    private void Start()
    {
        m_thisCollider = GetComponentInChildren<Collider>(); // Get reference to the collider.

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

        // If in camera view add to List, otherwise remove from list.
        var bounds = m_thisCollider.bounds;
        m_cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(m_cameraFrustrum, bounds)) // Test whether object's bounds is within camera frustrum.
        {
            m_camObjects.AddObject(gameObject); // Add itself to target camera's object list.
        }
        else
        {
            m_camObjects.RemoveObject(gameObject); // Remove itself from target camera's object list.
        }
    }
}
