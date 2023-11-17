using Fungus;
using UnityEngine;

[EventHandlerInfo("Wendigo", 
    "Objective Complete", 
    "The block will execute once an objective is completed.")]
[AddComponentMenu("")]
public class ObjectiveCompleteEvent : EventHandler
{
    [Tooltip("A reference to the objective to be checked that it is completed.")]
    public KeyObjectDescriptor objectiveToCheck;

    public void Complete(KeyObjectDescriptor objective)
    {
        Debug.Log(objective.objectName + " Completed!");

        if (objectiveToCheck == null)
            ExecuteBlock();
        else if (objective.Equals(objectiveToCheck))
            ExecuteBlock();
    }

}
