using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrapScript : MonoBehaviour {

    private float maxActiveTime = 1f;
    private float activeTime;

	private void OnEnable()
    {
        activeTime = maxActiveTime;
    }
	
	void Update ()
    {
        activeTime -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && activeTime >= 0)
        {
            collision.gameObject.GetComponent<HealthbarController>().currentHealth -= 10;
        }
        else if (collision.gameObject.tag == "Enemy" && activeTime >= 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
