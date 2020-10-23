using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {

    }

    public void ChangeMusic()
    {

    }

    void Update()
    {
        mainCamera.transform.Rotate(0, 3f * Time.deltaTime, 0);
    }

}
