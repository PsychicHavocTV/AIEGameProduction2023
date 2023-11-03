using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ObjectsInView : MonoBehaviour
{
    // List of game objects within the camera's frustrum.
    public List<GameObject> objects = new List<GameObject>();

    /// <summary>
    /// Add an object to the list and then sort.
    /// </summary>
    /// <param name="obj">Reference to the game object.</param>
    public void AddObject(GameObject obj)
    {
        if (!objects.Contains(obj)) // Don't add itself if already in list.
        {
            objects.Add(obj);
            SortByDistance(); // Sort.
        }
    }

    /// <summary>
    /// Remove an object from the list then resort.
    /// </summary>
    /// <param name="obj">Reference to the game object.</param>
    public void RemoveObject(GameObject obj)
    {
        if (objects.Count <= 0) // Don't try to remove if list is empty.
            return;

        if (objects.Contains(obj)) // Don't try to remove if not in list.
        {
            obj.layer = 0; // Reset object's layer to default.
            objects.Remove(obj);
            SortByDistance(); // Sort.
        }
    }

    /// <summary>
    /// Sorts the object list by each objects distance to the camera, then set the objects to the correct layer.
    /// </summary>
    private void SortByDistance()
    {
        if (objects.Count <= 0) // Don't try to sort if list is empty.
            return;

        // Sort object list by each objects distance to the camera.
        objects.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position));
        });

        for (int i = objects.Count; i < 0; i--)
        {
            objects[i].layer = 0; // Set all object's layers to default.
        }
        objects[0].layer = 31; // Set closest object to KeyObjects layer.
    }
}
