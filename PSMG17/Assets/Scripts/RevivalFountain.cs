using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalFountain : MonoBehaviour {

    private HealthbarController hp;
    private GameObject player1;
    private GameObject player2;
    private Saferoom saferoom;

    private void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        saferoom = GetComponentInParent<Saferoom>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hp = collision.gameObject.GetComponent<HealthbarController>();
            hp.currentHealth = hp.maxHealth;

            if (!player1.activeSelf)
            {
                player1.transform.SetPositionAndRotation(new Vector3(saferoom.player1X, saferoom.player1Y, 0), new Quaternion());
                player1.SetActive(true);
                player1.GetComponent<HealthbarController>().currentHealth = player1.GetComponent<HealthbarController>().maxHealth;
            }
            if (!player2.activeSelf)
            {
                player2.transform.SetPositionAndRotation(new Vector3(saferoom.player2X, saferoom.player2Y, 0), new Quaternion());
                player2.SetActive(true);
                player2.GetComponent<HealthbarController>().currentHealth = player2.GetComponent<HealthbarController>().maxHealth;
            }
        }
    }
}
