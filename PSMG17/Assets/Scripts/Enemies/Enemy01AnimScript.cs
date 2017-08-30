using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01AnimScript : MonoBehaviour {

    private Rigidbody2D enemy;
    private Animator animator;
    private bool facingRight = true;
    private Transform enemySprite;
    private Transform target;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D attackArea;
    private CapsuleCollider2D attackHitbox;

    void Start () {
        
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemySprite = transform.FindChild("sprite");
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        attackArea = transform.FindChild("AttackArea").GetComponent<BoxCollider2D>();
        attackHitbox = transform.FindChild("AttackArea").GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Speed", Mathf.Abs(enemy.velocity.x));
        animator.SetFloat("vSpeed", Mathf.Abs(enemy.velocity.y));
        if (enemy.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        if (enemy.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 enemyScale = enemySprite.localScale;
        enemyScale.x *= -1;
        enemySprite.localScale = enemyScale;
        capsuleCollider.offset = new Vector2(capsuleCollider.offset.x * (-1), capsuleCollider.offset.y);
        attackArea.offset = new Vector2(attackArea.offset.x * (-1), attackArea.offset.y);
        attackHitbox.offset = new Vector2(attackHitbox.offset.x * (-1), attackHitbox.offset.y);
    }
}
