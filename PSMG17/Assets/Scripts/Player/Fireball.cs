using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private Rigidbody2D rb;
    private bool playerFacingLeft;

    private float fireballDespawnTime = 5f;
    private float speed = 5f;

    private int dmgEnemy = 25;
    private int dmgBasilisk = 10;
    private int dmgBlackKnight = 7;
    private int dmgBeehive = 7;
    private int dmgPillar = 20;
    private int dmgDeacon = 7;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        playerFacingLeft = GetComponentInParent<PlayerController>().facingLeft;
        transform.parent = null;
        StartCoroutine(DestroyFireball());
	}

    private IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(fireballDespawnTime);
        Destroy(this.gameObject);
    }

    void Update ()
    {
        if (!playerFacingLeft)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Arrow(Clone)")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "HolyArrow(Clone)")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name != "Boulder(Clone)")
            {
                collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= dmgEnemy;
                collision.gameObject.GetComponent<EnemyAI>().Knockback();
            }
        }

        if (collision.gameObject.tag == "Pillar")
        {
            collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= dmgPillar;
        }
        if (collision.gameObject.tag == "Deacon")
        {
            collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= dmgDeacon;
        }

        if (collision.gameObject.tag == "Basilisk")
        {
            if (collision.gameObject.name.Contains("BasiliskScream"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Headbutt")
            {

            }
            else
            {
                collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= dmgBasilisk;
            }
        }
        if (collision.gameObject.tag == "BlackKnight")
        {
            if (collision.gameObject.name == "BlackKnight") collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= dmgBlackKnight;
        }
        if (collision.gameObject.name == "Beehive")
        {
            collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= dmgBeehive;
        }
        if (collision.gameObject.tag == "Bee")
        {
            Destroy(collision.gameObject);
        }
        //knock boulders back and re-define them as player weapons
        if (collision.gameObject.name.Contains("Boulder"))
        {
            GameObject boulder = collision.gameObject;
            Transform slingshot = boulder.GetComponentInParent<Transform>().GetComponentInParent<Transform>();
            ProjectileController projCtrl = collision.gameObject.GetComponent<ProjectileController>();

            projCtrl.direction = -(transform.position - boulder.transform.position).normalized * projCtrl.speed;
            projCtrl.lifetime = projCtrl.maxLifetime;
            boulder.tag = "PlayerWeapon";
        }

        Destroy(this.gameObject);
    }
}
