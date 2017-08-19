using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBeeCount : MonoBehaviour {

    [SerializeField]
    private int maxChilds = 30;
    private int childCount;
	
	void Update () {
        childCount = transform.childCount;
        KillBees();
	}

    private void KillBees()
    {
        if (childCount > maxChilds)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
