using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMechanism : MonoBehaviour {

    private List<Transform> pressureplates = new List<Transform>();
    private List<Transform> traps = new List<Transform>();

	// Use this for initialization
	void Start ()
    {
        FillPressureplatesList();
        FillTrapsList();
	}

    private void FillPressureplatesList()
    {
        for (int i = 0; i < transform.FindChild("PressurePlates").childCount; i++)
        {
            pressureplates.Add(transform.FindChild("PressurePlates").GetChild(i));
        }
    }

    private void FillTrapsList()
    {
        for (int i = 0; i < transform.FindChild("Traps").childCount; i++)
        {
            traps.Add(transform.FindChild("Traps").GetChild(i));
        }
    }

    // Update is called once per frame
    void Update ()
    {
        ActivateTraps();
	}

    private void ActivateTraps()
    {
        for (int i = 0; i < pressureplates.Count; i++)
        {
            if (pressureplates[i].GetChild(1).gameObject.activeSelf)
            {
                traps[i].GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                traps[i].GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
