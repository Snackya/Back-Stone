using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaController : MonoBehaviour {
    private GameObject parent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float downwardAttackRange;
    private float attackCooldown = 2.5f;
    private bool isAttacking = false;

	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject;
        spriteRenderer = parent.GetComponent<SpriteRenderer>();
        downwardAttackRange = spriteRenderer.bounds.extents.y - 0.5f;
        animator = parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        float cooldownRemaining;
        Vector3 playerPos = other.gameObject.transform.position;
        Vector3 selfPos = parent.transform.position;

        if(other.gameObject.tag == "Player" && !isAttacking)
        {
            isAttacking = true;

            //sword attack if BK has arms; kick otherwise
            if (playerPos.y > selfPos.y - downwardAttackRange)
            {
                if (playerPos.x <= selfPos.x)
                {
                    animator.SetTrigger("attackLeftTrigger");
                    animator.SetTrigger("kickLeftTrigger");
                }
                else if (playerPos.x > selfPos.x)
                {
                    animator.SetTrigger("attackRightTrigger");
                    animator.SetTrigger("kickRightTrigger");
                }
            }
            else
            {
                int random = Random.Range(0, 2);
                Debug.Log(random);
                if(random == 0)
                {
                    animator.SetTrigger("attackFrontLeftTrigger");
                }
                else
                {
                    animator.SetTrigger("attackFrontRightTrigger");
                }
            }
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
