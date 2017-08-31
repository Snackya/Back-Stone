using System.Collections;
using UnityEngine;

public class PlayerCollisionControl : MonoBehaviour {


    private Rigidbody2D playerBody;
    private Animator animator;
    private SpriteRenderer sprRenderer;
    private PlayerController playerScript;

    private bool invulnerable = false;
    [SerializeField]
    private float knockbackPower = 1000f;
    [SerializeField]
    private float maxInvTime = 1.6f;
    private float currentInvTime;

    private float dmgBasiliskHeadbutt = 50f;
    private float dmgBasiliskScream = 35f;
    private float dmgEnemy = 13f;
    private float dmgArcherArrow = 10f;
    private float dmgDeaconArrow = 12f;
    private float dmgBee = 6.5f;
    private float dmgBlackKnight = 12f;
    private float dmgCircle = 5f;
    private float dmgBoulder = 18f;

    void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprRenderer = GetComponentInChildren<SpriteRenderer>();
        playerScript = GetComponent<PlayerController>();

        currentInvTime = maxInvTime;
    }
	
	void Update ()
    {
        CheckForInv();
    }

    //player gets hit by an enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float dmg = 0f;
        bool knockback = false;
        bool changeLayer = false;
        if (!invulnerable && !playerScript.isDodging)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Basilisk" || collision.gameObject.tag == "Bee"
                || collision.gameObject.tag == "BlackKnight" || collision.gameObject.tag == "Trap" || collision.gameObject.tag == "Circle"
                || collision.gameObject.tag == "Boulder")
            {
                HealthbarController hpControl = GetComponent<HealthbarController>();

                if (collision.gameObject.name == "Headbutt")
                {
                    dmg = dmgBasiliskHeadbutt;
                    knockback = true;
                    changeLayer = false;
                }
                else if (collision.gameObject.tag == "Circle")
                {
                    dmg = dmgCircle;
                    knockback = false;
                    changeLayer = false;
                }
                else if (collision.gameObject.tag == "Boulder")
                {
                    dmg = dmgBoulder;
                    knockback = true;
                    changeLayer = false;
                }
                else if (collision.gameObject.name.Contains("BasiliskScream"))
                {
                    dmg = dmgBasiliskScream;
                    knockback = true;
                    changeLayer = false;
                }
                else if (collision.gameObject.tag == "Enemy")
                {
                    if (collision.gameObject.name.Contains("Holy"))
                    {
                        dmg = dmgDeaconArrow;
                        knockback = true;
                        changeLayer = false;
                    }
                    else if (collision.gameObject.name.Contains("Arrow"))
                    {
                        dmg = dmgArcherArrow;
                        knockback = true;
                        changeLayer = true;
                    }
                    else
                    {
                        dmg = dmgEnemy;
                        knockback = true;
                        changeLayer = true;
                    }
                } 
                else if (collision.gameObject.tag == "Bee")
                {
                    dmg = dmgBee;
                    knockback = false;
                    changeLayer = true;
                }
                else if (collision.gameObject.tag == "BlackKnight")
                {
                    dmg = dmgBlackKnight;
                    knockback = true;
                    changeLayer = false;
                }
                hpControl.ReceiveDamage(dmg);

                //knock both characters back
                if (knockback)
                {
                    Vector3 knockbackDirection = (transform.position - collision.transform.position);
                    playerBody.AddForce(knockbackDirection * knockbackPower);
                }

                animator.SetTrigger("getHitTrigger");
                invulnerable = true;

                if (changeLayer) gameObject.layer = LayerMask.NameToLayer("PlayerHit");
                if (gameObject.activeSelf)
                {
                    StartCoroutine(InvFrames());
                }
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
            yield return new WaitForSeconds(0.1f);
            sprRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void CheckForInv()
    {
        if (invulnerable)
        {
            currentInvTime -= Time.deltaTime;

            if (currentInvTime <= 0)
            {
                invulnerable = false;
                currentInvTime = maxInvTime;
            }
        }
    }
}
