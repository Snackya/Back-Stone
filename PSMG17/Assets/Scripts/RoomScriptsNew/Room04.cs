using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room04 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private Slider beehiveHealth;

    private Bounds roomBounds;

    private bool playerInside = false;

    private Beehive beehiveScript;
    private Transform beehive;
    private Transform door;

    [SerializeField]
    private AudioSource beesSound;

    void Start()
    {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        beehiveScript = GetComponentInChildren<Beehive>();
        beehive = transform.FindChild("Beehive");
        door = transform.FindChild("Door");
    }

    void Update()
    {
        ActivateBeehive();
        OpenDoor();
        CheckIfBeehiveDestroyed();
    }

    private void CheckIfBeehiveDestroyed()
    {
        if (beehive.GetComponent<EnemyHealth>().health.CurrentVal == 0)
        {
            beesSound.Stop();
            beehiveHealth.gameObject.SetActive(false);
        }
    }

    private void ActivateBeehive()
    {
        if (roomBounds.Contains(player1.position) || roomBounds.Contains(player2.position))
        {
            if (!playerInside)
            {
                beesSound.Play();
                playerInside = true;
                beehiveScript.SpawnBees();
                beehiveHealth.gameObject.SetActive(true);
            }
        }
    }

    private void OpenDoor()
    {
        if (!beehive.gameObject.activeSelf)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ResetRoom()
    {
        playerInside = false;
        beehiveScript.ResetBeehive();
        beehive.gameObject.SetActive(true);
        beehiveHealth.gameObject.SetActive(false);
        beesSound.Stop();

        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }
}
