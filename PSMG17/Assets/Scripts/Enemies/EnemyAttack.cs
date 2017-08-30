using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator animator;

    private void OnEnable()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("Attack");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
