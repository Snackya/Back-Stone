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

    private bool playersInside = false;

    private Beehive beehiveScript;
    private Transform beehive;
    private Transform door;
    private Transform backdoor;

    [SerializeField]
    private AudioSource beesSound;

    void Start()
    {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        beehive = transform.FindChild("Beehive");
        beehiveScript = beehive.GetComponent<Beehive>();
        door = transform.FindChild("Door");
        backdoor = transform.FindChild("Backdoor");
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
            backdoor.GetChild(0).gameObject.SetActive(true);
            backdoor.GetChild(1).gameObject.SetActive(false);
            beesSound.Stop();
            beehiveHealth.gameObject.SetActive(false);
        }
    }

    private void ActivateBeehive()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside)
            {
                backdoor.GetChild(0).gameObject.SetActive(false);
                backdoor.GetChild(1).gameObject.SetActive(true);
                beesSound.Play();
                beehive.gameObject.SetActive(true);
                playersInside = true;
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
        playersInside = false;
        beehive.gameObject.SetActive(false);
        beehiveScript.ResetBeehive();
        beehiveHealth.gameObject.SetActive(false);
        beesSound.Stop();

        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
        backdoor.GetChild(0).gameObject.SetActive(true);
        backdoor.GetChild(1).gameObject.SetActive(false);
    }
}
