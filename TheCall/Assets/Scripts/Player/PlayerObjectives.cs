using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("A reference to the objectives in the player's UI.")]
    public GameObject objectivesUI;

    [Tooltip("A reference to the objectives panel in the player's UI.")]
    public GameObject objectivesPanel;

    [Tooltip("A reference to a objective prefab for the player's UI.")]
    public GameObject objectivePrefab;

    public List<KeyObjectDescriptor> CurrentObjectives
    {
        get { return m_currentObjectives; }
    }
    private static List<KeyObjectDescriptor> m_currentObjectives = new List<KeyObjectDescriptor>(); // List of current objectives.

    public List<KeyObjectDescriptor> CompletedObjectives
    {
        get { return m_completedObjectives; }
    }
    private static List<KeyObjectDescriptor> m_completedObjectives = new List<KeyObjectDescriptor>(); // List of completed objectives.

    /// <summary>
    /// Adds a specified objective to the current objectives list.
    /// </summary>
    /// <param name="objective">A reference to the objective.</param>
    public void AddObjective(KeyObjectDescriptor objective)
    {
        m_currentObjectives.Add(objective); // Add to list.

        // UI Stuff.
        objectivesUI.SetActive(true); // Make sure it's all active.
        if (m_currentObjectives.Count > 0)
            objectivesPanel.SetActive(true); // Make panel active if objectives exist.

        var newObjective = Instantiate(objectivePrefab, objectivesPanel.transform); // Add new objective to UI.
        newObjective.GetComponentInChildren<TextMeshProUGUI>().text = objective.objectiveDescription; // Set text.
    }

    /// <summary>
    /// Removes the specified objective from the current objectives list.
    /// </summary>
    /// <param name="objective">A reference to the objective.</param>
    public void RemoveObjective(KeyObjectDescriptor objective)
    {
        if (m_currentObjectives.Count <= 0) // Don't try anything if there's nothing to even remove.
            return;

        m_currentObjectives.Remove(objective); // Remove from list.

        // UI Stuff.
        var objectives = objectivesPanel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var o in objectives) // Iterate through each objective in UI.
        {
            if (o.text == objective.objectiveDescription) // If it matches.
            {
                Transform p = o.transform.parent;
                // Animate it, and remove it from the UI.
                Vector3 newPos = new Vector3(-512f, p.position.y);
                iTween.MoveTo(p.gameObject, iTween.Hash("position", newPos, "islocal", false, "time", 1.0f, 
                    "oncompletetarget", gameObject, "oncomplete", "DestroyObjectOnComplete", 
                    "oncompleteparams", iTween.Hash("object", o.gameObject, "parent", p.gameObject)));
            }
        }

        if (m_currentObjectives.Count <= 0)
            objectivesPanel.SetActive(false); // Disable panel if no objectives.
    }

    /// <summary>
    /// Adds the objective to the completed objectives list and then removes it from the current objectives list.
    /// </summary>
    /// <param name="objective">A reference to the objective.</param>
    public void CompleteObjective(KeyObjectDescriptor objective)
    {
        m_completedObjectives.Add(objective);
        RemoveObjective(objective);
    }

    /// <summary>
    /// [Don't use] Called by iTween to destroy the object after an animation completes.
    /// </summary>
    public void DestroyObjectOnComplete(object parameters)
    {
        Hashtable par = (Hashtable)parameters;
        Debug.Log("Destroyed " + (GameObject)par["object"]);
        Destroy((GameObject)par["object"]);
        Destroy((GameObject)par["parent"]);
    }

    private void OnGUI()
    {
        // [DEBUG]
        // Temporary text to say which is the current objective.
        if (m_currentObjectives.Count <= 0)
            return;

        //GUI.Label(new Rect(10, 40, 300, 20), m_currentObjectives[0].objectName);
    }
}
