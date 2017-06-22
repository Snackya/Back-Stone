using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 40;
            other.gameObject.GetComponent<EnemyAI>().Knockback();
        }

        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("Standard Attack on Boss.");
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 10;
        }
    }
}
