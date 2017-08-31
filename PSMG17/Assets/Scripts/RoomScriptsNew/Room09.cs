using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room09 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private Beehive[] beehiveScripts;
    [SerializeField]
    private Transform[] beehives;
    [SerializeField]
    private List<Slider> beehivesHealth;
    private Bounds roomBounds;
    private bool playerInside = false;
    private Transform door;

    [SerializeField]
    private AudioSource beesSound;

    // Use this for initialization
    void Start () {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        door = transform.FindChild("Door");
    }

    // Update is called once per frame
    void Update () {
        ActivateBeehives();
        OpenDoor();
        CheckIfBeehivesDestroyed();
    }

    private void CheckIfBeehivesDestroyed()
    {
        int deathCounter = 0;
        foreach (Transform beehive in beehives)
        {
            if (beehive.GetComponent<EnemyHealth>().health.CurrentVal == 0) deathCounter++;
        }
        if (deathCounter == beehives.Length)
        {
            DeactivateHealthBars();
        }
    }

    private void OpenDoor()
    {
        int count = 0;
        foreach (Transform beehive in beehives)
        {
            if (!beehive.gameObject.activeSelf) count++;
        }
        if (count == beehives.Length)
        {
            beesSound.Stop();
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void ActivateBeehives()
    {
        if (roomBounds.Contains(player1.position) || roomBounds.Contains(player2.position))
        {
            if (!playerInside)
            {
                beesSound.Play();
                playerInside = true;
                foreach (Beehive beehiveScript in beehiveScripts)
                {
                    beehiveScript.SpawnBees();
                    ActivateHealthBars();
                }
            }
        }
    }

    private void ActivateHealthBars()
    {
        foreach (Slider beehiveHealth in beehivesHealth)
        {
            beehiveHealth.gameObject.SetActive(true);
        }
    }

    private void DeactivateHealthBars()
    {
        foreach (Slider beehiveHealth in beehivesHealth)
        {
            beehiveHealth.gameObject.SetActive(false);
        }
    }

    public void ResetRoom()
    {
        playerInside = false;
        foreach (Transform beehive in beehives)
        {
            beehive.gameObject.SetActive(true);
        }
        foreach (Beehive beehiveScript in beehiveScripts)
        {
            beehiveScript.ResetBeehive();
        }
        DeactivateHealthBars();
        beesSound.Stop();
        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }
}
