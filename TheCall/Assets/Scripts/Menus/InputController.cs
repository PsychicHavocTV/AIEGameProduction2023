using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
            if (playerInput.enabled == true)
            {
                playerInput.enabled = false;
            }
            if (menuInput.enabled == false)
            {
                menuInput.enabled = true;
                Debug.Log("Game should pause.");
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

        if (GameManager.Instance.GameOver == true)
        {
            pauseHandler.enabled = false;
            gameOverHandler.enabled = true;
        }
        if (GameManager.Instance.GamePaused == true)
        {
            gameOverHandler.enabled = false;
            pauseHandler.enabled = true;
        }
    }


    private void OnNavigateUP(InputValue value)
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (GameManager.Instance.GamePaused == true)
        {
            //pauseHandler.resetColor = false;
            if (pauseHandler.confirmationWindowShowing == false)
            {
                if (pauseHandler.currentIndex > 0)
                {
                    switch(pauseHandler.currentIndex)
                    {
                        case 2:
                            {
                                pauseHandler.quitButton.image.color = pauseHandler.quitButton.colors.normalColor;
                                pauseHandler.loadCheckpointButton.image.color = pauseHandler.loadCheckpointButton.colors.highlightedColor;
                                pauseHandler.currentIndex--;
                                break;
                            }
                        case 1:
                            {
                                pauseHandler.loadCheckpointButton.image.color = pauseHandler.loadCheckpointButton.colors.normalColor;
                                pauseHandler.resumeButton.image.color = pauseHandler.resumeButton.colors.highlightedColor;
                                pauseHandler.currentIndex--;
                                break;
                            }
                    }
                }
            }
            if (GameManager.Instance.GameOver == true)
            {
                if (gameOverHandler.currentIndex > 0)
                {
                    Debug.Log("Going Up.");
                    switch (gameOverHandler.currentIndex)
                    {
                        case 1:
                            {
                                if (gameOverHandler.loadButton.enabled == true)
                                {
                                    gameOverHandler.loadButton.image.color = gameOverHandler.loadButton.colors.highlightedColor;
                                    gameOverHandler.exitButton.image.color = gameOverHandler.exitButton.colors.normalColor;
                                    gameOverHandler.currentIndex--;
                                }
                                else
                                {
                                    gameOverHandler.exitButton.image.color = gameOverHandler.exitButton.colors.highlightedColor;
                                    gameOverHandler.currentIndex = 1;
                                }
                                break;
                            }
                        case 0:
                            {
                                gameOverHandler.currentIndex = 0;
                                if (gameOverHandler.loadButton.enabled == true)
                                {
                                    gameOverHandler.loadButton.image.color = gameOverHandler.loadButton.colors.highlightedColor;
                                }
                                else
                                {
                                    gameOverHandler.exitButton.image.color = gameOverHandler.exitButton.colors.highlightedColor;
                                }
                                break;
                            }
                    }
                }
            }
        }
    }

    private void OnNavigateDOWN(InputValue value)
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (GameManager.Instance.GamePaused == true)
        {
            pauseHandler.resetColor = false;
            //if (pauseHandler.confirmationWindowShowing == false)
            //{
                if (pauseHandler.currentIndex < 2)
                {
                    Debug.Log("Going Down.");
                    switch (pauseHandler.currentIndex)
                    {
                        case 0:
                            {
                                pauseHandler.resumeButton.image.color = pauseHandler.resumeButton.colors.normalColor;
                                pauseHandler.loadCheckpointButton.image.color = pauseHandler.loadCheckpointButton.colors.highlightedColor;
                                pauseHandler.currentIndex++;
                                break;
                            }
                        case 1:
                            {
                                pauseHandler.loadCheckpointButton.image.color = pauseHandler.loadCheckpointButton.colors.normalColor;
                                pauseHandler.quitButton.image.color = pauseHandler.quitButton.colors.highlightedColor;
                                pauseHandler.currentIndex++;
                                break;
                            }
                    }
                }
            //}
        }
        if (GameManager.Instance.GameOver == true)
        {
            if (gameOverHandler.currentIndex < 2)
            {
                switch (gameOverHandler.currentIndex)
                {
                    case 0:
                        {
                            if (gameOverHandler.loadButton.enabled == true)
                            {
                                gameOverHandler.loadButton.image.color = gameOverHandler.loadButton.colors.normalColor;
                                gameOverHandler.exitButton.image.color = gameOverHandler.exitButton.colors.highlightedColor;
                                gameOverHandler.currentIndex++;
                            }
                            else
                            {
                                gameOverHandler.currentIndex = 0;
                                gameOverHandler.exitButton.image.color = gameOverHandler.exitButton.colors.highlightedColor;
                            }
                            break;
                        }
                }
            }
        }
    }


    private void OnConfirm(InputValue value)
    {
        //pauseHandler.resetColor = false;
        if (GameManager.Instance.GamePaused == true)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (pauseHandler.confirmationWindowShowing == false)
            {
                switch (pauseHandler.currentIndex)
                {
                    case 0:
                        {
                            pauseHandler.ResumeGame();
                            break;
                        }
                    case 1:
                        {
                            pauseHandler.LoadData();
                            break;
                        }
                    case 2:
                        {
                            pauseHandler.QuitGame();
                            break;
                        }
                }
            }
        }
        else if (GameManager.Instance.GameOver == true)
        {
            Button loadButton = null;
            GameObject confirmWindow = null;
            loadButton = gameOverHandler.GetLoadButton(loadButton);
            confirmWindow = gameOverHandler.GetConfirmWindow(confirmWindow);
            Cursor.lockState = CursorLockMode.Locked;
            //if (gameOverHandler.confirmationWindowShowing == false)
            //{
            //}
            switch (gameOverHandler.currentIndex)
            {
                case 0:
                    {
                        
                        if (loadButton.enabled == true)
                        {
                            gameOverHandler.LoadData();
                        }
                        else if (loadButton.enabled == false)
                        {
                            gameOverHandler.QuitGame();
                        }
                        break;
                    }
                case 1:
                    {
                        if (loadButton.enabled == true)
                        {
                            gameOverHandler.QuitGame();
                        }
                        else if (loadButton.enabled == false)
                        {
                            gameOverHandler.currentIndex = 0;
                        }
                        break;
                    }
            }
            //else if (gameOverHandler.confirmationWindowShowing == true)
            //{
            //    switch (gameOverHandler.currentIndex)
            //    {
            //        case 0:
            //            {
            //                
            //                break;
            //            }
            //    }
            //}
        }
        ////else if (pauseHandler.confirmationWindowShowing == true)
        ////{
        ////    if (pauseHandler.currentIndex == 0)
        ////    {
        ////        pauseHandler.QuitGame();
        ////    }
        ////    else if (pauseHandler.currentIndex == 1)
        ////    {
        ////        if (pauseHandler.confirmationWindow.activeSelf == true)
        ////        {
        ////            pauseHandler.currentIndex = 1;
        ////            pauseHandler.confirmationWindow.SetActive(false);
        ////            pauseHandler.confirmationWindowShowing = false;
        ////        }
        ////    }
        ////}
    }
    private void OnNavigateLeft(InputValue value)
    {
    }

    private void OnNavigateRight(InputValue value)
    {
    }

    private void OnBack(InputValue value)
    {
        if (GameManager.Instance.GamePaused == true)
        {
            if (pauseHandler.confirmationWindowShowing == false)
            {
                pauseHandler.ResumeGame();
            }
        }
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
        if (GameManager.Instance.GamePaused == true)
        {
            pauseHandler.resumeButton.image.color = pauseHandler.resumeButton.colors.normalColor;
            pauseHandler.loadCheckpointButton.image.color = pauseHandler.loadCheckpointButton.colors.normalColor;
            pauseHandler.quitButton.image.color = pauseHandler.quitButton.colors.normalColor;

        }
        else if (GameManager.Instance.GameOver == true)
        {
            if (gameOverHandler.loadButton.enabled == true)
            {
                gameOverHandler.loadButton.image.color = gameOverHandler.loadButton.colors.normalColor;
            }
            gameOverHandler.exitButton.image.color = gameOverHandler.exitButton.colors.normalColor;
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
