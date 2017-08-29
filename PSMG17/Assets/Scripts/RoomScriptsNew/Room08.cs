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
    private Transform door;
    private List<Transform> spawnPositions = new List<Transform>();
    private Bounds pressurePlateBounds;
    private bool playerNearPressurePlates = false;
    public int maxEnemies = 30;
    public int enemiesSpawned = 0;
    private float spawnRate = 0.6f;

    private bool canKillBothPlayers = false;

    private void Start()
    {
        roomDivider = GetComponentInChildren<RoomDivider>();
        door = transform.FindChild("Door");
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
            canKillBothPlayers = true;
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
        OpenDoor();
        if (canKillBothPlayers) KillBothPlayers();
    }

    private void KillBothPlayers()
    {
        if (player1.GetComponent<HealthbarController>().currentHealth <= 0)
        {
            player2.GetComponent<HealthbarController>().currentHealth -=
                player2.GetComponent<HealthbarController>().maxHealth;
            player2.gameObject.SetActive(false);
        }
        if (player2.GetComponent<HealthbarController>().currentHealth <= 0)
        {
            player1.GetComponent<HealthbarController>().currentHealth -=
                player1.GetComponent<HealthbarController>().maxHealth;
            player1.gameObject.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        if (enemiesSpawned == maxEnemies && AllEnemiesDead())
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
            canKillBothPlayers = false;
        }
    }

    public bool AllEnemiesDead()
    {
        int noEnemyCounter = 0;

        foreach (Transform spawnPosition in spawnPositions)
        {
            if (spawnPosition.childCount == 0) noEnemyCounter++;
        }
        if (noEnemyCounter == spawnPositions.Count) return true;
        else return false;
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
        KillAllEnemies();
        roomDivider.ResetRoomDivider();
        enemiesSpawned = 0;
        playerNearPressurePlates = false;
        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }

    private void KillAllEnemies()
    {
        foreach (Transform spawnPosition in spawnPositions)
        {
            foreach (Transform enemy in spawnPosition)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
