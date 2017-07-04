using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;                // player movement speed
    public int playerNumber;                    // used to assign players to different controls
    public float knockbackPower = 1000f;
    public float maxInvTime = 1.6f;
    [HideInInspector] public bool isDodging = false;

    private bool invulnerable;
    private bool dodgeOnCooldown = false;
    private float currentInvTime;

    private Rigidbody2D playerBody;
    private Collider2D playerCollider;
    private SpriteRenderer sprRenderer;
    private Vector3 movementInput;
    private Vector3 movementVelocity;
    private Animator animator;
    private Transform playerSprite;             // To change the players current direction
    private bool facingLeft = true;
    private CapsuleCollider2D attackCollider;

    private float originalMoveSpeed;
    private float sprintSpeedIncrease = 4f;   // amount of speed increase, when player is sprinting
    private string m_movementAxisKeyboardX;
    private string m_movementAxisKeyboardY;
    private string m_movementAxisGamepadX;
    private string m_movementAxisGamepadY;

    [HideInInspector]
    public bool swordEquipped = true;
    private SwipeAttack swipeAttack;
    /**
    Testing
    [SerializeField]
    private Stat health;
    **/

    void Start()
    {
        // initializing playerBody with the player character
        playerBody = GetComponent<Rigidbody2D>();

        playerSprite = transform.FindChild("sprite");
        attackCollider = GetComponentInChildren<CapsuleCollider2D>();

        playerCollider = GetComponent<Collider2D>();
        // setting orginalMoveSpeed to the movement speed given in unity
        originalMoveSpeed = moveSpeed;

        animator = GetComponent<Animator>();

        m_movementAxisKeyboardX = "Horizontal" + playerNumber;
        m_movementAxisKeyboardY = "Vertical" + playerNumber;
        m_movementAxisGamepadX = "GamepadHorizontal" + playerNumber;
        m_movementAxisGamepadY = "GamepadVertical" + playerNumber;

        invulnerable = false;
        currentInvTime = maxInvTime;

        // Testing
        swipeAttack = GetComponentInChildren<SwipeAttack>();
    }

	void Update()
    {
        // storing the user movement input in a variable
        movementInput = new Vector3(Input.GetAxis(m_movementAxisKeyboardX), Input.GetAxis(m_movementAxisKeyboardY), 0f);  //keyboard control
        //movementInput = new Vector3(Input.GetAxis(m_movementAxisGamepadX), Input.GetAxis(m_movementAxisGamepadY), 0f);  //gamepad control
        movementVelocity = movementInput * moveSpeed;
        
        Dodge();
        // Testing
        // CheckIfDead();

        CheckForInv();  //check if player should currently be invulnerable

        if (swordEquipped)
        {
            SwordAttack();
        }
    }

    /**
    private void CheckIfDead()
    {
        if (health.CurrentVal == 0)
        {
            Destroy(this.gameObject);
        }
    }
    **/

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
            // Debug.Log("rechts");
            Flip();
        }
        if (movementVelocity.x < 0 && !facingLeft)
        {
            // Debug.Log("links");
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
        //Vector3 movement = (movementInput * moveSpeed);
        playerBody.velocity = movementVelocity;
    }


    //player gets hit by an enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !invulnerable && !isDodging)
        {
            //Debug.Log("Bepis");
            Vector3 knockback = (transform.position - collision.transform.position);

            HealthbarController hpControl = GetComponent<HealthbarController>();

            float dmg = 20f;

            if (collision.gameObject.name == "Basilisk") dmg = 40f;
            else if (collision.gameObject.name.Contains("BasiliskScream")) dmg = 30f;

            hpControl.ReceiveDamage(dmg);

            float enemyKnockbackPower = 600f;
            //knock both characters back
            playerBody.AddForce(knockback * knockbackPower);
            //collision.gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(-knockback * enemyKnockbackPower);
            //other.attachedRigidbody.AddForce(-knockback * enemyKnockbackPower);

            invulnerable = true;
            if (gameObject.activeSelf)
            {
                StartCoroutine(InvFrames());
            }
        }
    }

    private void CheckForInv()
    {
        if (invulnerable)
        {
            currentInvTime -= Time.deltaTime;

            if (currentInvTime <= 0)
            {
                invulnerable = false;
                // Debug.Log("blech");
                currentInvTime = maxInvTime;
            }
        }
    }

    private IEnumerator InvFrames()
    {
        sprRenderer = GetComponentInChildren<SpriteRenderer>();

        //loop to add a flashing effect to the player, showing invulnerability
        while (invulnerable)
        {
            sprRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sprRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
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
        Debug.Log("SPIN TO WIN");
        animator.SetTrigger("swipeTrigger");
    }
}
