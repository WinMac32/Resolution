using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritzAbility : PlayerAbility
{

    public ParticleSystem particles;

    public float abilityTime;
    public float rechargeTime;
    public float traceDistance;
    public float lotionCost = 10f;

    private float startTime;
    private float lastActiveTime;

    private LotionManager lotionManager;

    void Start()
    {
        lotionManager = GameManager.instance.lotionManager;
        startTime = -1;
        lastActiveTime = Time.time;
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
            }
        }

        if (Input.GetButtonDown(button) && Time.time - lastActiveTime >= rechargeTime && lotionManager.UseLotion(lotionCost))
        {
            startTime = Time.time;
            particles.Play();
        }
    }
}
