using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room18 : MonoBehaviour
{

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemy2;
    [SerializeField]
    private GameObject archer;
    [SerializeField]
    private Transform door17To18;

    private Bounds roomBounds;
    private List<Transform> spawnPositions = new List<Transform>();
    private List<Transform> archerSpawns = new List<Transform>();
    private Transform door;

    private bool playerInside = false;
    private int spawnRate = 3;
    private int currentEnemies = 0;
    private int maxEnemies = 6;


    void Start()
    {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        door = transform.FindChild("Door");
        FillSpawnPositionsList();
        FillArcherSpawnsList();
    }

    private void FillArcherSpawnsList()
    {
        for (int i = 0; i < transform.FindChild("ArcherSpawns").childCount; i++)
        {
            archerSpawns.Add(transform.FindChild("ArcherSpawns").GetChild(i));
        }
    }

    private void FillSpawnPositionsList()
    {
        for (int i = 0; i < transform.FindChild("SpawnPositions").childCount; i++)
        {
            spawnPositions.Add(transform.FindChild("SpawnPositions").GetChild(i));
        }
    }

    void Update()
    {
        ActivateEnemies();
        if(currentEnemies == maxEnemies && EnemiesAreDead())
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (EnemiesAreDead())
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);

            door17To18.GetChild(0).gameObject.SetActive(true);
            door17To18.GetChild(1).gameObject.SetActive(false);
        }
    }

    private bool EnemiesAreDead()
    {
        int enemyCounter = 0;
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (spawnPositions[i].childCount != 0) enemyCounter++;
        }
        for (int i = 0; i < archerSpawns.Count; i++)
        {
            if (archerSpawns[i].childCount != 0) enemyCounter++;
        }
        if (enemyCounter == 0) return true;
        else return false;
    }

    private IEnumerator SpawnEnemies()
    {
        int spawnPosition = GetRandomInt(0, spawnPositions.Count);
        int enemyType = GetRandomInt(0, 2);
        yield return new WaitForSeconds(spawnRate);
        if (enemyType == 1)
        {
            GameObject newEnemy = Instantiate(enemy, spawnPositions[spawnPosition]);
        }
        else
        {
            GameObject newEnemy = Instantiate(enemy2, spawnPositions[spawnPosition]);

        }
        currentEnemies++;
        if (currentEnemies < maxEnemies) StartCoroutine(SpawnEnemies());
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void ActivateEnemies()
    {
        if (roomBounds.Contains(player1.position) || roomBounds.Contains(player2.position))
        {
            if (!playerInside)
            {
                playerInside = true;
                CloseDoor();
                SpawnArchers();
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    private void CloseDoor()
    {
        door17To18.GetChild(0).gameObject.SetActive(false);
        door17To18.GetChild(1).gameObject.SetActive(true);
    }

    private void SpawnArchers()
    {
        for (int i = 0; i < archerSpawns.Count; i++)
        {
            GameObject newArcher = Instantiate(archer, archerSpawns[i]);
        }
    }

    public void ResetRoom()
    {
        playerInside = false;
        DestroyEnemies();

        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }

    private void DestroyEnemies()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            foreach (Transform enemy in spawnPositions[i])
            {
                Destroy(enemy.gameObject);
            }
        }
        for (int i = 0; i < archerSpawns.Count; i++)
        {
            foreach (Transform archer in archerSpawns[i])
            {
                Destroy(archer.gameObject);
            }
        }
    }
}
