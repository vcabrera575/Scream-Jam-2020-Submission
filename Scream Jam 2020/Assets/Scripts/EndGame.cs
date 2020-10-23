using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static int currentScore;
    public static int highScore;
    public GameController text;

    private void GameEnd()
    {
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //get current score
        currentScore = text.candy;
        //get highscore
        highScore = PlayerPrefs.GetInt("highscore", 0);

        //if beat highscore, replace it
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
        //save current score
        PlayerPrefs.SetInt("score", currentScore);
        //go to win screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private void Update()
    {
        Vector3 player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        float dist = Vector3.Distance(player, transform.position);
        if (dist < 3f)
            GameEnd();
    }
}
