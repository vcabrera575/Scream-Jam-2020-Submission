using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InterfaceMessage : MonoBehaviour
{
    public GameController gameController;
    public Text message;

    public float displayTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        message.text = gameController.message.ToString();

        // If the message was recently changed, chagne 
        if (gameController.messageChanged == true)
        {
            displayTimer = gameController.messageOnScreenTimer;
            gameController.messageChanged = false;
        }

        // If things were changed, crossfade the screen out and then reset the message text
        if (displayTimer <= 0)
        {
            message.CrossFadeAlpha(0, gameController.messageOnScreenTimer, false);
            message.text = "";
            message.canvasRenderer.SetAlpha(1f);
        }
        else 
            displayTimer -= Time.deltaTime; 
    }

}
