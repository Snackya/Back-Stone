using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalFountain : MonoBehaviour {

    private HealthbarController hp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hp = collision.gameObject.GetComponent<HealthbarController>();
            hp.currentHealth = hp.maxHealth;
        }
    }
}
