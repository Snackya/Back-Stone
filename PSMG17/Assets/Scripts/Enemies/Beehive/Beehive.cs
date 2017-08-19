using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour {

    [SerializeField]
    private GameObject bee;
    [SerializeField]
    private Transform spawnPos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnBees()
    {
        StartCoroutine(SpawnBee());
    }

    private IEnumerator SpawnBee()
    {
        GameObject newBee = Instantiate(bee, spawnPos);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnBee());
    }
}
