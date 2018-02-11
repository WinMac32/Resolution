using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform cameraTransform;
    public float walkSpeed;
    public float jumpAmount;

    private float colliderHeight;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;

    private GameManager gameManager;

    private LayerMask layerMask;

    private bool jump;

	public float attackAnimLength = 1.33f;

    public bool left { get; private set; }
	private Animator animator;
	private float attackTime = 0f;
	private bool isBeingAttacked = false;
	private bool isDead = false;

    void Start()
    {
        gameManager = GameManager.instance;
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

		if (isBeingAttacked) {
			playerBody.velocity = new Vector2 (0, 0);
			attackTime += Time.deltaTime;
			if (attackTime > attackAnimLength) {
				isBeingAttacked = false;
			}
		}
    }

    private void FixedUpdate()
    {
		if (!isBeingAttacked && !isDead) {
			float xAxis = Input.GetAxisRaw ("Horizontal");

			if (xAxis < 0) {
				Flip (-1);
				left = true;
			} else if (xAxis > 0) {
				Flip (1);
				left = false;
			}

			Vector2 velocity = new Vector2 (xAxis * walkSpeed, playerBody.velocity.y);
			playerBody.velocity = velocity;
			animator.SetFloat ("Running", Mathf.Abs (velocity.x));

			if (jump && isGrounded ()) {
				playerBody.velocity += new Vector2 (0, jumpAmount);
			}

			jump = false;
		}
    }

    private bool isGrounded()
    {
        return Physics2D.CircleCast(playerBody.position,0.1f, -Vector2.up, colliderHeight * 2f + 0.1f, layerMask);
    }

    private void Flip(float mult)
    {
        transform.localScale = new Vector2(mult * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    //player getting hit
    public void Damage(int lotion)
    {
		if (!gameManager.lotionManager.UseLotion (lotion)) {
			Kill ();
		} 
    }

	public void getAttacked(int lotion) {
		Damage (lotion);
		isBeingAttacked = true;
		attackTime = 0f;
	}
    public void Kill()
    {
		animator.SetTrigger ("IsDying");
		playerBody.velocity = new Vector2 (0, 0);
		isDead = true;
    }

}
