using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightAnimationControl : MonoBehaviour {

    private bool onlyTorso;
    private bool lostArms;
    private EnemyHealth enemyHealth;
    private Animator animator;
    private GameObject blackKnight;
	// Use this for initialization
	void Start () {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        blackKnight = transform.gameObject;
        onlyTorso = false;
        lostArms = false;
	}
	
	// Update is called once per frame
	void Update () {
        LoseLimbs();
        UpdateSpeed();
	}

    private void LoseLimbs()
    {
        float maxHp = enemyHealth.health.MaxVal;
        float curHp = enemyHealth.health.CurrentVal;
        float percentHp = maxHp / curHp;

        if (percentHp < 0.3 && !lostArms)
        {
            lostArms = true;
            animator.SetTrigger("loseArmTrigger");
            if (percentHp < 0.15 && !onlyTorso)
            {
                onlyTorso = true;
                animator.SetTrigger("torsoTrigger");
            }
        }
    }

    private void UpdateSpeed()
    {
        animator.SetFloat("speed", Math.Abs(transform.GetComponent<Rigidbody2D>().velocity.x));
        animator.SetFloat("vSpeed", Math.Abs(transform.GetComponent<Rigidbody2D>().velocity.y));
    }
}
