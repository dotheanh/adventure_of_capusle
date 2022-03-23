using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public GameObject PlayerGraphic;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform mainCamera;

    public Joystick joystick;

    public Joybutton joybutton;
    protected bool jump;
    private float jumpHeight = 1.0f;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (!jump && joybutton.Pressed) {
            jump = true;
        }
        else if (jump && !joybutton.Pressed) {
            jump = false;
        }

        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)    // direction.magnitude = length of vector, use to prevent micro move noising
        {
            // rotate character to target angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(PlayerGraphic.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            PlayerGraphic.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        // Changes the height position of the player..
        if (jump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
