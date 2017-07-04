using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject[] targets;
    public float speed = 0.5f;

    float lifetime = 4f;

    void Update()
    {
        Debug.Log(lifetime);
        lifetime -= Time.deltaTime;

        if (lifetime <= 0) GameObject.Destroy(gameObject);

        //move projectile forward according to the current rotation
        else GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
    }

    void Awake()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //do not destroy if it collides with the enemy shooting it
        if (collision.gameObject.tag != "Enemy")
        {
            Debug.Log("scream destroyed by collision");
            GameObject.Destroy(gameObject);
        }
    }
}
