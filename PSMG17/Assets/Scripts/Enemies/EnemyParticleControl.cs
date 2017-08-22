using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleControl : MonoBehaviour {

    [SerializeField]
    private Transform bloodSplash;

    private void OnTriggerEnter2D()
    {
        bloodSplash.GetComponent<ParticleSystem>().Play();
    }
}
