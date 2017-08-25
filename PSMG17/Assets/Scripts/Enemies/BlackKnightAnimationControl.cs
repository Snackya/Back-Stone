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
    private Animator legsAnimator;
    private GameObject blackKnight;
    private Transform arms;
    private Transform legs;
    private Transform world;

    void Start () {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        armsAnimator = transform.parent.GetChild(1).GetComponent<Animator>();
        legsAnimator = transform.parent.GetChild(2).GetComponent<Animator>();
        arms = transform.parent.GetChild(1);
        legs = transform.parent.GetChild(2);
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
            legs.gameObject.SetActive(true);
            legs.position = transform.position;
            StartCoroutine(PositionFreeze());
            animator.SetTrigger("torsoTrigger");
            legsAnimator.SetTrigger("torsoTrigger");
            transform.localPosition = transform.localPosition;
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
