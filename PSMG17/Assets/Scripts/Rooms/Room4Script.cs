using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room4Script : MonoBehaviour
{

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private Slider basiliskHealthBar;

    private BoxCollider2D room;
    private Bounds roomBounds;

    private bool playersInside = false;
    private bool alreadyDiedOnce = false;

    private GameObject enemy;
    private GameObject boss;

    void Start()
    {
        room = transform.FindChild("RoomBounds").GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;

        enemy = transform.FindChild("Enemies").gameObject;
        boss = enemy.transform.FindChild("Basilisk").gameObject;

        enemy.SetActive(false);
        boss.SetActive(false);
        basiliskHealthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        ActivateEnemies();
        CheckIfEnemiesAreDead();
    }

    private void CheckIfEnemiesAreDead()
    {
        if (!boss.activeSelf && !alreadyDiedOnce)
        {
            basiliskHealthBar.gameObject.SetActive(false);
            alreadyDiedOnce = true;
        }
    }

    private void ActivateEnemies()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside)
            {
                playersInside = true;
                enemy.SetActive(true);
                boss.SetActive(true);
                basiliskHealthBar.gameObject.SetActive(true);
            }
        }
    }
}
