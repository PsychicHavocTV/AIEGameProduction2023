using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Interaction))]
public class Statue : MonoBehaviour
{
    [SerializeField]
    private int statueIndex = 0;
    [SerializeField]
    private GameObject playerParentRef;
    [SerializeField]
    private GameObject wendigo;
    [SerializeField]
    private GameObject[] wendigoCreatures;
    [SerializeField]
    private CharacterController playerCharacterController;
    [SerializeField]
    private StatueInteract sI;

    [Header("UI References")]
    [SerializeField]
    private TextMeshProUGUI m_gameSavedText;

    private Interaction m_interaction;

    private void Start()
    {
        m_interaction = GetComponent<Interaction>();
    }

    private void Update()
    {
        if (wendigo != GameManager.Instance.activeWendigo && GameManager.Instance.activeWendigo != null)
        {
            wendigo = GameManager.Instance.activeWendigo;
        }

        if (m_interaction.Interactable)
        {
            Debug.Log("Player Can Now Interact.");

            if (m_interaction.Interacted)
            {
                SaveCheckpoint();
                sI.PlayInteractSound();

                if (GameManager.Instance.showSaveText == true)
                {
                    GameManager.Instance.showSaveText = false;
                    StopCoroutine(ShowGameSaveText());
                    if (m_gameSavedText.enabled == false)
                    {
                        StartCoroutine(ShowGameSaveText());
                        GameManager.Instance.showSaveText = false;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Player Can No Longer Interact");
        }
    }

    public void SaveCheckpoint()
    {
        Debug.Log("***SAVING GAME DATA***");
        for (int i = 0; i <= wendigoCreatures.Length - 1; i++)
        {
            GameManager.Instance.wendigoCreatures[i] = wendigoCreatures[i];
        }
        GameManager.Instance.player = playerParentRef;
        GameManager.Instance.UpdateCheckpoint(statueIndex);
        GameManager.Instance.SaveCheckpointData(wendigo, playerParentRef);
        PlayerController pController;
        pController = playerParentRef.GetComponent<PlayerController>();
        if (pController.enabled == false)
        {
            pController.enabled = true;
        }
        GameManager.Instance.showSaveText = true;
    }

    public IEnumerator ShowGameSaveText()
    {
        GameManager.Instance.showSaveText = false;
        m_gameSavedText.enabled = true;
        m_gameSavedText.alpha = 255;
        yield return new WaitForSeconds(1.5f);
        m_gameSavedText.enabled = false;
        StopCoroutine(ShowGameSaveText());
    }
}
