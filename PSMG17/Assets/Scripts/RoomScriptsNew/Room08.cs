using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room08 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private GameObject enemy;

    private RoomDivider roomDivider;
    private List<Transform> spawnPositions = new List<Transform>();
    private Bounds pressurePlateBounds;
    private bool playerNearPressurePlates = false;
    private int maxEnemies = 30;
    private int enemiesSpawned = 0;
    private float spawnRate = 0.6f;

    private void Start()
    {
        roomDivider = GetComponentInChildren<RoomDivider>();
        pressurePlateBounds = transform.FindChild("TrapMechanism").FindChild("PressurePlates").GetComponent<BoxCollider2D>().bounds;
        FillSpawnPositionsList();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        int spawnPosition = GetRandomInt(0, spawnPositions.Count);
        if (playerNearPressurePlates && enemiesSpawned < maxEnemies)
        {
            GameObject newEnemy = Instantiate(enemy, spawnPositions[spawnPosition]);
            enemiesSpawned++;
        }
        yield return new WaitForSecondsRealtime(spawnRate);
        StartCoroutine(SpawnEnemies());
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void FillSpawnPositionsList()
    {
        for (int i = 0; i < transform.FindChild("SpawnPositions").childCount; i++)
        {
            spawnPositions.Add(transform.FindChild("SpawnPositions").GetChild(i));
        }
    }

    private void Update()
    {
        CheckIfPlayerIsInPressureplatesBound();
    }

    private void CheckIfPlayerIsInPressureplatesBound()
    {
        if (pressurePlateBounds.Contains(player1.position) || pressurePlateBounds.Contains(player2.position))
        {
            playerNearPressurePlates = true;
        }
    }

    public void ResetRoom()
    {
        roomDivider.ResetRoomDivider();
        enemiesSpawned = 0;
        playerNearPressurePlates = false;
    }
    
}
