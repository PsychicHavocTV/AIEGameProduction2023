using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    PlayerInput menuInput;

    public PauseController pauseHandler;
    public GameOverMenuController gameOverHandler;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameOver == true || GameManager.Instance.GamePaused == true)
        {
            Debug.Log("Game should pause.");
            if (playerInput.enabled == true)
            {
                playerInput.enabled = false;
            }
            if (menuInput.enabled == false)
            {
                menuInput.enabled = true;
            }
        }
        else
        {
            if (menuInput.enabled == true)
            {
                menuInput.enabled = false;
            }
            if (playerInput.enabled == false)
            {
                playerInput.enabled = true;
            }
        }

        if (GameManager.Instance.GamePaused == true)
        {
            gameOverHandler.enabled = false;
        }
        else if (GameManager.Instance.GameOver == true)
        {
            gameOverHandler.enabled = false;
        }
    }

    private void OnNavigateLeft(InputValue value)
    {

        //pauseHandler.resetColor = false;
        //if (pauseHandler.confirmationWindowShowing == true)
        //{
        //    if (pauseHandler.currentIndex > 0)
        //    {
        //        pauseHandler.confirmationButtons[1].image.color = pauseHandler.confirmationButtons[1].colors.normalColor;
        //        pauseHandler.confirmationButtons[0].image.color = pauseHandler.confirmationButtons[0].colors.highlightedColor;
        //        pauseHandler.currentIndex--;
        //    }
        //}
    }

    private void OnNavigateRight(InputValue value)
    {
        //pauseHandler.resetColor = false;
        //if (pauseHandler.confirmationWindowShowing == true)
        //{
        //    if (pauseHandler.currentIndex < pauseHandler.confirmationButtons.Length - 1)
        //    {
        //        pauseHandler.confirmationButtons[1].image.color = pauseHandler.confirmationButtons[1].colors.selectedColor;
        //        pauseHandler.confirmationButtons[0].image.color = pauseHandler.confirmationButtons[0].colors.highlightedColor;
        //        pauseHandler.currentIndex++;
        //    }
        //}
    }

    private void OnNavigateUP(InputValue value)
    {
        //pauseHandler.resetColor = false;
        //if (Cursor.lockState != CursorLockMode.Locked)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //if (pauseHandler.confirmationWindowShowing == false)
        //{
        //    if (pauseHandler.currentIndex > 0)
        //    {
        //        for (int i = 0; i <= pauseHandler.menuButtons.Length - 1; i++)
        //        {
        //            if (pauseHandler.menuButtons[i] != pauseHandler.menuButtons[pauseHandler.currentIndex - 1])
        //            {
        //                pauseHandler.menuButtons[i].image.color = pauseHandler.menuButtons[i].colors.normalColor;
        //            }
        //            if (pauseHandler.menuButtons[i] == pauseHandler.menuButtons[pauseHandler.currentIndex - 1])
        //            {
        //                pauseHandler.menuButtons[i].image.color = pauseHandler.menuButtons[i].colors.highlightedColor;
        //            }
        //        }
        //        pauseHandler.currentIndex--;
        //    }
        //}
    }

    private void OnNavigateDOWN(InputValue value)
    {
        pauseHandler.resetColor = false;
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (pauseHandler.confirmationWindowShowing == false)
        {
            if (pauseHandler.currentIndex < pauseHandler.menuButtons.Length)
            {
                if (pauseHandler.currentIndex == 0)
                {
                    pauseHandler.resumeButton.image.color = pauseHandler.resumeButton.colors.normalColor;
                    pauseHandler.loadCheckpointButton.image.color = pauseHandler.loadCheckpointButton.colors.highlightedColor;
                    pauseHandler.currentIndex++;
                }

                //for (int i = 0; i < pauseHandler.menuButtons.Length - 1; i++)
                //{
                //    Debug.Log(pauseHandler.menuButtons[i].name);
                //    if (pauseHandler.menuButtons[i] != pauseHandler.menuButtons[pauseHandler.currentIndex + 1])
                //    {
                //        pauseHandler.menuButtons[i].image.color = pauseHandler.menuButtons[i].colors.normalColor;
                //    }
                //    else if (pauseHandler.menuButtons[i] == pauseHandler.menuButtons[pauseHandler.currentIndex + 1])
                //    {
                //        pauseHandler.menuButtons[i].image.color = pauseHandler.menuButtons[i].colors.highlightedColor;
                //    }
                //}
            }
        }
    }


    private void OnConfirm(InputValue value)
    {
        //pauseHandler.resetColor = false;
        //if (Cursor.lockState != CursorLockMode.Locked)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //if (pauseHandler.confirmationWindowShowing == false)
        //{
        //    for (int i = 0; i <= pauseHandler.menuButtons.Length - 1; i++)
        //    {
        //        if (pauseHandler.menuButtons[pauseHandler.currentIndex] == pauseHandler.resumeButton)
        //        {
        //            pauseHandler.ResumeGame();
        //        }
        //        else if (pauseHandler.menuButtons[pauseHandler.currentIndex] == pauseHandler.loadCheckpointButton)
        //        {
        //            pauseHandler.LoadData();
        //        }
        //        else if (pauseHandler.menuButtons[pauseHandler.currentIndex] == pauseHandler.quitButton)
        //        {
        //            pauseHandler.OpenConfirmWindow();
        //        }
        //    }
        //}
        //else if (pauseHandler.confirmationWindowShowing == true)
        //{
        //    if (pauseHandler.currentIndex == 0)
        //    {
        //        pauseHandler.QuitGame();
        //    }
        //    else if (pauseHandler.currentIndex == 1)
        //    {
        //        if (pauseHandler.confirmationWindow.activeSelf == true)
        //        {
        //            pauseHandler.currentIndex = 1;
        //            pauseHandler.confirmationWindow.SetActive(false);
        //            pauseHandler.confirmationWindowShowing = false;
        //        }
        //    }
        //}
    }

    private void OnBack(InputValue value)
    {
        //if (pauseHandler.confirmationWindowShowing == false)
        //{
        //    pauseHandler.ResumeGame();
        //}
        //else if (pauseHandler.confirmationWindowShowing == true)
        //{
        //    pauseHandler.currentIndex = 1;
        //    pauseHandler.confirmationWindow.SetActive(false);
        //    pauseHandler.confirmationWindowShowing = false;
        //}
    }

    private void OnNavigateMouse(InputValue value)
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (pauseHandler.resetColor == false)
        {
                pauseHandler.resumeButton.image.color = pauseHandler.resumeButton.colors.normalColor;
            for (int i = 0; i <= pauseHandler.menuButtons.Length - 1; i++)
            {
                //pauseHandler.menuButtons[i].image.color = pauseHandler.menuButtons[i].colors.normalColor;
            }
            if (pauseHandler.confirmationWindow.activeSelf == true)
            {
                //pauseHandler.confirmationButtons[1].image.color = pauseHandler.confirmationButtons[1].colors.normalColor;
            }
            pauseHandler.resetColor = true;
        }
    }


    private void OnUnpause(InputValue value)
    {
        if (GameManager.Instance.GamePaused == true)
        {
            pauseHandler.ResumeGame();
        }
    }
}
