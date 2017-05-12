using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    private GameObject activePlate;

    [HideInInspector]
    public bool isActive;

    void Start()
    {
        activePlate = transform.Find("Active").gameObject;
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // set pressure plate to active state and changes sprite, when a player enter it
        if (collision.gameObject.tag == "Player")
        {
            activePlate.SetActive(true);
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // resets plate, when player leaves it
        if (collision.gameObject.tag == "Player")
        {
            activePlate.SetActive(false);
            isActive = false;
        }
    }
}
