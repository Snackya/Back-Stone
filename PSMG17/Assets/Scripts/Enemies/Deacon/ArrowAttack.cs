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
            for (int i = 0; i < 360; i += 45)
            {
                GameObject newArrow = Instantiate(holyArrow, transform);
                newArrow.transform.rotation = Quaternion.Euler(0, 0, i);
            }
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(ShootArrows());
    }
}
