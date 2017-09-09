using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPressurePlates : MonoBehaviour {

    [SerializeField]
    private Transform[] hubs;
    [SerializeField]
    private HubPuzzle hubPuzzle;

    private int rotationAngle = 90;
    private float rotationSpeed = 0.02f;

    [SerializeField]
    private AudioSource rotatingStone;

    private float[] rotationTolerance = new float[] { 0.71f, -0.7f };


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hubPuzzle.puzzleCompleted)
        {
            for (int i = 0; i < hubs.Length; i++)
            {
                if (i == 0)
                {
                    StartCoroutine(RotateHubs(true, hubs[i]));
                }
                else
                {
                    StartCoroutine(RotateHubs(false, hubs[i]));
                }
            }
        }
    }

    private IEnumerator RotateHubs(bool turnRight, Transform hub)
    {
        rotatingStone.Play();
        for (int i = 0; i < rotationAngle; i++)
        {
            if (turnRight)
            {
                hub.Rotate(new Vector3(0, 0, 1));
            }
            else
            {
                hub.Rotate(new Vector3(0, 0, -1));
            }
            yield return new WaitForSeconds(rotationSpeed);
        }
        FixRotation(hub);
    }

    private void FixRotation(Transform hub)
    {
        if (hub.localEulerAngles.z < 90 + rotationTolerance[0] && hub.localEulerAngles.z > 90 + rotationTolerance[1]) hub.Rotate(new Vector3(0, 0, 90));
        if (hub.localEulerAngles.z < 270 + rotationTolerance[0] && hub.localEulerAngles.z > 270 + rotationTolerance[1]) hub.Rotate(new Vector3(0, 0, 90));
        if (hub.localEulerAngles.z > -90 + rotationTolerance[1] && hub.localEulerAngles.z < -90 + rotationTolerance[0]) hub.Rotate(new Vector3(0, 0, -90));
        if (hub.localEulerAngles.z > -270 + rotationTolerance[1] && hub.localEulerAngles.z < -270 + rotationTolerance[0]) hub.Rotate(new Vector3(0, 0, -90));
    }
}
