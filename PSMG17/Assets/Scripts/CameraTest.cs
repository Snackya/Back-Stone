using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour {

    [SerializeField]
    private Transform[] players;
    [SerializeField]
    private GameObject area;

    private List<Transform> rooms;
    private BoxCollider2D curRoomCollider;
    private Bounds curRoomBounds;

    private Camera gameCamera;
    private float zoomSpeed;
    private float dampTime = 0.5f;
    private Vector3 moveVelocity;
    private float[] area01CamSizes = new float[] { 5f, 6.5f, 6f, 7.5f };
    

    private void Awake()
    {
        gameCamera = GetComponentInChildren<Camera>();
        rooms = new List<Transform>();
        GetRooms();
        SetCameraToFirstRoom();
    }

    private void GetRooms()
    {
        foreach (Transform room in area.transform)
        {
            rooms.Add(room);
        }
    }

    void Update () {
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
        // iterating through the rooms
        for (int i = 0; i < rooms.Count; i++)
        {
            curRoomCollider = rooms[i].GetComponent<BoxCollider2D>();
            curRoomBounds = curRoomCollider.bounds;

            /* checking if the current room contains both players and smoothly sets the camera position
             * to the room if true */
            if (curRoomBounds.Contains(players[0].position) && curRoomBounds.Contains(players[1].position))
            {
                transform.position = Vector3.SmoothDamp(transform.position, curRoomBounds.center, ref moveVelocity, dampTime);
                gameCamera.orthographicSize = Mathf.SmoothDamp(gameCamera.orthographicSize, area01CamSizes[i], ref zoomSpeed, dampTime);
            }
        }
    }
}
