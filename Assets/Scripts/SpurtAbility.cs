using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpurtAbility : PlayerAbility
{

    public Transform emitter;
    public Transform lotionParticle;

    public int amount = 10;
    public float speed = 10;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButton("Spurt"))
        {
            for (int i = 0; i < amount; i++)
            {
                var particle = Instantiate(lotionParticle);
                var body = particle.GetComponent<Rigidbody2D>();
                body.position = emitter.position + new Vector3((Random.value - 0.5f) * 0.5f, (Random.value - 0.5f) * 0.5f, 0);
                body.velocity += new Vector2(-speed, 0);
                body.angularVelocity += 5f;
            }
        }
    }
}
