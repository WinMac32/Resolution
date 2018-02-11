using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControler : MonoBehaviour {

    public ElevatorControler target;
    public PlayerController player;
    public GameObject platfrom;
    public bool isPlatfromHere;
    public float CD = 3f;
    public float currentCD = 0;
	// Use this for initialization
	void Start () {
        if (isPlatfromHere)
        {
            platfrom.SetActive(true);

        }
        else
        {
            platfrom.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        currentCD -= Time.deltaTime;
        if (isPlatfromHere)
        {
            platfrom.SetActive(true);
        }
        else
        {
            platfrom.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player")&&currentCD<=0)
        {
            player.transform.position = target.transform.position;
            platfrom.SetActive(false);
            isPlatfromHere = false;
            target.isPlatfromHere = true;
            target.currentCD = CD;

        }
    }
}
