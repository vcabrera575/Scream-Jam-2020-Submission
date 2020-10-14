using UnityEngine;
using UnityEngine.UI;

public class InterfaceMessage : MonoBehaviour
{
    public GameController controller;
    public Text message;

    // Update is called once per frame
    void Update()
    {
        message.text = controller.message.ToString();
    }
}
