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
    
    // TODO: Durch Funktion ersetzen, die playerNumber aus der Elternklasse holt
    [SerializeField]
    private int playerNumber;

    void Awake()
    {
        cooldown.Initialize();
    }

    void Update()
    {
        if (Input.GetButtonDown("Swipe" + playerNumber) && cooldown.CurrentVal == cooldown.MaxVal)
        {
            cooldown.CurrentVal = 0f;
            Attack();
        }
        ResetCooldown();
    }

    // TODO: Animation ausführen
    private void Attack()
    {
        print("SPIN TO WIN!");
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

    // macht noch nichts
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 30;
            other.gameObject.GetComponent<EnemyAI>().Knockback();
        }
    }
}
