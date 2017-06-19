using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public Stat health;

    void Awake()
    {
        health.Initialize();
    }

    // Update is called once per frame
    void Update () {
        CheckIfDead();
        /*if(Input.GetButtonDown("Attack1"))
        {
            health.CurrentVal -= 50;
        }*/
	}

    private void CheckIfDead()
    {
        if (health.CurrentVal == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
