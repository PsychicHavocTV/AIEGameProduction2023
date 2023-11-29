using System;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class KeyObjectDescriptor : MonoBehaviour
{
    // Contains all information and properties for a key object.
    [Tooltip("The name of the object.")]
    public string objectName;

    [Tooltip("The value of the object.")]
    public int objectValue;

    [Tooltip("The description of the objective.")]
    public string objectiveDescription;

    [Range(0f, 100f)]
    [Tooltip("The threshold for how much of the object needs to be taken up on the screen for it to be recognized.")]
    public float objectiveThreshold;

    [HideInInspector]
    public string guid; // The object's unique ID.

    // https://www.gamedeveloper.com/programming/building-a-simple-system-for-persistence-with-unity
    // Unique ID for each object.
#if UNITY_EDITOR
    private void Awake()
    {
        if (String.IsNullOrEmpty(guid)) // Create unique ID.
        {
            //guid = Guid.NewGuid().ToString();
            //GameManager.RegisterInstanceGUID(this.guid, this.GetInstanceID());
            //GameManager.RegisterObjectGUID(this.guid, this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //GameManager.DeregisterGUID(this.guid); // Avoid null referencing.
    }

    private void Update()
    {
        //if (this.GetInstanceID() != GameManager.GetInstanceID(this.guid)) // Create unique ID.
        //{
            //guid = Guid.NewGuid().ToString();
            //GameManager.RegisterInstanceGUID(this.guid, this.GetInstanceID());
            //GameManager.RegisterObjectGUID(this.guid, this.gameObject);
        //}
    }
#endif

}
