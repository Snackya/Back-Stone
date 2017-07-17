using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;

    private float speed = 2;
    private float lifetime = 4f;
    private Vector2 direction;


    void Awake()
    {
        target = GetComponentInParent<BasiliskController>().target;
        direction = (transform.position - target.position).normalized;

        float angleRad = Mathf.Atan2(transform.position.y - target.position.y, 
            transform.position.x - target.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;

        transform.Rotate(0, 0, angleDeg);

        direction *= -speed;
    }

    void Update()
    {
        Debug.Log(direction);
        lifetime -= Time.deltaTime;

        if (lifetime <= 0) Destroy(gameObject);

        //move projectile forward according to the current rotation
        else GetComponent<Rigidbody2D>().velocity = direction;
    }

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        //do not destroy if it collides with the enemy shooting it
        if (collision.gameObject.tag != "Enemy")
        {
            Debug.Log("scream destroyed by collision");
            Destroy(gameObject);
        }
    }
}
