using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameController controller;
    public Text score;


    // Update is called once per frame
    void Update()
    {
        score.text = "Candies: " + controller.candy.ToString();
    }
}
