using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour {

    [SerializeField]
    private GameObject bee;
    [SerializeField]
    private Transform spawnPos;

    private EnemyHealth health;
    private int grandChildCount;


	void Start () {
        health = GetComponent<EnemyHealth>();
	}
	
	
    public void ResetBeehive()
    {
        health.health.CurrentVal = health.health.MaxVal;
        StopAllCoroutines();
        grandChildCount = transform.GetChild(0).childCount;
        Debug.Log(grandChildCount);
        for (int i = 0; i < grandChildCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }

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
