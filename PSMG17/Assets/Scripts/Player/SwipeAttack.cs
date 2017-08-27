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
    [HideInInspector]
    public bool swipeActive = false;

    private int bonusDmgEnemy = 20;
    private int bonusDmgBasilisk = 10;
    private int bonusDmgBlackKnight = 10;
    private int bonusDmgBeehive = 10;
    private int bonusDmgPillar = 10;

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
            if (other.gameObject.name == "Arrow(Clone)")
            {
                Destroy(other.gameObject);
            }
            else if(other.gameObject.name != "Boulder(Clone)")
            {
                if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= bonusDmgEnemy;
                other.gameObject.GetComponent<EnemyAI>().Knockback();
            }
        }

        if (other.gameObject.tag == "Pillar")
        {
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= bonusDmgPillar;
        }

        if (other.gameObject.tag == "Basilisk")
        {
            if (other.gameObject.name.Contains("BasiliskScream"))
            {
                Destroy(other.gameObject);
            }
            else if (other.gameObject.name == "Headbutt")
            {

            }
            else
            {
                if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= bonusDmgBasilisk;
            }
        }
        if (other.gameObject.name == "Beehive")
        {
            if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= bonusDmgBeehive;
        }
        if (other.gameObject.tag == "BlackKnight")
        {
            if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= bonusDmgBlackKnight;
        }
    }
}
