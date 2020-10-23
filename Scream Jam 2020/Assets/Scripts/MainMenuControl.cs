using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public MainMenu creditButton;
    public MainMenu quitCreditButton;
    public Text highScoreText;
    public Canvas credits;
    public Canvas mainMenu;
    int highScore = 0;
    bool creditsShowing = false;

    public Camera mainCamera;
    public Transform follower;

    // Start is called before the first frame update
    void Start()
    {
        //get highscore
        highScore = PlayerPrefs.GetInt("highscore", 0);

        highScoreText.text = "High Score: " + highScore;
        mainCamera = Camera.main;
    }

    public void ToggleCredits()
    {
        if (!creditsShowing)
        {
            credits.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
            creditsShowing = true;
        }
        else
        {
            credits.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            creditsShowing = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.Rotate(0, 3f * Time.deltaTime, 0);

        //get highscore
        highScore = PlayerPrefs.GetInt("highscore", 0);

        highScoreText.text = "High Score: " + highScore;

        if (creditButton.showCredits)
        {
            // Show credits
            credits.gameObject.SetActive(true);
        }

    }
}
