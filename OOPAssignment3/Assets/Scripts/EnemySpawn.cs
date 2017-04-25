using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	
	public Vector3 room1Values;

	Vector3 room1SpawnPosition;

	public GameObject enemy;
	public FPSController fpscontroller;
	public float nextSpawn = 0f;
	public float waitTime;
	public int enemyCount = 0;
	
	void Update()
	{
		if(Time.time > nextSpawn && enemyCount < 60 && fpscontroller.GetComponent<FPSController>().isDead == false)
		{
			room1SpawnPosition = new Vector3(Random.Range(room1Values.x, room1Values.x + 20), room1Values.y, Random.Range(-room1Values.z, room1Values.z));
			
			SpawnEnemyRoom1();
			
			waitTime = Random.Range(1.5f,4f);
			nextSpawn += waitTime;
		}		
	}
	
	public void SpawnEnemyRoom1()
	{
		enemyCount ++;
		Instantiate(enemy, room1SpawnPosition, Quaternion.identity);
	}
}
