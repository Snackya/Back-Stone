using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBarrier : MonoBehaviour {

    private List<Transform> circles = new List<Transform>();
    private List<Transform> pillars = new List<Transform>();

	void Start ()
    {
        FillCirclesList();
        FillPillarsList();
	}

    private void FillCirclesList()
    {
        for (int i = 0; i < transform.FindChild("Circles").childCount; i++)
        {
            circles.Add(transform.FindChild("Circles").GetChild(i));
        }
    }

    private void FillPillarsList()
    {
        for (int i = 0; i < transform.FindChild("Pillars").childCount; i++)
        {
            pillars.Add(transform.FindChild("Pillars").GetChild(i));
        }
    }

    void Update ()
    {
        DeactivateCircles();
	}

    private void DeactivateCircles()
    {
        for (int i = 0; i < pillars.Count; i++)
        {
            if (pillars[i].GetChild(1).gameObject.activeSelf)
            {
                circles[i].gameObject.SetActive(false);
            }
        }
    }
}
