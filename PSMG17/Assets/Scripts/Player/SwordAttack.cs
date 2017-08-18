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

        if (other.gameObject.tag == "Basilisk")
        {
            if (other.gameObject.name.Contains("BasiliskScream"))
            {
                Destroy(other.gameObject);
            }
            else if (other.gameObject.name == "Headbutt")
            {
                
            }
            else
            {
                Debug.Log("Standard Attack on Basilisk.");
                other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 10;
            }
        }
    }
}
