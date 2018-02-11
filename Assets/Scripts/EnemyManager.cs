using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform[] swampPoints;
    public PlayerController player;
    public EnemyController enemyPreFab;
	public float xSpawnOffset;
	public float ySpawnOffset;
	private EnemyController enemy;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
		if (swampPoints.Length > 0)
        {
            for (int i=0; i < swampPoints.Length; i++)
            {
				Vector3 spawnPosition = new Vector3 (swampPoints[i].position.x + xSpawnOffset, 
					swampPoints[i].position.y + ySpawnOffset, 
					swampPoints[i].position.z);
                enemy= Instantiate(enemyPreFab, spawnPosition, Quaternion.identity) as EnemyController;
                enemy.target = player;
            }
            
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
