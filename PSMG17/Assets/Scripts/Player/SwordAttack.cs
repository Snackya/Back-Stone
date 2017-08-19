﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.name == "Arrow(Clone)")
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 45;
                other.gameObject.GetComponent<EnemyAI>().Knockback();
            }
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
                other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 15;
            }
        }
        if (other.gameObject.name == "Beehive")
        {
            other.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 10;
        }
        if (other.gameObject.tag == "Bee")
        {
            Destroy(other.gameObject);
        }
    }
}
