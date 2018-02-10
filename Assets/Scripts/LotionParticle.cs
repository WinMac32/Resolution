using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionParticle : MonoBehaviour
{

    public float expiry = 1;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime >= expiry)
        {
            Destroy(gameObject);
        }
    }
}
