using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room02 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private GameObject enemy;

    private BoxCollider2D room;
    private Bounds roomBounds;
    private List<Transform> spawnPositions = new List<Transform>();

    private bool playersInside = false;
    private int maxEnemies = 3;
    private int currentEnemies = 0;
    private int spawnRate = 5;


    void Start()
    {
        room = GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
        FillSpawnPositionsList();
    }

    private void FillSpawnPositionsList()
    {
        for (int i = 0; i < transform.FindChild("SpawnPositions").childCount; i++)
        {
            spawnPositions.Add(transform.FindChild("SpawnPositions").GetChild(i));
        }
    }

    void Update () {
        ActivateEnemies();
        if (currentEnemies == maxEnemies)
        {
            CheckIfEnemiesAreDead();
        }
    }


    private void CheckIfEnemiesAreDead()
    {
        int noEnemyCounter = 0;
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (spawnPositions[i].childCount == 0) noEnemyCounter++;
        }
        Debug.Log(noEnemyCounter);
        if (noEnemyCounter == spawnPositions.Count)
        {
            Debug.Log("OPEN THE DOOR!");
        }
    }


    private void ActivateEnemies()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside)
            {
                playersInside = true;
                InitialSpawn();
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    private void InitialSpawn()
    {
        GameObject enemy01 = Instantiate(enemy, spawnPositions[7]);
    }

    private IEnumerator SpawnEnemies()
    {
        int spawnPosition = GetRandomInt(0, spawnPositions.Count);
        yield return new WaitForSeconds(spawnRate);
        GameObject newEnemy = Instantiate(enemy, spawnPositions[spawnPosition]);
        currentEnemies++;
        if (currentEnemies < maxEnemies) StartCoroutine(SpawnEnemies());
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public void resetRoom()
    {
        currentEnemies = 0;
        playersInside = false;
    }
}
