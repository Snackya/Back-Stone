using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room06 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private Slider basiliskHealthBar;
    private Transform door;
    private Transform backdoor;

    private BoxCollider2D room;
    private Bounds roomBounds;

    private bool playersInside = false;
    private bool bossAlreadyDied = false;

    private GameObject enemy;
    private GameObject boss;
    private EnemyHealth bossHealth;

    void Start()
    {
        room = transform.FindChild("RoomBounds").GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
        door = transform.FindChild("Door");
        backdoor = transform.FindChild("Backdoor");

        enemy = transform.FindChild("Enemies").gameObject;
        boss = enemy.transform.FindChild("Basilisk").gameObject;

        enemy.SetActive(false);
        boss.SetActive(false);
        basiliskHealthBar.gameObject.SetActive(false);
        bossHealth = boss.GetComponent<EnemyHealth>();
    }

    void Update()
    {
        ActivateEnemies();
        CheckIfEnemiesAreDead();
        OpenDoor();
    }

    private void CheckIfEnemiesAreDead()
    {
        if (bossHealth.health.CurrentVal == 0)
        {
            basiliskHealthBar.gameObject.SetActive(false);
            bossAlreadyDied = true;
            backdoor.GetChild(0).gameObject.SetActive(true);
            backdoor.GetChild(1).gameObject.SetActive(false);
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
                basiliskHealthBar.gameObject.SetActive(true);
                
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
        basiliskHealthBar.gameObject.SetActive(false);
        bossHealth.health.CurrentVal = bossHealth.health.MaxVal;
    }
}
