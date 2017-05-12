using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOpener : MonoBehaviour {

    public PressurePlate leftPlate;
    public PressurePlate rightPlate;

    private GameObject leftGate;
    private GameObject rightGate;
    private GameObject roomSeperator1;
    private GameObject roomSeperator2;
    private GameObject bottomGate;

	void Start () {
        // initializing gates
        leftGate = transform.Find("LeftGate").gameObject;
        rightGate = transform.Find("RightGate").gameObject;
        roomSeperator1 = transform.Find("RoomSeperator1").gameObject;
        roomSeperator2 = transform.Find("RoomSeperator2").gameObject;
        bottomGate = transform.Find("BottomGate").gameObject;
    }
	
	void Update () {
		if (leftPlate.isActive && rightPlate.isActive)
        {
            // opens gates for players to progress and closes others to seperate them
            leftGate.SetActive(false);
            rightGate.SetActive(false);
            roomSeperator1.SetActive(true);
            roomSeperator2.SetActive(true);
            bottomGate.SetActive(true);
        }
	}
}
