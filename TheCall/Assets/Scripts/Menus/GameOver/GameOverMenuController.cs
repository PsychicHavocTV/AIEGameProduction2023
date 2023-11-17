using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Security;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    PlayerInput menuInput;
    [SerializeField]
    Button loadButton;
    [SerializeField]
    Button exitButton;
    [SerializeField]
    Button confirmQuitButton;
    [SerializeField]
    Button cancelQuitButton;
    [SerializeField]
    GameObject confirmationWindow;

    public GameObject gameOverCanvas;
    public bool confirmationWindowShowing = false;
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

        if (GameManager.Instance.GamePaused == true)
        {
            if (playerInput.enabled == true)
            {
                playerInput.enabled = false;
            }
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
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
        if (confirmationWindowShowing == false)
        {
            if (currentIndex > 0)
            {
                exitButton.image.color = exitButton.colors.normalColor;
                loadButton.image.color = loadButton.colors.highlightedColor;
                currentIndex--;
            }
        }
        
    }

    private void OnNavigateDOWN(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (confirmationWindowShowing == false)
        {
            if (currentIndex < 1)
            {
                loadButton.image.color = loadButton.colors.normalColor;
                exitButton.image.color = exitButton.colors.highlightedColor;
                currentIndex++;
            }
        }
        
    }

    private void OnNavigateLeft(InputValue value)
    {
        if (confirmationWindowShowing == true)
        {
            if (currentIndex > 0)
            {
                cancelQuitButton.image.color = cancelQuitButton.colors.normalColor;
                confirmQuitButton.image.color = confirmQuitButton.colors.highlightedColor;
                currentIndex--;
            }
        }
    }

    private void OnNavigateRight(InputValue value)
    {
        if (confirmationWindowShowing == true)
        {
            if (currentIndex < 1)
            {
                confirmQuitButton.image.color = confirmQuitButton.colors.normalColor;
                cancelQuitButton.image.color = cancelQuitButton.colors.highlightedColor;
                currentIndex++;
            }
        }
    }

    private void OnConfirm(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (confirmationWindowShowing == false)
        {
            switch (currentIndex)
            {
                case 0:
                    {
                        loadButton.image.color = loadButton.colors.pressedColor;
                        Debug.Log("Load most recent checkpoint.");
                        break;
                    }
                case 1:
                    {
                        exitButton.image.color = exitButton.colors.pressedColor;
                        Debug.Log("Open Confirmation Window.");
                        confirmationWindowShowing = true;
                        if(confirmationWindow.activeSelf == false)
                        {
                            confirmationWindow.SetActive(true);
                            currentIndex = 0;
                        }
                        break;
                    }
            }
        }
        else if (confirmationWindowShowing == true)
        {
            if (currentIndex == 0)
            {
                confirmQuitButton.image.color = confirmQuitButton.colors.pressedColor;
                QuitGame();
            }
            else if (currentIndex == 1)
            {
                if (confirmationWindow.activeSelf == true)
                {
                    currentIndex = 1;
                    confirmationWindow.SetActive(false);
                    confirmationWindowShowing = false;
                }
            }
        }
    }

    private void OnBack(InputValue value)
    {
        if (confirmationWindowShowing == true)
        {
            currentIndex = 1;
            confirmationWindow.SetActive(false);
            confirmationWindowShowing = false;
        }
    }

    private void OnNavigateMouse(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            loadButton.image.color = loadButton.colors.normalColor;
            exitButton.image.color = exitButton.colors.normalColor;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
