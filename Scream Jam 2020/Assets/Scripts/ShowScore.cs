
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        int hiscore = PlayerPrefs.GetInt("highscore", 0);
        if (score == 1)
            txt.text = "You got " + score.ToString() + " candy! \n";
        else
            txt.text = "You got " + score.ToString() + " candies!\n";
            
        txt.text += "The Highscore: " + hiscore.ToString();

        if (score == hiscore)
            txt.text += "\nNew High Score!!";
    }
}
