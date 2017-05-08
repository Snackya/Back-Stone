using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;                // player movement speed
    public int playerNumber;                    // used to assign players to different controls
    public float knockbackPower = 1000f;
    
    private Rigidbody2D playerBody;
    private Vector3 movementInput;
    private Vector3 movementVelocity;

    private float originalMoveSpeed;
    private float sprintSpeedIncrease = 4f;   // amount of speed increase, when player is sprinting
    private string m_movementAxisKeyboardX;
    private string m_movementAxisKeyboardY;
    private string m_movementAxisGamepadX;
    private string m_movementAxisGamepadY;

    // Testing
    [SerializeField]
    private Stat health;

    void Awake()
    {
        health.Initialize();
    }

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
        movementInput = new Vector3(Input.GetAxis(m_movementAxisKeyboardX), Input.GetAxis(m_movementAxisKeyboardY), 0f);  //keyboard control
        //movementInput = new Vector3(Input.GetAxis(m_movementAxisGamepadX), Input.GetAxis(m_movementAxisGamepadY), 0f);  //gamepad control
        movementVelocity = movementInput * moveSpeed;
        Attack();
        Sprint();
        // Testing
        // CheckIfDead();
	}

    private void CheckIfDead()
    {
        if (health.CurrentVal == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
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
        //Vector3 movement = (movementInput * moveSpeed);
        playerBody.velocity = movementVelocity;
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Attack" + playerNumber))
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetTrigger("attackTrigger");
        }
        
    }

    //player gets hit by an enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Bepis");
            Vector3 knockback = (transform.position - collision.transform.position);

            // Testing
            health.CurrentVal -= 10;

            float enemyKnockbackPower = 300f;
            //knock both characters back
            playerBody.AddForce(knockback * knockbackPower);
            collision.gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(-knockback * enemyKnockbackPower);
            //other.attachedRigidbody.AddForce(-knockback * enemyKnockbackPower);
        }
    }
}
