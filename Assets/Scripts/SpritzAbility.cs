using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritzAbility : PlayerAbility
{

    public ParticleSystem particles;

    public float abilityTime;
    public float rechargeTime;
    public float traceDistance;

    private float startTime;
    private float lastActiveTime;
	private Animator animator;

    void Start()
    {
        startTime = -1;
        lastActiveTime = Time.time;
		animator = GetComponent<Animator> ();
	}

    void Update()
    {
        if (startTime != -1)
        {
            if (Time.time - startTime >= abilityTime)
            {
                startTime = -1;
                particles.Stop();
                lastActiveTime = Time.time;
				animator.SetBool ("IsSpraying", false);
            }
        }

        if (Input.GetButtonDown(button) && Time.time - lastActiveTime >= rechargeTime)
        {
            startTime = Time.time;
            particles.Play();
			animator.SetBool ("IsSpraying", true);
        }
    }
}
