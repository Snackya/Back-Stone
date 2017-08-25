using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandStand : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().swordEquipped = false;
        }
    }
}
