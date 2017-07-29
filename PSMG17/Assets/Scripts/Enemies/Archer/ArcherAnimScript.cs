using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimScript : MonoBehaviour {

    private Rigidbody2D enemy;
    private Animator animator;
    private bool facingRight = true;
    private Transform arm;
    private ArcherArmRotation armScript;

    private int armRotationRight = -30;
    private int armRotationLeft = 210;

    void Start () {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        arm = transform.FindChild("Arm");
        armScript = GetComponentInChildren<ArcherArmRotation>();
    }
	

	void Update () {
        animator.SetFloat("Speed", Mathf.Abs(enemy.velocity.x));
        animator.SetFloat("vSpeed", Mathf.Abs(enemy.velocity.y));

        if (enemy.velocity.x > 0 && !facingRight)
        {
            armScript.rotationOffset = armRotationRight;
            Flip();
        }
        if (enemy.velocity.x < 0 && facingRight)
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

        //Vector3 armScale = arm.localScale;
        //armScale.x *= -1;
        //arm.localScale = armScale;
    }
}
