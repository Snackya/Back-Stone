using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;                // player movement speed
    public int playerNumber;                    // used to assign players to different controls
    [HideInInspector] public bool isDodging = false;

    private bool dodgeOnCooldown = false;

    private Rigidbody2D playerBody;
    private Collider2D playerCollider;
    private SpriteRenderer sprRenderer;
    private Vector3 movementInput;
    private Vector3 movementVelocity;
    private Animator animator;
    private Transform playerSprite;             // To change the players current direction
    private bool facingLeft = true;
    private CapsuleCollider2D attackCollider;

    private float sprintSpeedIncrease = 4f;   // amount of speed increase, when player is sprinting
    private string m_movementAxisKeyboardX;
    private string m_movementAxisKeyboardY;
    private string m_movementAxisGamepadX;
    private string m_movementAxisGamepadY;

    [HideInInspector]
    public bool swordEquipped = true;
    

    void Start()
    {
        // initializing playerBody with the player character
        playerBody = GetComponent<Rigidbody2D>();

        playerSprite = transform.FindChild("sprite");
        attackCollider = transform.FindChild("Sword").GetComponent<CapsuleCollider2D>();

        playerCollider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();

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
        
        Dodge();

        if (swordEquipped)
        {
            SwordAttack();
        }
    }

    void FixedUpdate()
    {
        FreezePlayer();
        Move();
        AnimationController();
    }

    private void FreezePlayer()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player" + playerNumber + "AttackLeft"))
        {
            movementVelocity = new Vector3(0, 0, 0);
        }
    }

    private void AnimationController()
    {
        animator.SetFloat("Speed", Math.Abs(movementVelocity.x));
        animator.SetFloat("vSpeed", Math.Abs(movementVelocity.y));
        if (movementVelocity.x > 0 && facingLeft)
        {
            Flip();
        }
        if (movementVelocity.x < 0 && !facingLeft)
        {
            Flip();
        }
       
    }

    //Dodge-roll with invulnerability
    private void Dodge()
    {
        if (Input.GetButtonDown("Dodge" + playerNumber) && !isDodging && !dodgeOnCooldown)
        {
            animator.SetTrigger("dodgeTrigger");
            dodgeOnCooldown = true;
            StartCoroutine(DodgeCooldownCounter());
        }
    }

    //disable dodging for x seconds
    private IEnumerator DodgeCooldownCounter()
    {
        yield return new WaitForSeconds(5.0f);
        dodgeOnCooldown = false;
    }

    private void Move()
    {
        playerBody.velocity = movementVelocity;
    }

    private void Flip()
    {
        facingLeft = !facingLeft;

        attackCollider.offset = new Vector2(attackCollider.offset.x * (-1), attackCollider.offset.y);

        Vector3 playerScale = playerSprite.localScale;
        playerScale.x *= -1;
        playerSprite.localScale = playerScale;
    }

    private void SwordAttack()
    {
        if (Input.GetButtonDown("Attack" + playerNumber))
        {
            animator.SetTrigger("attackTrigger");
        }
        //Debug.Log(invulnerable);
    }

    public void SwipeAttack()
    {
        animator.SetTrigger("swipeTrigger");
    }
}
