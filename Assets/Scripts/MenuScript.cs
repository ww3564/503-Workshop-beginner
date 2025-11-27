using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject quitButton;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Pause(InputAction.CallbackContext inputData)
    {
        PauseFunction();
    }

    public void ResumeButton()
    {
        PauseFunction();
    }

    private void PauseFunction()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            resumeButton.SetActive(false);
            quitButton.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Time.timeScale = 0f;
            resumeButton.SetActive(true);
            quitButton.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
