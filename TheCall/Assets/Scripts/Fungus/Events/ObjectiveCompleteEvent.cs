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
        if (objectiveToCheck == null)
            return; // Don't continue if there's no objective criteria doesn't exist.

        Debug.Log(objective.objectName + " Completed!");

        if (objective.objectiveDescription == objectiveToCheck.objectiveDescription) // If objective is the same as the one to check.
            ExecuteBlock(); // Execute Fungus block.
    }

}
