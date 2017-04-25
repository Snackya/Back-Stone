using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
	}

    private void Attack()
    {
        if (Input.GetButtonDown("Attack1"))
        {
            Debug.Log("it's working");
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("attackTrigger");
        }
    }
}
