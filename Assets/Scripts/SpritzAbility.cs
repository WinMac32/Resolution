using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritzAbility : PlayerAbility
{

    public ParticleSystem particles;
    public Transform projectile;

    private PlayerController player;

    public float abilityTime;
    public float rechargeTime;
    public float traceDistance;
    public float lotionCost = 10f;
    public float projectileSpeed = 5f;
	public float projectileYOffset = 0.15f;
    public int damage;

    private float startTime;
    private float lastActiveTime;

    private LotionManager lotionManager;
	private Animator animator;

    void Start()
    {
        player = GetComponent<PlayerController>();
        lotionManager = GameManager.instance.lotionManager;
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

        if (Input.GetButtonDown(button) && Time.time - lastActiveTime >= rechargeTime && lotionManager.UseLotion(lotionCost))
        {
            Quaternion rotation = Quaternion.Euler(0, player.left ? 180 : 0, 0);
            particles.transform.rotation = rotation;

            lastActiveTime = Time.time;
            startTime = Time.time;

            particles.Play();

            var velocity = rotation * new Vector3(projectileSpeed, 0, 0);

            var instance = Instantiate(projectile);
            instance.GetComponent<AbilityProjectile>().damage = damage;
            Rigidbody2D body = instance.GetComponent<Rigidbody2D>();
			body.position = transform.position + new Vector3(0, projectileYOffset, 0);
            body.velocity = velocity;

			animator.SetBool ("IsSpraying", true);
        }
    }
}
