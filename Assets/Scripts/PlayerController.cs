using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform cameraTransform;
    public float walkSpeed;
    public float jumpAmount;

    private float colliderHeight;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;

    private LayerMask layerMask;

    private bool jump;

	void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        colliderHeight = playerCollider.bounds.extents.y;
        layerMask = LayerMask.GetMask("Ground");
	}
	
	void Update () {
        Vector2 position = playerBody.position;
        cameraTransform.position = new Vector3(position.x, position.y, cameraTransform.position.z);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
	}

    private void FixedUpdate()
    {
        var xAxis = Input.GetAxisRaw("Horizontal");

        Vector2 velocity = new Vector2(xAxis * walkSpeed, playerBody.velocity.y);
        playerBody.velocity = velocity;

        if (jump)
        {
            Debug.Log(isGrounded());
        }

        if (jump && isGrounded())
        {
            playerBody.velocity += new Vector2(0, jumpAmount);
        }

        jump = false;
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(playerBody.position, -Vector2.up, colliderHeight * 2f+0.1f, layerMask);
    }
}
