using UnityEngine;

public class KeyObjectDescriptor : MonoBehaviour
{
    // Contains all information and properties for a key object.
    [Tooltip("The name of the object.")]
    public string objectName;

    [Tooltip("The description of the object.")]
    public string objectDescription;

    [Tooltip("The value of the object.")]
    public int objectValue;

    [Tooltip("The threshold for how much of the object needs to be taken up on the screen for it to be recognized.")]
    public float objectThreshold;

}
