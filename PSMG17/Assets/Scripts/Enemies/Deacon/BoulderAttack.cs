using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAttack : MonoBehaviour {

    private Transform boulder;
    private Transform groundHit;

    private float timeForBoulderSpawn = 1f;
    private float lifetime = 1.85f;

	// Use this for initialization
	void Start ()
    {
        boulder = transform.GetChild(0);
        groundHit = transform.GetChild(1);
        StartCoroutine(ActivateBoulder());
	}

    private IEnumerator ActivateBoulder()
    {
        yield return new WaitForSecondsRealtime(timeForBoulderSpawn);
        boulder.gameObject.SetActive(true);
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }

}
