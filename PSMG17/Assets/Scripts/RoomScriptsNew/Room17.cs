using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room17 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject archer;
    private Bounds roomBounds;
    private Beehive beehiveScript;
    [SerializeField]
    private Slider beehiveHealth;
    private Transform beehive;
    private List<Transform> spawnPositions = new List<Transform>();
    private List<Transform> archerSpawns = new List<Transform>();
    private Transform door;

    private bool playerInside = false;
    private int spawnRate = 6;

    [SerializeField]
    private AudioSource beesSound;

    void Start ()
    {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        beehiveScript = GetComponentInChildren<Beehive>();
        beehive = transform.FindChild("Beehive");
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

    void Update ()
    {
        ActivateEnemies();
        if (!beehive.gameObject.activeSelf) OpenDoor();
        if (beehive.GetComponent<EnemyHealth>().health.CurrentVal == 0)
        {
            beesSound.Stop();
            beehiveHealth.gameObject.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        if (EnemiesAreDead())
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
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
        GameObject newEnemy = Instantiate(enemy, spawnPositions[spawnPosition]);
        yield return new WaitForSeconds(spawnRate);
        if (beehive.gameObject.activeSelf) StartCoroutine(SpawnEnemies());
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
                beesSound.Play();
                playerInside = true;
                beehiveScript.SpawnBees();
                SpawnArchers();
                StartCoroutine(SpawnEnemies());
                beehiveHealth.gameObject.SetActive(true);
            }
        }
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
        StopAllCoroutines();
        beehiveScript.ResetBeehive();
        beehiveHealth.gameObject.SetActive(false);
        DestroyEnemies();
        beesSound.Stop();

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
