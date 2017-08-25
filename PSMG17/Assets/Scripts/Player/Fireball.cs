using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private Rigidbody2D rb;
    private bool playerFacingLeft;

    private float fireballDespawnTime = 5f;
    private float speed = 5f;

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
            else if (collision.gameObject.name != "Boulder(Clone)")
            {
                collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 25;
                collision.gameObject.GetComponent<EnemyAI>().Knockback();
            }
        }

        Destroy(this.gameObject);
    }
}
