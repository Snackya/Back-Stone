using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saferoom : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private BoxCollider2D room;
    private Bounds roomBounds;

    private bool playersInside = false;

    [SerializeField]
    private GameOverManager gameOverManager;


    public int player1X;
    public int player1Y;
    public int player2X;
    public int player2Y;

    void Start()
    {
        room = GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
    }


    void Update()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside) Debug.Log("Both players entered the room.");
            playersInside = true;
            gameOverManager.setNewRespawnPosition(new Vector3(player1X, player1Y, 0), new Vector3(player2X, player2Y, 0));
        }
    }


    public void resetRoom()
    {
        playersInside = false;
    }
}

