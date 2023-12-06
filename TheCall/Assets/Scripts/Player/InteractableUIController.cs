using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUIController : MonoBehaviour
{
    public Image interactableUIIcon;
    public TextMeshProUGUI gameSavedText;
    public TextMeshProUGUI interactionText;
    public HideController hidingController;

    private void Start()
    {
        gameSavedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.atStatue == false && hidingController.isHidden == false && GameManager.Instance.atHidingSpot == false)
        {
            interactableUIIcon.enabled = false;
            interactionText.enabled = false;
        }
        else if (GameManager.Instance.atHidingSpot == true)
        {
            interactionText.enabled = true;
            interactableUIIcon.enabled = true;
            interactionText.text = "Press (INTERACT) to Hide";
            interactableUIIcon.color = Color.green;
        }
        else if (hidingController.isHidden == true)
        {
            interactionText.enabled = true;
            interactableUIIcon.enabled = true;
            interactionText.text = "Press (INTERACT) to Stop Hiding";
            interactableUIIcon.color = Color.blue;
        }
        else if (GameManager.Instance.atStatue == true)
        {
            interactionText.enabled = true;
            interactableUIIcon.enabled = true;
            interactionText.text = "Press (INTERACT) to Save Checkpoint";
            interactableUIIcon.color = Color.cyan;
        }

        if (GameManager.Instance.showSaveText == true)
        {
            GameManager.Instance.showSaveText = false;
            StopCoroutine(ShowGameSaveText());
            StartCoroutine(ShowGameSaveText());
        }
    }

    public IEnumerator ShowGameSaveText()
    {
        if (gameSavedText.alpha != 255 && gameSavedText.enabled == false)
        {
            gameSavedText.alpha = 255;
        }
        gameSavedText.enabled = true;
        gameSavedText.alpha = 255;
        yield return new WaitForSeconds(0.5f);
        gameSavedText.CrossFadeAlpha(0, 1.5f, false);
        yield return new WaitForSeconds(1.5f);
        gameSavedText.alpha = 255;
        gameSavedText.enabled = false;
        GameManager.Instance.showSaveText = false;
        StopCoroutine(ShowGameSaveText());
    }
}
