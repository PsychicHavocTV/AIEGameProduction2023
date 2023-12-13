using UnityEngine;

[RequireComponent(typeof(Interaction))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_playerController;

    private Interaction m_interaction;

    void Start()
    {
        m_interaction = GetComponent<Interaction>();
    }

    void Update()
    {
        if (m_interaction.Interacted)
        {
            m_playerController.crowbar = true;
            if (m_interaction.m_uiInstance != null)
                Destroy(m_interaction.m_uiInstance);
            Destroy(gameObject);
        }
    }
}
