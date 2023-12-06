using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public PlayerInput menuInput;
    public TextMeshProUGUI pauseMenuHeader;
    public Button resumeButton;
    public Button loadCheckpointButton;
    public Button quitButton;
    public Button confirmationQuit;
    public Button confirmationCancel;
    public GameObject pauseCanvas;
    public GameObject confirmationWindow;

    public Button[] menuButtons;
    public Button[] confirmationButtons;
    public GameObject playerRef;
    public GameObject wendigoRef;
    public bool confirmationWindowShowing;
    public bool pauseMenuShowing = false;
    public bool resetColor = false;
    public int currentIndex;

    private void Start()
    {
        pauseMenuShowing = false;

    }

    private void Update()
    {
        wendigoRef = GameManager.Instance.activeWendigo;
        if (GameManager.Instance.GamePaused == true)
        {
            if (pauseMenuShowing == false)
            {
                pauseMenuShowing = true;
                PauseMenu();
            }
        }
        else if (GameManager.Instance.GamePaused == false)
        {
            if (pauseMenuShowing == true)
            {
                pauseMenuShowing = false;
            }
        }
    }

    public void ResumeGame()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (pauseCanvas.activeSelf == true)
        {
            pauseCanvas.SetActive(false);
        }
        currentIndex = 0;
        GameManager.Instance.GamePaused = false;
    }

    public void LoadData()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        GameManager.Instance.GamePaused = false;
        GameManager.Instance.LoadCheckpointData(wendigoRef, playerRef);
    }

    void PauseMenu()
    {
        Debug.Log("Game should pause.");
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (pauseCanvas.activeSelf == false)
        {
            pauseCanvas.SetActive(true);
        }
    }

    public void OpenConfirmWindow()
    {
        Debug.Log("Open Confirmation Window.");
        confirmationWindowShowing = true;
        if (confirmationWindow.activeSelf == false)
        {
            confirmationWindow.SetActive(true);
            currentIndex = 0;
        }
    }

    public void CloseConfirmWindow()
    {
        Debug.Log("Close Confirmation Window.");
        confirmationWindowShowing = false;
        if (confirmationWindow.activeSelf == true)
        {
            confirmationWindow.SetActive(false);
            for (int i = 0; i <= menuButtons.Length - 1; i++)
            {
                if (menuButtons[i] == quitButton)
                {
                    currentIndex = i;
                    break;
                }
            }
        }
    }

    //private void OnNavigateLeft(InputValue value)
    //{
    //    resetColor = false;
    //    if (confirmationWindowShowing == true)
    //    {
    //        if (currentIndex > 0)
    //        {
    //            confirmationButtons[1].image.color = confirmationButtons[1].colors.normalColor;
    //            confirmationButtons[0].image.color = confirmationButtons[0].colors.highlightedColor;
    //            currentIndex--;
    //        }
    //    }
    //}
    //
    //private void OnNavigateRight(InputValue value)
    //{
    //    resetColor = false;
    //    if (confirmationWindowShowing == true)
    //    {
    //        if (currentIndex < confirmationButtons.Length - 1)
    //        {
    //            confirmationButtons[1].image.color = confirmationButtons[1].colors.selectedColor;
    //            confirmationButtons[0].image.color = confirmationButtons[0].colors.highlightedColor;
    //            currentIndex++;
    //        }
    //    }
    //}
    //
    //private void OnNavigateUP(InputValue value)
    //{
    //    resetColor = false;
    //    if (Cursor.lockState != CursorLockMode.Locked)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    if (confirmationWindowShowing == false)
    //    {
    //        if (currentIndex > 0)
    //        {
    //            for (int i = 0; i <= menuButtons.Length - 1; i++)
    //            {
    //                if (menuButtons[i] != menuButtons[currentIndex - 1])
    //                {
    //                    menuButtons[i].image.color = menuButtons[i].colors.normalColor;
    //                }
    //                if (menuButtons[i] == menuButtons[currentIndex - 1])
    //                {
    //                    menuButtons[i].image.color = menuButtons[i].colors.highlightedColor;
    //                }
    //            }
    //            currentIndex--;
    //        }
    //    }
    //}
    //
    //private void OnNavigateDOWN(InputValue value)
    //{
    //    resetColor = false;
    //    if (Cursor.lockState != CursorLockMode.Locked)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    if (confirmationWindowShowing == false)
    //    {
    //        if (currentIndex < menuButtons.Length)
    //        {
    //            for (int i = 0; i < menuButtons.Length - 1; i++)
    //            {
    //                Debug.Log(menuButtons[i].name);
    //                if (menuButtons[i] != menuButtons[currentIndex + 1])
    //                {
    //                    menuButtons[i].image.color = menuButtons[i].colors.normalColor;
    //                }
    //                else if (menuButtons[i] == menuButtons[currentIndex + 1])
    //                {
    //                    menuButtons[i].image.color = menuButtons[i].colors.highlightedColor;
    //                }
    //            }
    //            currentIndex++;
    //        }
    //    }
    //}
    //
    //
    //private void OnConfirm(InputValue value)
    //{
    //    resetColor = false;
    //    if (Cursor.lockState != CursorLockMode.Locked)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    if (confirmationWindowShowing == false)
    //    {
    //        for (int i = 0; i <= menuButtons.Length - 1; i++)
    //        {
    //            if (menuButtons[currentIndex] == resumeButton)
    //            {
    //                ResumeGame();
    //            }
    //            else if (menuButtons[currentIndex] == loadCheckpointButton)
    //            {
    //                LoadData();
    //            }
    //            else if (menuButtons[currentIndex] == quitButton)
    //            {
    //                OpenConfirmWindow();
    //            }
    //        }
    //    }
    //    else if (confirmationWindowShowing == true)
    //    {
    //        if (currentIndex == 0)
    //        {
    //            QuitGame();
    //        }
    //        else if (currentIndex == 1)
    //        {
    //            if (confirmationWindow.activeSelf == true)
    //            {
    //                currentIndex = 1;
    //                confirmationWindow.SetActive(false);
    //                confirmationWindowShowing = false;
    //            }
    //        }
    //    }
    //}
    //
    //private void OnBack(InputValue value)
    //{
    //    if (confirmationWindowShowing == false)
    //    {
    //        ResumeGame();
    //    }
    //    else if (confirmationWindowShowing == true)
    //    {
    //        currentIndex = 1;
    //        confirmationWindow.SetActive(false);
    //        confirmationWindowShowing = false;
    //    }
    //}
    //
    //private void OnNavigateMouse(InputValue value)
    //{
    //    if (Cursor.lockState != CursorLockMode.None)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //    if (resetColor == false)
    //    {
    //        for (int i = 0; i <= menuButtons.Length - 1; i++)
    //        {
    //            menuButtons[i].image.color = menuButtons[i].colors.normalColor;
    //        }
    //        if (confirmationWindow.activeSelf == true)
    //        {
    //            confirmationButtons[0].image.color = confirmationButtons[0].colors.normalColor;
    //            confirmationButtons[1].image.color = confirmationButtons[1].colors.normalColor;
    //        }
    //        resetColor = true;
    //    }
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}
