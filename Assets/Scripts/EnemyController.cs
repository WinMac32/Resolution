using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public PlayerController target;
    public float searchRange = 5f;
    public int maxHP = 3;
    public int currentHP;
    public float speed = 2;
    private Vector2 displacement;
    private Rigidbody2D RB2D;
    private bool isIdle = true;
    private float flipCD = 0.2f;
    private float currentFlipCd = 0f;
    private float isFacingRight = 1f;


	// Use this for initialization
	void Start () {
        currentHP = maxHP;
        RB2D = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        currentFlipCd -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate () {

        displacement = target.transform.position -transform.position;
        //Debug.Log("flipCD " + currentflipCd);
        if (displacement.magnitude < searchRange)
        {
            RB2D.velocity=new Vector2(speed*getSign(displacement.x), RB2D.velocity.y);
            fixedXSpeed();
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
            fixedXSpeed();
            
        }
        // Debug.Log("isIdle = " + isIdle);
        Debug.Log("speed: " + speed + ", velocity: " + RB2D.velocity.magnitude);
	}


    private void OnCollisionStay2D(Collision2D collision)
    {
       // Debug.Log("Position " + transform.position.x + "," + transform.position.y);
        if (collision.gameObject.tag.Equals("Edge"))
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
        Debug.Log("yeah");
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
            RB2D.velocity = RB2D.velocity.normalized * speed;//now the speed is normalized
        }

        RB2D.velocity *= (speed / Mathf.Abs( RB2D.velocity.x)); // now the x component of the velocity is equal to the designed speed, and the overall speed is scaled acoordingly
    }
}
