using System;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(GuidComponent))]
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

    [Tooltip("A reference to the sprite to display as the icon for the objective.")]
    public Sprite objectiveIcon;

}
