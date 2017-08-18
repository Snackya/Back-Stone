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
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Basilisk")
            {
                HealthbarController hpControl = GetComponent<HealthbarController>();

                Debug.Log(collision.gameObject.name);
                if (collision.gameObject.name == "Headbutt")
                {
                    dmg = 45f;
                    knockback = true;
                }
                else if (collision.gameObject.name.Contains("BasiliskScream"))
                {
                    dmg = 30f;
                    knockback = true;
                }
                else if (collision.gameObject.tag == "Enemy")
                {
                    dmg = 15f;
                    knockback = true;
                    changeLayer = true;
                }
                Debug.Log(dmg);
                hpControl.ReceiveDamage(dmg);

                //knock both characters back
                if (knockback)
                {
                    animator.SetTrigger("getHitTrigger");
                    Vector3 knockbackDirection = (transform.position - collision.transform.position);

                    playerBody.AddForce(knockbackDirection * knockbackPower);
                    invulnerable = true;

                    if (changeLayer) gameObject.layer = LayerMask.NameToLayer("PlayerHit");
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(InvFrames());
                    }
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
