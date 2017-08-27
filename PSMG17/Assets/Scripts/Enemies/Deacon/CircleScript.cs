using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour {

    [SerializeField]
    private bool turnRight;
    private float rotationSpeed = 0.025f;
    private int dmg = 5;

    private void OnEnable()
    {
        StartCoroutine(RotateCircles());
    }

    private IEnumerator RotateCircles()
    {
        if (turnRight) transform.Rotate(new Vector3(0, 0, 1));
        else transform.Rotate(new Vector3(0, 0, -1));
        yield return new WaitForSeconds(rotationSpeed);
        StartCoroutine(RotateCircles());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthbarController>().currentHealth -= dmg;
        }
    }
}
