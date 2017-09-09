using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandHeal : MonoBehaviour {

    public Stat cooldown;
    [SerializeField]
    private float cooldownTime;

    private int playerNumber;
    private PlayerController player;
    [HideInInspector]
    public bool healActive = false;

    private void Awake()
    {
        cooldown.Initialize();
        player = GetComponentInParent<PlayerController>();
        playerNumber = player.playerNumber;
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Swipe" + playerNumber) && cooldown.CurrentVal == cooldown.MaxVal)
        {
            player.SwipeAttack();
            HealSelf();
            cooldown.CurrentVal = 0f;
        }
        ResetCooldown();
    }

    private void HealSelf()
    {
        transform.GetComponentInParent<HealthbarController>().currentHealth +=
            transform.GetComponentInParent<HealthbarController>().maxHealth / 5;
        transform.parent.FindChild("Particles").FindChild("HealingParticles").GetComponent<ParticleSystem>().Play();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthbarController>().currentHealth +=
                collision.gameObject.GetComponent<HealthbarController>().maxHealth / 5;
            collision.transform.FindChild("Particles").FindChild("HealingParticles").GetComponent<ParticleSystem>().Play();
        }
    }
}
