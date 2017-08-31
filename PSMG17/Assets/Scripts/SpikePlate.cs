using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlate : MonoBehaviour
{
    private GameObject activePlate;
    private BoxCollider2D col;

    [HideInInspector]
    public bool isActive;

    void Start()
    {
        activePlate = transform.Find("Active").gameObject;
        col = transform.GetComponent<BoxCollider2D>();
        col.enabled = false;
        isActive = false;
    }

    public void ActivateSpikes()
    {
        activePlate.SetActive(true);
        col.enabled = true;
    }

    public void DeactivateSpikes()
    {
        activePlate.SetActive(false);
        col.enabled = false;
    }
}
