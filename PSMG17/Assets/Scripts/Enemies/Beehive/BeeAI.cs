using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : MonoBehaviour {

    private Rigidbody2D rb;
    private float speed;
    private int randomRotation;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateRandomSpeed();
        StartCoroutine(RandomMovement());
	}

    private void CalculateRandomSpeed()
    {
        speed = UnityEngine.Random.Range(1.5f, 3f);
    }

    private IEnumerator RandomMovement()
    {
        randomRotation = CalculateRandomRotation();
        transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        rb.velocity = (Vector2)transform.TransformDirection(Vector3.up) * speed;

        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomMovement());
    }

    private int CalculateRandomRotation()
    {
        return UnityEngine.Random.Range(0, 360);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.rotation = Quaternion.Euler(0, 0, randomRotation + 180);
        rb.velocity = (Vector2)transform.TransformDirection(Vector3.up) * speed;
    }

}
