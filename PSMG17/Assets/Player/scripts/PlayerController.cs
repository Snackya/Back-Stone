using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 4f;

    private Rigidbody2D playerBody;

    private Vector3 movementInput;
    private Vector3 movementVelocity;

	// Use this for initialization
	void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        movementVelocity = movementInput * moveSpeed;
	}

    void FixedUpdate()
    {
        playerBody.velocity = movementVelocity;
    }
}
