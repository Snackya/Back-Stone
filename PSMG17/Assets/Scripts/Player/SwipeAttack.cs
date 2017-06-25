using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    [SerializeField]
    private Stat cooldown;
    [SerializeField]
    private float cooldownTime;

    private int playerNumber;
    private PlayerController player;

    void Awake()
    {
        cooldown.Initialize();
        player = GetComponentInParent<PlayerController>();
        playerNumber = player.playerNumber;
    }

    void Update()
    {
        if (Input.GetButtonDown("Swipe" + playerNumber) && cooldown.CurrentVal == cooldown.MaxVal)
        {
            player.SwipeAttack();
            cooldown.CurrentVal = 0f;
        }
        ResetCooldown();
    }

    private void ResetCooldown()
    {
        if (cooldown.CurrentVal == 0f)
        {
            StartCoroutine(SetCooldownValueToMax());
        }
    }

    private IEnumerator SetCooldownValueToMax()
    {
        for (int i = 0; i < 20; i++)
        {
            cooldown.CurrentVal += cooldown.MaxVal / 20;
            yield return new WaitForSeconds(cooldownTime / 20);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 30;
            other.gameObject.GetComponent<EnemyAI>().Knockback();
        }
        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("Swipe Attack on Boss.");
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 7;
        }
    }
}
