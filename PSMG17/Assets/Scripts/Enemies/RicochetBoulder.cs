using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBoulder : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //prevent slingshot damage on initial fire
        if(tag == "PlayerWeapon" && collision.gameObject.name == "Slingshot")
        {
            collision.gameObject.GetComponent<EnemyHealth>().health.CurrentVal -= 50;
            Destroy(gameObject);
        }       
    }
}
