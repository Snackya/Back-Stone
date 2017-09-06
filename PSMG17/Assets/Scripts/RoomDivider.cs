using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDivider : MonoBehaviour {

    private Transform pressurePlate1;
    private Transform pressurePlate2;
    private Transform entrance;
    private Rigidbody2D entranceRB;
    private Transform exit;

    private bool roomDivided = false;
    private float roomDividerSpeed = 12f;

    [SerializeField]
    private bool isRoom08;
    private Room08 room08;
    [SerializeField]
    private bool isRoom13;
    private Room13 room13;

    [SerializeField]
    private AudioSource roomDividerSound;

	void Start ()
    {
        pressurePlate1 = transform.FindChild("PressurePlates").GetChild(0);
        pressurePlate2 = transform.FindChild("PressurePlates").GetChild(1);
        entrance = transform.FindChild("Entrance");
        entranceRB = entrance.GetComponent<Rigidbody2D>();
        exit = transform.FindChild("Exit");

        if (isRoom08) room08 = GetComponentInParent<Room08>();
        if (isRoom13) room13 = GetComponentInParent<Room13>();
    }
	
	void Update ()
    {
        DivideRoom();
        if (isRoom13) if (room13.memoryComplete) OpenExit();
        if (isRoom08) if (room08.enemiesSpawned == room08.maxEnemies && room08.AllEnemiesDead()) OpenExit();
	}

    private void OpenExit()
    {
        exit.GetChild(1).gameObject.SetActive(true);
        exit.GetChild(0).gameObject.SetActive(false);
    }

    private void DivideRoom()
    {
        if (pressurePlate1.GetChild(1).gameObject.activeSelf && pressurePlate2.GetChild(1).gameObject.activeSelf && !roomDivided)
        {
            roomDivided = true;
            if (isRoom13) room13.canKillBothPlayers = true;
            StartCoroutine(MoveRoomDivider(-roomDividerSpeed));
        }
    }

    private IEnumerator MoveRoomDivider(float speed)
    {
        roomDividerSound.Play();

        entrance.GetChild(1).gameObject.SetActive(true);
        entrance.GetChild(0).gameObject.SetActive(false);
        entranceRB.velocity = new Vector2(speed, 0);
        yield return new WaitForSecondsRealtime(0.3f);
        entranceRB.velocity = new Vector2(0, 0);
        entrance.GetChild(0).gameObject.SetActive(true);
        entrance.GetChild(1).gameObject.SetActive(false);
    }

    public void ResetRoomDivider()
    {
        roomDivided = false;
        StartCoroutine(MoveRoomDivider(roomDividerSpeed));
        exit.GetChild(0).gameObject.SetActive(true);
        exit.GetChild(1).gameObject.SetActive(false);
    }
}
