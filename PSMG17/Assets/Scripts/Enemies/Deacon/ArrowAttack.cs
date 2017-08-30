using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack : MonoBehaviour {

    [SerializeField]
    private Transform room20;
    [SerializeField]
    private GameObject holyArrow;
    private MagicalBarrier magicalBarrier;
    [SerializeField]
    private AudioSource arrowsShooting;

	// Use this for initialization
	void Start ()
    {
        magicalBarrier = room20.GetComponentInChildren<MagicalBarrier>();
        StartCoroutine(ShootArrows());
	}

    private IEnumerator ShootArrows()
    {
        if (magicalBarrier.barrierActive)
        {
            arrowsShooting.Play();
            int rotationOffset = GetRandomInt(0, 45);
            for (int i = 0; i < 360; i += 45)
            {
                GameObject newArrow = Instantiate(holyArrow, transform);
                newArrow.transform.rotation = Quaternion.Euler(0, 0, i + rotationOffset);
            }
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(ShootArrows());
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
