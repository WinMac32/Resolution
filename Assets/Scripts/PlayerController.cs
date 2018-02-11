using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform cameraTransform;
    public float walkSpeed;
    public float jumpAmount;
    public Transform[] abilityEmitters;

    private float colliderHeight;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;

    private GameManager gameManager;

    private LayerMask layerMask;

    private bool jump;

    private bool left;

	private Animator animator;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        colliderHeight = playerCollider.bounds.extents.y;
        layerMask = LayerMask.GetMask("Ground");
		animator = GetComponent<Animator> ();
    }

    void Update()
    {
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

        if (xAxis < 0)
        {
            Flip(-1);
            foreach (var trans in abilityEmitters)
            {
                trans.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            }
        }
        else if (xAxis > 0)
        {
            Flip(1);
            foreach (var trans in abilityEmitters)
            {
                trans.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
            }
        }

        Vector2 velocity = new Vector2(xAxis * walkSpeed, playerBody.velocity.y);
        playerBody.velocity = velocity;
		animator.SetFloat ("Running", Mathf.Abs(velocity.x));

        if (jump && isGrounded())
        {
            playerBody.velocity += new Vector2(0, jumpAmount);
        }

        jump = false;
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(playerBody.position, -Vector2.up, colliderHeight * 2f + 0.1f, layerMask);
    }

    private void Flip(float mult)
    {
        transform.localScale = new Vector2(mult * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}
