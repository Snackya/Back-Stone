using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleGuyAnimationControl : MonoBehaviour {
    private int drinkDelay = 3;
    private int minDelay = 4;
    private int maxDelay = 8;
    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RandomlyDrink());
    }

    IEnumerator RandomlyDrink()
    {
        animator.SetTrigger("drinkTrigger");
        drinkDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(drinkDelay);
        StartCoroutine(RandomlyDrink());
    }
}
