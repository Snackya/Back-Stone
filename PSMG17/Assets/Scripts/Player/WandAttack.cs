using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAttack : MonoBehaviour {

    [SerializeField]
    private GameObject projectile;

    private Transform fireballSpawn;

	void Start () {
        fireballSpawn = transform.FindChild("FireballSpawn");
	}


    public void ShootFireball()
    {
        GameObject newFireball = Instantiate(projectile, fireballSpawn);
    }

   
}
