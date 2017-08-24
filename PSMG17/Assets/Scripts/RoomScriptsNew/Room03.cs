using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room03 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    private Bounds roomBounds;

    private bool playerInside = false;
    private GameObject glaves;

    void Start () {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        glaves = transform.FindChild("Glaves").gameObject;
    }
	
	void Update () {
        ActivateGlaves();
        DeactivateGlaves();
	}

    private void DeactivateGlaves()
    {
        if (!roomBounds.Contains(player1.position) && !roomBounds.Contains(player2.position))
        {
            if (playerInside)
            {
                playerInside = false;
                glaves.SetActive(false);
            }
        }
    }

    private void ActivateGlaves()
    {
        if (roomBounds.Contains(player1.position) || roomBounds.Contains(player2.position))
        {
            if (!playerInside)
            {
                playerInside = true;
                glaves.SetActive(true);
            }
        }
    }
}
