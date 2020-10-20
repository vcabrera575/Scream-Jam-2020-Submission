using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyMeter : MonoBehaviour
{
    public GameController gameController;
    public Slider candySlider;

    public RectTransform normalFill;
    public RectTransform sickFill;
    public RectTransform speedFill;

    // Update is called once per frame
    void Update()
    {
        // We must reset the value every time, or else the old bar will stay filled.
        candySlider.value = 0;
        if (gameController.candySpeedTimer > 0)
        {
            candySlider.fillRect = speedFill;
        }
        else if (gameController.isSick) // Red bar
        {
            candySlider.fillRect = sickFill;
        }
        else
        {
            candySlider.fillRect = normalFill;
        }

        candySlider.value = gameController.playerFullness;
    }
}
