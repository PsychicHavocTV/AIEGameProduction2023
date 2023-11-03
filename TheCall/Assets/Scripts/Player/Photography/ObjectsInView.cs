using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ObjectsInView : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    public void AddObject(GameObject obj)
    {
        if (!objects.Contains(obj)) // Don't add itself if already in list.
        {
            objects.Add(obj);
            SortByDistance();
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (objects.Count <= 0)
            return;

        if (objects.Contains(obj)) // Don't try to remove if not in list.
        {
            obj.layer = 0;
            objects.Remove(obj);
            SortByDistance();
        }
    }

    void SortByDistance()
    {
        if (objects.Count <= 0)
            return;

        // Sort by distance to camera.
        objects.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position));
        });

        for (int i = objects.Count; i < 0; i--)
        {
            objects[i].layer = 0;
        }
        objects[0].layer = 31;
    }
}
