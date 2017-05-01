using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    public float knockbackPower = 150f;
    private bool hasBeenAttacked = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !hasBeenAttacked)
        {
            hasBeenAttacked = true;
            Vector3 knockback = (other.transform.position - transform.position);
            other.attachedRigidbody.AddForce(knockback * knockbackPower);
            Debug.Log("Penis");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        hasBeenAttacked = false;
    }
}