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

    private Vector2 displacement;
    private Rigidbody2D RB2D;
    private bool isIdle = true;
    private float flipCD = 0.3f;
    private float currentFlipCd = 0f;
    private float isFacingRight = 1f;
    private bool isInitalized = false;
    private bool ignorePlayer;
    // private bool isGrounded = false;

    private float lastHit;

	// Use this for initialization
	void Start () {


	}

    private void Update()
    {
        currentFlipCd -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!isInitalized)
        {
            Initialize();
        }
        displacement = target.transform.position -transform.position;
        //Debug.Log("flipCD " + currentflipCd);
        if (displacement.magnitude < searchRange && !ignorePlayer)
        {
            RB2D.velocity=new Vector2(speed*getSign(displacement.x), RB2D.velocity.y);
            //fixedXSpeed();
            if (getSign(displacement.x) > 0) //enemy tracing player on the right
            {
                isFacingRight = 1;
            }
            else if(getSign(displacement.x) < 0) //enemy tracing player on the right
            {
                isFacingRight = -1;
            }
            isIdle = false;
        }
        else if (displacement.magnitude > searchRange)
        {
            isIdle = true;
            //fixedXSpeed();
            ignorePlayer = false;
        }
        // Debug.Log("isIdle = " + isIdle);
        // Debug.Log("speed: " + speed + ", velocity: " + RB2D.velocity.magnitude);
	}


    private void OnCollisionStay2D(Collision2D collision)
    {

       // Debug.Log("Position " + transform.position.x + "," + transform.position.y);
        if (collision.gameObject.tag.Equals("Edge") || collision.gameObject.tag.Equals("Enemy"))
        {

            if (isIdle)
            {
                if(currentFlipCd < 0)
                {
                    flip();
                    
                }
                
            }

  /*          else //currently tracing player
            {
                //don't jump off the edge
                Vector2 displacementToEdge = collision.otherCollider.transform.position - transform.position;
                if (getSign(displacement.x) == getSign(displacementToEdge.x)){
                    RB2D.velocity = (new Vector2(0, RB2D.velocity.y));
                }
            }
            */
        }
    }

    private void flip()
    {
        //Debug.Log("yeah");
        //Todo flip the sprite
        isFacingRight *= -1;
        RB2D.velocity = new Vector2(speed*isFacingRight, RB2D.velocity.y);
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
 //           print(this.gameObject.name + " :" );
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

    public void Damage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        // TODO
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Time.time - lastHit >= hitSpeed)
            {
                target.Damage(hitDamage);
                lastHit = Time.time;
                ignorePlayer = true;
                flip();
                isIdle = true;
                fixedXSpeed();
            }
        }
    }
}
