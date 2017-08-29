using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeaconArrowController : MonoBehaviour {

    private Rigidbody2D rb;
    private Transform tf;
    private float lifetime = 5f;
    private float speed = 4f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveArrow();
        DestroyAfterTime();
	}

    private void MoveArrow()
    {
        rb.velocity = transform.right * speed;
    }

    private void DestroyAfterTime()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(this.gameObject);
    }
}
