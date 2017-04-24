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

	void Start()
    {
        // initializing playerBody with the player character
        playerBody = GetComponent<Rigidbody2D>();
        // setting orginalMoveSpeed to the movement speed given in unity
        originalMoveSpeed = moveSpeed;
	}

	void Update()
    {
        // storing the user movement input in a variable
        movementInput = new Vector3(Input.GetAxis("Horizontal" + playerNumber), Input.GetAxis("Vertical" + playerNumber), 0f);
        // calculating the movement velocity by multiplying the movement speed with the user input
        movementVelocity = movementInput * moveSpeed;
        // check if player is sprinting
        Sprint();
	}

    void FixedUpdate()
    {
        // applying the calculated velocity to the player
        playerBody.velocity = movementVelocity;
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
}
