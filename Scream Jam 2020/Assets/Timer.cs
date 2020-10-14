using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{
    public GameController controller;
    public Text score;

    // Update is called once per frame
    void Update()
    {
        score.text = "Timer: " + Mathf.Round(controller.gameTimer).ToString();
    }
}

