using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHitScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boulder")
        {
            collision.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.transform.GetComponent<Animator>().SetTrigger("Shatter");
        }
    }

}
