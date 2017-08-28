using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBarrier : MonoBehaviour {

    private List<Transform> circles = new List<Transform>();
    private List<Transform> pillars = new List<Transform>();

    private bool circlesReactivating = false;
    private float pillarReactivationTime = 10f;
    private float circleReactivationTime = 2f;

    [HideInInspector]
    public bool barrierActive = true;

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
        ReactivateCircles();
	}

    private void ReactivateCircles()
    {
        int inactiveCircleCounter = 0;
        foreach (Transform circle in circles)
        {
            if (!circle.gameObject.activeSelf) inactiveCircleCounter++;
        }
        if (inactiveCircleCounter == circles.Count && !circlesReactivating)
        {
            circlesReactivating = true;
            barrierActive = false;
            StartCoroutine(ActivateCircles());
        }
    }

    private IEnumerator ActivateCircles()
    {
        for (int i = 0; i < pillars.Count; i++)
        {
            yield return new WaitForSeconds(pillarReactivationTime / pillars.Count);
            ActivatePillar(i);
        }
        for (int i = circles.Count - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(circleReactivationTime / circles.Count);
            circles[i].gameObject.SetActive(true);
        }
        barrierActive = true;
    }

    private void ActivatePillar(int index)
    {
        pillars[index].GetComponent<EnemyHealth>().health.CurrentVal = 
            pillars[index].GetComponent<EnemyHealth>().health.MaxVal;
        pillars[index].GetChild(0).gameObject.SetActive(true);
        pillars[index].GetChild(1).gameObject.SetActive(false);
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
