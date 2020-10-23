
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
        txt.text = "Your Score:" + score.ToString() + "\n" + "The Hiscore: " + hiscore.ToString();
    }
}
