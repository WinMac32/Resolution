using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    private PlayerController player;
    // Use this for initialization
    public float DoTPeriod = 0.2f;  // how long will it damage the player for the next time
    public int DoTDamage = 2; // how much damage does it damage the player for each time
    private float currentTime = 0;
	void Start () {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("hello");
        if (collision.gameObject.tag.Equals("Player") && currentTime<0)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.Damage(DoTDamage);
            currentTime = DoTPeriod;
            print("hi");
        }
    }

    // Update is called once per frame
    void Update () {
        currentTime -= Time.deltaTime;
	}
}
