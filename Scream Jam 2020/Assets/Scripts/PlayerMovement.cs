using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    public CharacterController controller;
    public GameController gameController;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    bool grounded;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = move.normalized;

        // Check if the player is on the ground
        if (controller.isGrounded && velocity.y < 0)
            grounded = true;
        else
            grounded = false;

        // Gravity
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        // Detects if the player is pressing jump and then jumps
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            grounded = false;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move((move * gameController.playerSpeed * Time.deltaTime) + (velocity * Time.deltaTime));
    }
}
