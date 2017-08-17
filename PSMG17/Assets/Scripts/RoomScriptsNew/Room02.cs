using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room02 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private BoxCollider2D room;
    private Bounds roomBounds;


    private bool playersInside = false;

    private GameObject enemies;
    private Vector3[] enemyPositions;
    private bool enemiesActive = false;


    void Start () {
        room = GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
        enemies = transform.Find("Enemies").gameObject;
        InitializeEnemyPositions();
    }


    private void InitializeEnemyPositions()
    {
        enemyPositions = new Vector3[enemies.transform.childCount];
        int i = 0;
        foreach (Transform enemy in enemies.transform)
        {
            enemyPositions[i] = enemy.transform.position;
            i++;
        }
    }


    void Update () {
        ActivateEnemies();
        CheckIfEnemiesAreDead();
    }


    private void CheckIfEnemiesAreDead()
    {
        int deathCounter = 0;

        foreach (Transform child in enemies.transform)
        {
            if (!child.gameObject.activeSelf) deathCounter++;
        }

        if (deathCounter == enemies.transform.childCount) enemiesActive = false;
    }


    private void ActivateEnemies()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside) Debug.Log("Both players entered the room.");
            playersInside = true;
            enemies.SetActive(true);
            enemiesActive = true;
        }
    }


    public void resetRoom()
    {
        int i = 0;
        foreach (Transform enemy in enemies.transform)
        {
            enemy.transform.SetPositionAndRotation(enemyPositions[i], new Quaternion());
            enemy.gameObject.GetComponent<EnemyHealth>().health.CurrentVal =
                enemy.gameObject.GetComponent<EnemyHealth>().health.MaxVal;
            enemy.gameObject.SetActive(true);
            i++;
        }
        enemies.SetActive(false);
        enemiesActive = false;
        playersInside = false;
    }
}
