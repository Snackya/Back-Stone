using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTrigger : MonoBehaviour {
    private bool hasAlreadyStarted;
    private GameObject riddleGuy;
    private Transform door;
    private Transform backDoor;
    private Room19 roomManager;

    private void Start()
    {
        hasAlreadyStarted = false;
        riddleGuy = transform.parent.gameObject;
        door = transform.parent.parent.FindChild("Door");
        backDoor = transform.parent.parent.FindChild("Backdoor");
        roomManager = transform.parent.parent.GetComponent<Room19>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !hasAlreadyStarted)
        {
            hasAlreadyStarted = true;
            roomManager.closeDoors();
            riddleGuy.GetComponent<RiddleControl>().StartRiddles();
        }
    }
}
