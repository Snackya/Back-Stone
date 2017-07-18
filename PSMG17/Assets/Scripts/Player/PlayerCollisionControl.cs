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
        float dmg = 20f;
        if (!invulnerable && !playerScript.isDodging)
        {
            if (collision.gameObject.tag.Contains("Basilisk") || collision.gameObject.tag == "Enemy")
            {
                if (playerScript.playerNumber == 1)
                {
                    Debug.Log(collision.gameObject.tag);
                    Debug.Log(invulnerable);
                }
                animator.SetTrigger("getHitTrigger");

                Vector3 knockback = (transform.position - collision.transform.position);
                HealthbarController hpControl = GetComponent<HealthbarController>();

                if (collision.gameObject.name == "Basilisk") dmg = 10f;
                else if (collision.gameObject.name.Contains("BasiliskScream")) dmg = 30f;

                hpControl.ReceiveDamage(dmg);

                //knock both characters back
                playerBody.AddForce(knockback * knockbackPower);

                invulnerable = true;

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
