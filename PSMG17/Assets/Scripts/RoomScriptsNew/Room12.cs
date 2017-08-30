using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room12 : MonoBehaviour
{

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private Slider bossHealthBar;
    [SerializeField]
    private GameObject trashmob;
    [SerializeField]
    private MusicManager musicManager;

    private Transform door;
    private Transform backdoor;
    private List<Transform> spawnPositions = new List<Transform>();

    private BoxCollider2D room;
    private Bounds roomBounds;

    private bool playersInside = false;
    private bool bossAlreadyDied = false;

    private GameObject enemy;
    private GameObject boss;
    private EnemyHealth bossHealth;

    private int maxEnemies = 10;
    private int currentEnemies = 0;
    private int spawnRate = 5;

    void Start()
    {
        room = transform.FindChild("RoomBounds").GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
        door = transform.FindChild("Door");
        backdoor = transform.FindChild("Backdoor");

        enemy = transform.FindChild("Enemies").gameObject;
        boss = enemy.transform.FindChild("Slingshot").gameObject;

        enemy.SetActive(false);
        boss.SetActive(false);
        bossHealthBar.gameObject.SetActive(false);
        bossHealth = boss.GetComponent<EnemyHealth>();
        FillSpawnPositionsList();
    }

    void Update()
    {
        ActivateEnemies();
        CheckIfEnemiesAreDead();
        OpenDoor();
    }

    private void CheckIfEnemiesAreDead()
    {
        if (bossHealth.health.CurrentVal <= 0)
        {
            if (!bossAlreadyDied)
            {
                musicManager.StopBossMusic3();
                musicManager.PlayBackgroundMusic();
            }
            bossHealthBar.gameObject.SetActive(false);
            bossAlreadyDied = true;
            backdoor.GetChild(0).gameObject.SetActive(true);
            backdoor.GetChild(1).gameObject.SetActive(false);

            StopCoroutine(SpawnEnemies());
            DestroyEnemies();
        }
    }

    private void OpenDoor()
    {
        if (!boss.activeSelf)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            door.GetChild(0).gameObject.SetActive(false);
            door.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void ActivateEnemies()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position) && !bossAlreadyDied)
        {
            if (!playersInside)
            {
                playersInside = true;
                enemy.SetActive(true);
                boss.SetActive(true);
                bossHealthBar.gameObject.SetActive(true);
                musicManager.StopBackGroundMusic();
                musicManager.PlayBossMusic3();
                InitialSpawn();
                StartCoroutine(SpawnEnemies());

            }
            if (boss.activeSelf)
            {
                backdoor.GetChild(0).gameObject.SetActive(false);
                backdoor.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                backdoor.GetChild(0).gameObject.SetActive(true);
                backdoor.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void resetRoom()
    {
        playersInside = false;
        enemy.SetActive(false);
        boss.SetActive(false);
        backdoor.GetChild(0).gameObject.SetActive(true);
        backdoor.GetChild(1).gameObject.SetActive(false);
        bossHealthBar.gameObject.SetActive(false);
        bossHealth.health.CurrentVal = bossHealth.health.MaxVal;

        currentEnemies = 0;
        DestroyEnemies();
        musicManager.StopBossMusic3();
        musicManager.PlayBackgroundMusic();
    }

    //start of Room02 Copy

    private void FillSpawnPositionsList()
    {
        for (int i = 0; i < transform.FindChild("SpawnPositions").childCount; i++)
        {
            spawnPositions.Add(transform.FindChild("SpawnPositions").GetChild(i));
        }
    }

    private void InitialSpawn()
    {
        GameObject enemy01 = Instantiate(trashmob, spawnPositions[7]);
    }

    private IEnumerator SpawnEnemies()
    {
        int spawnPosition = GetRandomInt(0, spawnPositions.Count);
        yield return new WaitForSeconds(spawnRate);
        GameObject newTrashmob = Instantiate(trashmob, spawnPositions[spawnPosition]);
        currentEnemies++;
        if (currentEnemies < maxEnemies) StartCoroutine(SpawnEnemies());
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void DestroyEnemies()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            foreach (Transform trashmob in spawnPositions[i])
            {
                Destroy(trashmob.gameObject);
            }
        }
    }
}