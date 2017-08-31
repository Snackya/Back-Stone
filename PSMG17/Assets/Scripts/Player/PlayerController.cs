using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;                // player movement speed
    public int playerNumber;                    // used to assign players to different controls
    [HideInInspector]
    public bool isDodging = false;
    [HideInInspector]
    public bool canMove = true;                 //enables objects like the DialogManager to freeze the player

    private bool dodgeOnCooldown = false;

    private Rigidbody2D playerBody;
    private Collider2D playerCollider;
    private SpriteRenderer sprRenderer;
    private Vector3 movementInput;
    private Vector3 movementVelocity;
    private Animator animator;
    private Transform playerSprite;             // To change the players current direction
    public bool facingLeft = true;
    private CapsuleCollider2D attackCollider;

    private float sprintSpeedIncrease = 4f;   // amount of speed increase, when player is sprinting
    private string m_movementAxisKeyboardX;
    private string m_movementAxisKeyboardY;
    private string m_movementAxisGamepadX;
    private string m_movementAxisGamepadY;
    public bool usingGamepad = false;

    [HideInInspector]
    public bool swordEquipped = true;
    [HideInInspector]
    public bool standardAttackReady = true;
    private GameObject sword;
    private GameObject wand;

    //Sounds
    [SerializeField] private AudioSource swordAttackSound;
    [SerializeField] private AudioSource fireballSound;
    [SerializeField] private AudioSource healSound;
    [SerializeField] private AudioSource swipeAttackSound;
    

    void Start()
    {
        // initializing playerBody with the player character
        playerBody = GetComponent<Rigidbody2D>();

        playerSprite = transform.FindChild("sprite");
        attackCollider = transform.FindChild("Sword").GetComponent<CapsuleCollider2D>();

        playerCollider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();

        sword = transform.FindChild("Sword").gameObject;
        wand = transform.FindChild("Wand").gameObject;

        m_movementAxisKeyboardX = "Horizontal" + playerNumber;
        m_movementAxisKeyboardY = "Vertical" + playerNumber;
        m_movementAxisGamepadX = "GamepadHorizontal" + playerNumber;
        m_movementAxisGamepadY = "GamepadVertical" + playerNumber;

        animator.SetBool("swordEquipped", true);
    }

	void Update()
    {
        SetMovementVelocity();
        Dodge();      
        Attack();
        SwitchWeapons();

        if (Input.GetKeyDown(KeyCode.K)) PlaySwordAttackSound();
    }

    private void SetMovementVelocity()
    {
        // storing the user movement input in a variable
        if (usingGamepad)
        {
            movementInput = new Vector3(Input.GetAxis(m_movementAxisGamepadX), Input.GetAxis(m_movementAxisGamepadY), 0f);  //gamepad control

        }
        else
        {
            movementInput = new Vector3(Input.GetAxis(m_movementAxisKeyboardX), Input.GetAxis(m_movementAxisKeyboardY), 0f);  //keyboard control
        }
        movementVelocity = movementInput * moveSpeed;
    }

    private void SwitchWeapons()
    {
        if (swordEquipped)
        {
            animator.SetBool("swordEquipped", true);
            sword.SetActive(true);
            wand.SetActive(false);
        }
        else
        {
            animator.SetBool("swordEquipped", false);
            sword.SetActive(false);
            wand.SetActive(true);
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player" + playerNumber + "AttackLeft") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("Player" + playerNumber + "WandAttack") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Player" + playerNumber + "WandHeal"))
        {
            movementVelocity = new Vector3(0, 0, 0);
        }
    }

    private void AnimationController()
    {
        if (canMove)
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
        else
        {
            //prevent movement animation during input disable
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("vSpeed", 0f);
        }    
    }

    //Dodge-roll with invulnerability
    private void Dodge()
    {
        if (Input.GetButtonDown("Dodge" + playerNumber) && !isDodging && !dodgeOnCooldown && canMove)
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

    private IEnumerator AttackCooldownCounter()
    {
        yield return new WaitForSeconds(1f);
        standardAttackReady = true;
    }

    private void Move()
    {
        if (canMove)
        {
            playerBody.velocity = movementVelocity;
        }
        else
        {
            playerBody.velocity = new Vector2(0, 0);    //prevent player from running forward when disabling input during movement
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;

        attackCollider.offset = new Vector2(attackCollider.offset.x * (-1), attackCollider.offset.y);
        Vector3 wandScale = wand.transform.localScale;
        wandScale.x *= -1;
        wand.transform.localScale = wandScale;

        Vector3 playerScale = playerSprite.localScale;
        playerScale.x *= -1;
        playerSprite.localScale = playerScale;
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Attack" + playerNumber) && standardAttackReady && canMove)
        {
            animator.SetTrigger("attackTrigger");
            standardAttackReady = false;
            StartCoroutine(AttackCooldownCounter());
        }
    }

    public void SwipeAttack()
    {
        if (canMove)
        {
            animator.SetTrigger("swipeTrigger");
        }
    }

    // stupid workaround :(
    public void WandAttack()
    {
        if (canMove)
        {
            wand.GetComponent<WandAttack>().ShootFireball();
        }
    }

    public void PlaySwordAttackSound()
    {
        swordAttackSound.Play();
    }

    public void PlayFireballSound()
    {
        fireballSound.Play();
    }

    public void PlayHealSound()
    {
        healSound.Play();
    }

    public void PlaySwipeAttackSound()
    {
        swipeAttackSound.Play();
    }
}
