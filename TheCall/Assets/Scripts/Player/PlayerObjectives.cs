using Fungus;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour
{
    public Flowchart flowchart;

    [HideInInspector]
    public bool objectiveComplete = false;

    public KeyObjectDescriptor CurrentObjective
    {
        get { return m_currentObjective; }
        set { SetObjective(value); }
    }

    private KeyObjectDescriptor m_currentObjective;

    private void LateUpdate()
    {
    }

    public void SetObjective(KeyObjectDescriptor objective)
    {
        m_currentObjective = objective;
    }

    public bool CheckObjectiveComplete()
    {
        bool complete = objectiveComplete;
        objectiveComplete = false;

        return complete;
    }

    private void OnGUI()
    {
        // [DEBUG]
        // Temporary text to say which is the current objective.
        if (m_currentObjective == null)
            return;

        GUI.Label(new Rect(10, 40, 300, 20), m_currentObjective.objectName);
    }
}
