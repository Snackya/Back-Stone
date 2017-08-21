using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour {

    [SerializeField]
    private Transform teleporterSibling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Teleport(Collider2D collision)
    {
        yield return new WaitForSeconds(2f);
        collision.transform.GetComponent<Rigidbody2D>().MovePosition(teleporterSibling.position);
    }
}
