using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public Text highScoreText;
    int highScore = 0;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //get highscore
        highScore = PlayerPrefs.GetInt("highscore", 0);

        highScoreText.text = "High Score: " + highScore;
        mainCamera = Camera.main;
    }

    public void ChangeMusic()
    {

    }


    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.Rotate(0, 3f * Time.deltaTime, 0);

        //get highscore
        highScore = PlayerPrefs.GetInt("highscore", 0);

        highScoreText.text = "High Score: " + highScore;
        
    }
}
