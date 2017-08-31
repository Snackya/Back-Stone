using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {

    [SerializeField]
    private bool firstSet;
    private GameObject front;
    private Memory memory;


    private void Start()
    {
        front = transform.GetChild(0).gameObject;
        memory = transform.GetComponentInParent<Memory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerLegs")
        {
            if (firstSet && memory.firstSetFlipable)
            {
                front.SetActive(true);
            } 
            else if (!firstSet && memory.secondSetFlipable)
            {
                front.SetActive(true);
            }
        }
    }

    public void ResetCard()
    {
        front.SetActive(false);
    }
}
