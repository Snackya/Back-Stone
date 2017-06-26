using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private Transform[] players;
    [SerializeField]
    private GameObject area;
    [SerializeField]
    private float maxOrthographicSize = 20f;

    private List<Transform> rooms;
    private BoxCollider2D curRoomCollider;
    private Bounds curRoomBounds;
    private List<Bounds> roomsBounds;

    private Camera gameCamera;
    private float zoomSpeed;
    private float dampTime = 0.5f;
    private Vector3 moveVelocity;
    private float[] area01CamSizes = new float[] { 5.5f, 7f, 6.5f, 8f };


    private void Awake()
    {
        gameCamera = GetComponentInChildren<Camera>();
        rooms = new List<Transform>();
        GetRooms();
        SetCameraToFirstRoom();
        GetCurrentAreaBounds(rooms);
    }

    private void GetCurrentAreaBounds(List<Transform> rooms)
    {
        roomsBounds = new List<Bounds>();

        for (int i = 0; i < rooms.Count; i++)
        {
            roomsBounds.Add(rooms[i].GetComponent<BoxCollider2D>().bounds);
        }
    }

    private void GetRooms()
    {
        foreach (Transform room in area.transform)
        {
            rooms.Add(room);
        }
    }

    void Update()
    {
        SetCameraPosition();
    }

    private void SetCameraToFirstRoom()
    {
        // initially places the camera on the first room
        curRoomCollider = rooms[0].GetComponent<BoxCollider2D>();
        curRoomBounds = curRoomCollider.bounds;

        transform.position = curRoomBounds.center;
        gameCamera.orthographicSize = area01CamSizes[0];
    }

    private void SetCameraPosition()
    {
        Vector3 player1Pos = players[0].position;
        Vector3 player2Pos = players[1].position;

        for (int i = 0; i < roomsBounds.Count; i++)
        {
            for (int j = 0; j < roomsBounds.Count; j++)
            {
                if (roomsBounds[j].Contains(player1Pos) && roomsBounds[i].Contains(player2Pos) ||
                roomsBounds[j].Contains(player2Pos) && roomsBounds[i].Contains(player1Pos))
                {
                    transform.position = Vector3.SmoothDamp(transform.position,
                        (roomsBounds[i].center + roomsBounds[j].center) / 2,
                        ref moveVelocity,
                        dampTime);
                    gameCamera.orthographicSize = Mathf.SmoothDamp(gameCamera.orthographicSize,
                        Math.Min(area01CamSizes[i] + area01CamSizes[j] * Math.Abs(j - i),
                        maxOrthographicSize),
                        ref zoomSpeed,
                        dampTime);
                }
            }
        }
    }

    /* Old function, only viable for if players are located max 1 room from another
     * 
    private void SetCameraPosition()
    {
        Vector3 player1Pos = players[0].position;
        Vector3 player2Pos = players[1].position;

        // iterating through the rooms
        for (int i = 0; i < rooms.Count; i++)
        {
            curRoomCollider = rooms[i].GetComponent<BoxCollider2D>();
            curRoomBounds = curRoomCollider.bounds;

            if (i != rooms.Count - 1) nextRoomBounds = rooms[i + 1].GetComponent<BoxCollider2D>().bounds;

            /* checking if the current room contains both players and smoothly sets the camera position
             * to the room if true *
            if (curRoomBounds.Contains(player1Pos) && curRoomBounds.Contains(player2Pos))
            {
                transform.position = Vector3.SmoothDamp(transform.position, 
                    curRoomBounds.center, 
                    ref moveVelocity, 
                    dampTime);
                gameCamera.orthographicSize = Mathf.SmoothDamp(gameCamera.orthographicSize, 
                    area01CamSizes[i], 
                    ref zoomSpeed, 
                    dampTime);
            }
            /* if one player is in another room, than the other one, the camera focuses on the center 
             * of those two rooms *
            else if (nextRoomBounds.Contains(player1Pos) && curRoomBounds.Contains(player2Pos) || 
                nextRoomBounds.Contains(player2Pos) && curRoomBounds.Contains(player1Pos))
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                    (curRoomBounds.center + nextRoomBounds.center) / 2,
                    ref moveVelocity,
                    dampTime);
            }
        }
    }*/
}
