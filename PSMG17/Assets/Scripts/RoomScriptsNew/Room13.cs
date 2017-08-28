using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room13 : MonoBehaviour {

    [HideInInspector]
    public bool memoryComplete = false;
    [SerializeField]
    private GameObject enemy;

    private Transform door;
    private Memory memory;
    private RoomDivider roomDivider;

    private List<Transform> spawnPositions = new List<Transform>();

    void Start()
    {
        door = transform.FindChild("Door");
        memory = GetComponentInChildren<Memory>();
        roomDivider = GetComponentInChildren<RoomDivider>();
        FillSpawnPositionsList();
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
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (memoryComplete)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void SpawnEnemies()
    {
        if (CurrentSpawns() <= 1)
        {
            int spawnPosition1 = GetRandomInt(0, spawnPositions.Count / 2);
            int spawnPosition2 = GetRandomInt(spawnPositions.Count / 2, spawnPositions.Count);

            GameObject enemy1 = Instantiate(enemy, spawnPositions[spawnPosition1]);
            GameObject enemy2 = Instantiate(enemy, spawnPositions[spawnPosition2]);
        }
        
    }

    private int CurrentSpawns()
    {
        int counter = 0;
        foreach (Transform spawnPosition in spawnPositions)
        {
            if (spawnPosition.childCount > 0) counter++;
        }
        return counter;
    }

    private int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public void ResetRoom()
    {
        memory.ResetPuzzle();
        memoryComplete = false;

        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);

        roomDivider.ResetRoomDivider();
    }
}
