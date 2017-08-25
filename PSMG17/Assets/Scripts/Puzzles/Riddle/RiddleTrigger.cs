using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTrigger : MonoBehaviour {
    private bool hasAlreadyStarted;
    private GameObject riddleGuy;

    private void Start()
    {
        hasAlreadyStarted = false;
        riddleGuy = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !hasAlreadyStarted)
        {
            hasAlreadyStarted = true;
            riddleGuy.GetComponent<RiddleControl>().StartRiddles();
        }
    }
}
