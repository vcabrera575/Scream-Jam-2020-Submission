using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public bool showCredits = false;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        GetComponent<MainMenu>().enabled = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        mainCamera.GetComponent<MainMenuControl>().ToggleCredits();
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }

}
