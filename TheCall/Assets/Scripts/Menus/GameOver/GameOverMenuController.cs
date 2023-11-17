using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    PlayerInput menuInput;
    [SerializeField]
    Button loadButton;
    [SerializeField]
    Button mainMenuButton;

    public GameObject gameOverCanvas;
    bool gameOverShowing = false;
    int currentIndex = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(menuInput.currentControlScheme);
        if (GameManager.Instance.GameOver == true)
        {
            if (gameOverShowing == false)
            {
                if (playerInput.enabled == true)
                {
                    playerInput.enabled = false;
                }
                if (menuInput.enabled == false)
                {
                    menuInput.enabled = true;
                }
                gameOverShowing = true;
                GameOverMenu();
            }
        }
    }

    void GameOverMenu()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (gameOverCanvas.activeSelf == false)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    private void OnNavigateUP(InputValue value)
    {
        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (currentIndex > 0)
        {
            currentIndex--;
            mainMenuButton.image.color = mainMenuButton.colors.normalColor;
            loadButton.image.color = loadButton.colors.highlightedColor;
        }
    }

    private void OnNavigateDOWN(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (currentIndex < 1)
        {
            loadButton.image.color = loadButton.colors.normalColor;
            mainMenuButton.image.color = mainMenuButton.colors.highlightedColor;
            currentIndex++;
        }
    }

    private void OnConfirm(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        switch (currentIndex)
        {
            case 0:
                {
                    Debug.Log("Load most recent checkpoint.");
                    break;
                }
            case 1:
                {
                    Debug.Log("Return to main menu.");
                    break;
                }
        }
    }

    private void OnNavigateMouse(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
