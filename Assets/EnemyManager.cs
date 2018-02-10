using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform[] swampPoints;
    public PlayerController player;
    public EnemyController enemyPreFab;
    private EnemyController enemy;

    // Use this for initialization
    void Start () {
        print("1");
		if (swampPoints.Length > 0)
        {
            print("2");
            for (int i=0; i < swampPoints.Length; i++)
            {
                print("3");
                enemy= Instantiate(enemyPreFab, swampPoints[i].position,Quaternion.identity) as EnemyController;
                enemy.target = player;
            }
            
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
