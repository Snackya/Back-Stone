using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 4f;
    public int playerNumber = 1;

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
        movementInput = new Vector3(Input.GetAxis("Horizontal" + playerNumber), Input.GetAxis("Vertical" + playerNumber), 0f);
        movementVelocity = movementInput * moveSpeed;
	}

    void FixedUpdate()
    {
        playerBody.velocity = movementVelocity;
    }
}
