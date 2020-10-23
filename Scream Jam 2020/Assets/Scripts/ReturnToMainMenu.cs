using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    float Timer = 10f;

    void Update()
    {
        // Timer runs out, player clicked, pressed escape or jump, so quit to menu.
        if (Timer <= 0 || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(0);
        }

        Timer -= Time.deltaTime;
    }
}
