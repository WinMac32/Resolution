using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public PlayerController target;
    public float searchRange = 5f;
    public int maxHP = 3;
    public int currentHP;
    public float speed = 2;
    public float hitSpeed;
    public int hitDamage;
	public float attackAnimLength = 1.4f;
	public float deathAnimLength = 1.5f;

    private Vector2 displacement;
    private Rigidbody2D RB2D;
    private bool isIdle = true;
    private float flipCD = 0.3f;
    private float currentFlipCd = 0f;
    private float isFacingRight = 1f;
    private bool isInitalized = false;
    private bool ignorePlayer;
	private bool isAttacking = false;
	private bool isDying = false;
	private float attackTime = 0f;
	private float dyingTime = 0f;
    // private bool isGrounded = false;
	private Animator animator;
    private Vector3 startAscendPosition;
    private Vector3 targetAscendPosition;

    private float lastHit;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
        isIdle = true;
	}

    private void Update()
    {
        currentFlipCd -= Time.deltaTime;
		attackTime += Time.deltaTime;
		dyingTime += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!isInitalized)
        {
            Initialize();
        }
		if (!isAttacking && !isDying) {
			displacement = target.transform.position - transform.position;
			//Debug.Log("flipCD " + currentflipCd);
			if (displacement.magnitude < searchRange && !ignorePlayer) {
				RB2D.velocity = new Vector2 (speed * getSign (displacement.x), RB2D.velocity.y);
				//fixedXSpeed();
				if (getSign (displacement.x) > 0) { //enemy tracing player on the right
					isFacingRight = 1;
				} else if (getSign (displacement.x) < 0) { //enemy tracing player on the right
					isFacingRight = -1;
				}
				isIdle = false;
			} else if (displacement.magnitude > searchRange) {
				isIdle = true;
				//fixedXSpeed();
				ignorePlayer = false;
			}
		} else if (isDying) {
			if (dyingTime > deathAnimLength) {
				DestroyObject(gameObject);
			}

            transform.position = Vector3.Lerp(startAscendPosition, targetAscendPosition, dyingTime / deathAnimLength);
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            Color color = render.color;
            color.a = 1 - (dyingTime / deathAnimLength);
            render.color = color;
		} else {

			if (attackTime > attackAnimLength) {
				flip();
				isAttacking = false;
				animator.SetTrigger ("attack");
			}
		}
	}


    private void OnCollisionStay2D(Collision2D collision)
    {
		if (!isDying) {
			if (collision.gameObject.tag.Equals ("Edge") || collision.gameObject.tag.Equals ("Enemy")) {

				if (isIdle) {
					if (currentFlipCd < 0) {
						flip ();
                    
					}
                
				}
			}
		}
    }

    private void flip()
    {
        isFacingRight *= -1;
        RB2D.velocity = new Vector2(speed*isFacingRight, RB2D.velocity.y);
		transform.localScale = new Vector2(isFacingRight * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        fixedXSpeed();
        currentFlipCd = flipCD;
    }
    private float getSign(float x)
    {
        if (x > 0)
        {
            return 1;
        }

        else if (x < 0)
        {
            return -1;
        }
        return 0;
    }


    private void fixedXSpeed()
    {
        if (RB2D.velocity.magnitude > 0)
        {
            RB2D.velocity = RB2D.velocity.normalized * speed;//now the speed is normalized
        }

    }

    private void Initialize()
    {
        currentHP = maxHP;
        RB2D = GetComponent<Rigidbody2D>();
        displacement = target.transform.position - transform.position;
        if (displacement.magnitude > searchRange)
        {

            isIdle = true;
            RB2D.velocity = new Vector2(2f, 0f);
            fixedXSpeed();
        }
        isInitalized = true;
    }

    //enemy getting hit

    public void Damage(int amount)
    {
		if (!isDying) {
			currentHP -= amount;
			if (currentHP <= 0) {
				Kill ();
			}
		}
    }

    public void Kill()
    {
        DropSpawner[] spawners = FindObjectsOfType<DropSpawner>();
        DropSpawner minSpawner = null;
        float minDist = 0;
        foreach (DropSpawner spawner in spawners)
        {
            float dist = (spawner.transform.position - transform.position).magnitude;
            if (minSpawner == null || minDist > dist)
            {
                minSpawner = spawner;
                minDist = dist;
            }
        }

		if (minSpawner != null) {
        	minSpawner.spawn();
		}
		isDying = true;
		dyingTime = 0f;
		RB2D.velocity = new Vector2 (0, 0);
		animator.SetTrigger ("ascend");

        Vector3 camTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        targetAscendPosition = new Vector3(transform.position.x, camTop.y, transform.position.z);

        startAscendPosition = transform.position;

        RB2D.simulated = false;

        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
        }

        AudioManager.instance.PlayAudio(audioSource, SFX.Ascend);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (!isDying) {
			if (collision.gameObject.tag == "Player") {
				if (Time.time - lastHit >= hitSpeed) {
					animator.SetTrigger ("attack");
					target.getAttacked (hitDamage);
					isAttacking = true;
					attackTime = 0.0f;
					RB2D.velocity = new Vector2 (0, 0);
					lastHit = Time.time;
					ignorePlayer = true;
					isIdle = true;
					fixedXSpeed ();

                    AudioSource audioSource = GetComponent<AudioSource>();

                    if (audioSource == null)
                    {
                        audioSource = this.gameObject.AddComponent<AudioSource>();
                    }

                    AudioManager.instance.PlayAudio(audioSource, SFX.PlayerHit);
				}
			}
		}
    }
}
