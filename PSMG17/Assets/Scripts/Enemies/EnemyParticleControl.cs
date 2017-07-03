using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleControl : MonoBehaviour {

    [SerializeField]
    private Transform bloodSplash;

	// Use this for initialization
	void Start () {
        bloodSplash.GetComponent<ParticleSystem>().enableEmission = false;
	}

    private void OnTriggerEnter2D()
    {
        bloodSplash.GetComponent<ParticleSystem>().enableEmission = true;
        StartCoroutine(StopBloodSplash());
    }

    private IEnumerator StopBloodSplash()
    {
        yield return new WaitForSeconds(.4f);
        bloodSplash.GetComponent<ParticleSystem>().enableEmission = false;
    }
}
