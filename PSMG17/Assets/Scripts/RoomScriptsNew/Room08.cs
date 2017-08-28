using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room08 : MonoBehaviour {

    private RoomDivider roomDivider;

    private void Start()
    {
        roomDivider = GetComponentInChildren<RoomDivider>();
    }

    public void ResetRoom()
    {
        roomDivider.ResetRoomDivider();
    }
    
}
