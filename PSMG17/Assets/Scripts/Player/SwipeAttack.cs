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
    public bool swipeActive = false;

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
                if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 20;
                other.gameObject.GetComponent<EnemyAI>().Knockback();
            }
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
                Debug.Log("Swipe Attack on Basilisk.");
                if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 10;
            }
        }
        if (other.gameObject.name == "Beehive")
        {
            if (swipeActive) other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 10;
        }
    }
}
