using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Script : MonoBehaviour {

    public PressurePlate firstPlate;
    public PressurePlate secondPlate;

    private GameObject gate;

	void Start () {
        gate = transform.Find("Gate").gameObject;
	}
	
	void Update () {
		if (firstPlate.isActive && secondPlate.isActive)
        {
            gate.SetActive(false);
        }
	}
}
