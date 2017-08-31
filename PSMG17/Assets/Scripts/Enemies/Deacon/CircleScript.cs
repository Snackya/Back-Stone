using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour {

    [SerializeField]
    private bool turnRight;
    private float rotationSpeed = 0.025f;
    private int dmg = 5;

    public void RotateCircles()
    {
        StartCoroutine(RotateCircle());
    }

    private IEnumerator RotateCircle()
    {
        if (turnRight) transform.Rotate(new Vector3(0, 0, 1));
        else transform.Rotate(new Vector3(0, 0, -1));
        yield return new WaitForSeconds(rotationSpeed);
        StartCoroutine(RotateCircle());
    }
}
