using System.Collections;
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
    }
}
