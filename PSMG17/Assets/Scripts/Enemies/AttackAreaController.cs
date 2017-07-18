using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaController : MonoBehaviour {
    private GameObject parent;
    private Animator animator;
	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject;
        animator = parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
    }
}
