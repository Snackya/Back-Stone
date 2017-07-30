using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimScript : MonoBehaviour {

    private Rigidbody2D enemy;
    private Animator animator;
    private bool facingRight = true;
    private Transform arm;
    private ArcherArmRotation armScript;
    private EnemyAI enemyAI;

    private int armRotationRight = -30;
    private int armRotationLeft = 210;

    void Start () {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        arm = transform.FindChild("Arm");
        armScript = GetComponentInChildren<ArcherArmRotation>();
        enemyAI = GetComponent<EnemyAI>();
    }
	

	void Update () {
        animator.SetFloat("Speed", Mathf.Abs(enemy.velocity.x));
        animator.SetFloat("vSpeed", Mathf.Abs(enemy.velocity.y));

        if (transform.position.x < enemyAI.target.position.x 
            && !facingRight)
        {
            armScript.rotationOffset = armRotationRight;
            Flip();
        }
        if (transform.position.x > enemyAI.target.position.x 
            && facingRight)
        {
            armScript.rotationOffset = armRotationLeft;
            Flip();
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
