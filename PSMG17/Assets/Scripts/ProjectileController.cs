using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private string enemyType;
    private Transform target;
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 2.5f;
    private float lifetime = 7f;
    private Vector2 direction;

    private bool archerFacingRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemyType == "Basilisk")
        {
           target = GetComponentInParent<BasiliskController>().target;
        }
        else if (enemyType == "Archer")
        {
            target = GetComponentInParent<EnemyAI>().target;

            archerFacingRight = GetComponentInParent<ArcherAnimScript>().facingRight;
            if (archerFacingRight) transform.rotation = Quaternion.Euler(0, 0, 0);
            else transform.rotation = Quaternion.Euler(180, 0, 0);
        }
       
        direction = (transform.position - target.position).normalized;

        float angleRad = Mathf.Atan2(transform.position.y - target.position.y, 
            transform.position.x - target.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;

        transform.Rotate(0, 0, angleDeg);

        direction *= -speed;

        // prevents the projectile from moving and rotating along with its parent
        transform.parent = null;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0) Destroy(gameObject);

        //move projectile forward according to the current rotation
        else rb.velocity = direction;
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
