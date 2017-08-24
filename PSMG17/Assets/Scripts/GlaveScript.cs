using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaveScript : MonoBehaviour {

    private float rotationSpeed = 70f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private bool moveHorizontally = true;
    private Rigidbody2D rb;
    private Transform collider;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        collider = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        RotateGlave();
    }

    private void FixedUpdate()
    {
        MoveGlave();
    }

    private void MoveGlave()
    {
        if (moveHorizontally)
        {
            //rb.AddForce(new Vector2(movementSpeed, 0));
            rb.velocity = new Vector2(movementSpeed, 0);
        }
        else
        {
            //rb.AddForce(new Vector2(0, movementSpeed));
            rb.velocity = new Vector2(0, movementSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collider.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            movementSpeed = -movementSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collider.gameObject.SetActive(false);
        }
    }

    private void RotateGlave()
    {
        transform.Rotate(0, 0, 6.0f * rotationSpeed * Time.deltaTime);
    }
}
