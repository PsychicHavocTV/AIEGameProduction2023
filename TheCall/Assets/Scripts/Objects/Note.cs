using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interaction))]
public class Note : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the player controller.")]
    private PlayerController m_playerController;

    [Header("UI References")]
    [SerializeField, Tooltip("A reference to the sprite to display as the note.")]
    private Sprite m_noteImage;

    [SerializeField, Tooltip("A reference to the notes parent within the player's UI.")]
    private GameObject m_noteParent;

    [SerializeField, Tooltip("A reference to the notes parent within the player's UI.")]
    private GameObject m_notePrefab;

    private Interaction m_interaction; // A reference to the interaction script.

    private bool m_toggle = false;

    private GameObject m_noteInstance = null; // The instance of the prefab.

    private void Start()
    {
        m_interaction = transform.GetComponentInChildren<Interaction>();
    }

    private void Update()
    {
        if (m_interaction.Interacted == true) // Toggle if interacted with.
        {
            NoteToggle();
        }
        if (m_interaction.Interactable == false)
        {
            m_toggle = false;
            if (m_noteInstance != null) // Destroy prefab instance.
            {
                Destroy(m_noteInstance);
            }
        }
    }

    /// <summary>
    /// Toggles the note on screen.
    /// </summary>
    private void NoteToggle()
    {
        m_toggle = !m_toggle; // Toggle.
        if (m_toggle == true)
        {
            if (m_noteInstance == null) // Instantiate note prefab in the UI.
            {
                m_noteInstance = Instantiate(m_notePrefab, m_noteParent.transform);
                if (m_noteImage != null) // Set the note image.
                {
                    var img = m_noteInstance.GetComponentInChildren<Image>();
                    img.sprite = m_noteImage;
                    img.rectTransform.sizeDelta = m_noteImage.rect.size; // Set the correct size.
                    if (img.rectTransform.sizeDelta.y > Screen.height ||
                        img.rectTransform.sizeDelta.x > Screen.width) // Check if the sprite is too big.
                    {
                        img.rectTransform.sizeDelta = m_noteImage.rect.size / 2; // Halve it.
                    }
                }
            }
        }
        else
        {
            if (m_noteInstance != null) // Destroy it.
            {
                Destroy(m_noteInstance);
            }
        }
    }
}
