using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;                // player movement speed
    public int playerNumber;                    // used to assign players to different controls

    private float originalMoveSpeed;
    private float sprintSpeedIncrease = 4f;   // amount of speed increase, when player is sprinting
    private Rigidbody2D playerBody;
    private Vector3 movementInput;
    private Vector3 movementVelocity;
    private string m_movementAxisKeyboardX;
    private string m_movementAxisKeyboardY;
    private string m_movementAxisGamepadX;
    private string m_movementAxisGamepadY;


    void Start()
    {
        // initializing playerBody with the player character
        playerBody = GetComponent<Rigidbody2D>();
        // setting orginalMoveSpeed to the movement speed given in unity
        originalMoveSpeed = moveSpeed;

        m_movementAxisKeyboardX = "Horizontal" + playerNumber;
        m_movementAxisKeyboardY = "Vertical" + playerNumber;
        m_movementAxisGamepadX = "GamepadHorizontal" + playerNumber;
        m_movementAxisGamepadY = "GamepadVertical" + playerNumber;

    }

	void Update()
    {
        // storing the user movement input in a variable
        movementInput = new Vector2(Input.GetAxis(m_movementAxisKeyboardX), Input.GetAxis(m_movementAxisKeyboardY));
        //movementInput = new Vector2(Input.GetAxis(m_movementAxisGamepadX), Input.GetAxis(m_movementAxisGamepadY));
	}

    void FixedUpdate()
    {
        Sprint();
        Move();
    }

    private void Sprint()
    {
        /* increasing the movement speed by small amount when player holds down sprint button
         * and resetting it to the original value, when player releases the sprint button */
        if (Input.GetButtonDown("Sprint" + playerNumber))
        {
            moveSpeed += sprintSpeedIncrease;
        }
        else if (Input.GetButtonUp("Sprint" + playerNumber))
        {
            moveSpeed = originalMoveSpeed;
        }
    }

    private void Move()
    {
        Vector2 movement = (transform.forward + movementInput) * moveSpeed * Time.deltaTime;
        playerBody.MovePosition(playerBody.position + movement);
    }
}
