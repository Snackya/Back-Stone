using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room07 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    private Bounds roomBounds;

    private bool playerInside = false;
    private GameObject glaves;
    private GameObject enemies;

    [SerializeField]
    private AudioSource glaivesSound;

    void Start ()
    {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        glaves = transform.FindChild("Glaves").gameObject;
        enemies = transform.FindChild("Enemies").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ActivateGlavesAndEnemies();
        DeactivateGlavesAndEnemies();
	}

    private void ActivateGlavesAndEnemies()
    {
        if (roomBounds.Contains(player1.position) || roomBounds.Contains(player2.position))
        {
            if (!playerInside)
            {
                glaivesSound.Play();
                playerInside = true;
                glaves.SetActive(true);
                enemies.SetActive(true);
            }
        }
    }

    private void DeactivateGlavesAndEnemies()
    {
        if (!roomBounds.Contains(player1.position) && !roomBounds.Contains(player2.position))
        {
            if (playerInside)
            {
                glaivesSound.Stop();
                playerInside = false;
                glaves.SetActive(false);
                enemies.SetActive(false);
            }
        }
    }
}
