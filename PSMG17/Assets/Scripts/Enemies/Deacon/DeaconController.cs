using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeaconController : MonoBehaviour
{

    [SerializeField]
    private GameObject boulder;
    [SerializeField]
    private AudioSource explosionSound;
    private List<Transform> boulderSpawnPositions = new List<Transform>();
    private float attackRate = 1.2f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        FillBoulderSpawnPositionsList();
        StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(attackRate);
        int diceRoll = GetRandomInt(1, 6);
        if (diceRoll <= 3) animator.SetTrigger("Attack");
        StartCoroutine(AttackAnimation());
    }

    private void FillBoulderSpawnPositionsList()
    {
        for (int i = 0; i < transform.FindChild("BoulderSpawnPositions").childCount; i++)
        {
            boulderSpawnPositions.Add(transform.FindChild("BoulderSpawnPositions").GetChild(i));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) BoulderAttack();
    }

    public void BoulderAttack()
    {
        int spawnPosIndex = GetRandomInt(0, boulderSpawnPositions.Count);
        foreach (Transform spawnPosition in boulderSpawnPositions[spawnPosIndex])
        {
            GameObject newBoulder = Instantiate(boulder, spawnPosition);
        }
        StartCoroutine(PlayExplosionSound());
    }

    private IEnumerator PlayExplosionSound()
    {
        yield return new WaitForSeconds(1.4f);
        explosionSound.Play();
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
