using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightAnimationControl : MonoBehaviour {

    private bool onlyTorso;
    private bool lostArms;
    private EnemyHealth enemyHealth;
    private Animator animator;
    private Animator armsAnimator;
    private GameObject blackKnight;
    private Transform arms;
    private Transform world;

    void Start () {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        armsAnimator = transform.parent.GetChild(1).GetComponent<Animator>();
        arms = transform.parent.GetChild(1);
        world = transform.parent.parent;
        blackKnight = transform.gameObject;
        onlyTorso = false;
        lostArms = false;
	}
	
	void Update () {
        LoseLimbs();
        UpdateSpeed();
	}

    private void LoseLimbs()
    {
        float maxHp = enemyHealth.health.MaxVal;
        float curHp = enemyHealth.health.CurrentVal;
        float percentHp = curHp / maxHp;

        if (percentHp < 0.5 && !lostArms)
        {
            lostArms = true;
            arms.gameObject.SetActive(true);
            arms.position = transform.position;
            StartCoroutine(PositionFreeze());
            animator.SetTrigger("lostArmsTrigger");
            armsAnimator.SetTrigger("lostArmsTrigger");
        }
        if (percentHp < 0.25 && !onlyTorso)
        {
            onlyTorso = true;
            animator.SetTrigger("torsoTrigger");
        }
    }

    private void UpdateSpeed()
    {
        animator.SetFloat("speed", Math.Abs(transform.GetComponent<Rigidbody2D>().velocity.x));
        animator.SetFloat("vSpeed", Math.Abs(transform.GetComponent<Rigidbody2D>().velocity.y));
    }

    IEnumerator PositionFreeze()
    {
        blackKnight.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(2f);
        blackKnight.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
