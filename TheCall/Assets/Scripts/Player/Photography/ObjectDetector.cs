using UnityEditor;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public GameObject targetCamera;

    ObjectsInView camObjects;

    Collider thisCollider;

    Plane[] cameraFrustrum;

    void Awake()
    {
        thisCollider = GetComponentInChildren<Collider>();

        camObjects = targetCamera.GetComponentInChildren<ObjectsInView>();
    }

    void OnDisable()
    {
        camObjects.RemoveObject(gameObject);
    }

    void Update()
    {
        DetectObject();
    }

    private void DetectObject()
    {
        if (camObjects == null)
            return;

        Camera cam = targetCamera.GetComponent<Camera>();

        // If in camera view add to List, otherwise remove from list.
        var bounds = thisCollider.bounds;
        cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(cameraFrustrum, bounds))
        {
            camObjects.AddObject(gameObject);
        }
        else
        {
            camObjects.RemoveObject(gameObject);
        }
    }
}
