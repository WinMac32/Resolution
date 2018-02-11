using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    private PlayerController player;
    // Use this for initialization
    public float DoTPeriod = 0.2f;  // how long will it damage the player for the next time
    public int DoTDamage = 2; // how much damage does it damage the player for each time
    private float currentTime = 0;
    private float smokeTimer = 0.1f;
    private float currentSmokeTime = 0.1f;
    public GameObject[] smokes;
    private int i = 0;
	void Start () {
        foreach(GameObject smoke in smokes)
        {
            smoke.SetActive(false);
        }
        smokes[i].SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && currentTime<0)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.Damage(DoTDamage);
            currentTime = DoTPeriod;
        }
    }

    // Update is called once per frame
    void Update () {
        currentTime -= Time.deltaTime;

        if (currentSmokeTime >= 0)
        {
            currentSmokeTime -= Time.deltaTime;
        }
        else
        {
            smokes[i].SetActive(false);
            if (i < smokes.Length-1)
            {
                i++;
            }
            else {
                i = 0;
            }
            
            smokes[i].SetActive(true);
            currentSmokeTime = smokeTimer;
        }
	}
}
