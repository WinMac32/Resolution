using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionParticle : MonoBehaviour
{

    public float expiry = 1;

    private float startTime;

    private CircleCollider2D circleCollider;
    private float height;
    private float originalScale;

    void Start()
    {
        startTime = Time.time;
        circleCollider = GetComponent<CircleCollider2D>();
        height = circleCollider.bounds.extents.y * 2;
        originalScale = transform.localScale.y;
    }

    void Update()
    {
        if (Time.time - startTime >= expiry)
        {
            Destroy(gameObject);
        }

        var amt = (Time.time - startTime) / expiry;
        transform.localScale = new Vector3(transform.localScale.x, originalScale - (amt / 2));
    }
}
