using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    // TODO: Focus camera on one player, if the other one dies

    [SerializeField]
    private Transform[] players;
    [SerializeField]
    private GameObject[] areas;
    [SerializeField]
    private float maxOrthographicSize = 20f;

    private List<List<Transform>> rooms;
    private BoxCollider2D curRoomCollider;
    private Bounds curRoomBounds;
    private List<List<Bounds>> roomsBounds;

    private Camera gameCamera;
    private float zoomSpeed;
    private float dampTime = 0.5f;
    private Vector3 moveVelocity;
    private float[][] areaCamSizes = new float[][] {
        // Area 1
        new float[] { 5.5f, 7f, 6.5f, 8f },
        // Test Area
        new float[] { 5.5f, 8f, 10.5f},
        // Final Area 01
        new float[] { 8f, 11.3f, 14.5f, 8f, 5f, 9f, 16.5f, 11.5f, 17f, 7f, 7f, 17f, 11f, 10f, 8.5f, 13.5f, 11.5f, 9f, 12f, 23f}
    };


    private float[] testAreaCamSizes;


    private void Awake()
    {
        gameCamera = GetComponentInChildren<Camera>();
        rooms = new List<List<Transform>>();
        for (int i = 0; i < areas.Length; i++)
        {
            rooms.Add(new List<Transform>());
        }
        GetRooms();
        SetCameraToFirstRoom();
        GetAreaBounds(rooms);
    }

    private void GetAreaBounds(List<List<Transform>> rooms)
    {
        roomsBounds = new List<List<Bounds>>();
        for (int i = 0; i < rooms.Count; i++)
        {
            roomsBounds.Add(new List<Bounds>());
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = 0; j < rooms[i].Count; j++)
            {
                roomsBounds[i].Add(rooms[i][j].GetComponent<BoxCollider2D>().bounds);
            }
        }
    }

    private void GetRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            foreach (Transform room in areas[i].transform)
            {
                rooms[i].Add(room);
            }
        }
    }

    void Update()
    {
        SetCameraPosition();
    }

    private void SetCameraToFirstRoom()
    {
        // initially places the camera on the first room
        curRoomCollider = rooms[0][0].GetComponent<BoxCollider2D>();
        curRoomBounds = curRoomCollider.bounds;

        transform.position = curRoomBounds.center;
        gameCamera.orthographicSize = areaCamSizes[0][0];
    }

    private void SetCameraPosition()
    {
        Vector3 player1Pos = players[0].position;
        Vector3 player2Pos = players[1].position;

        for (int k = 0; k < roomsBounds.Count; k++)
        {
            for (int i = 0; i < roomsBounds[k].Count; i++)
            {
                for (int j = 0; j < roomsBounds[k].Count; j++)
                {
                    if (roomsBounds[k][j].Contains(player1Pos) && roomsBounds[k][i].Contains(player2Pos) ||
                    roomsBounds[k][j].Contains(player2Pos) && roomsBounds[k][i].Contains(player1Pos))
                    {
                        transform.position = Vector3.SmoothDamp(transform.position,
                            (roomsBounds[k][i].center + roomsBounds[k][j].center) / 2,
                            ref moveVelocity,
                            dampTime);
                        gameCamera.orthographicSize = Mathf.SmoothDamp(gameCamera.orthographicSize,
                            Math.Min(areaCamSizes[k][i] + areaCamSizes[k][j] * Math.Abs(j - i),
                            maxOrthographicSize),
                            ref zoomSpeed,
                            dampTime);
                    }
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
