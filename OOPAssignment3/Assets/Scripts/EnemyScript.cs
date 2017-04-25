using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	
	public int health;
	public FPSController fpscontroller;
	public bool isDead = false;
	private float nextHealth;
	
	void Start ()
	{
		health = 50;
	}
	
	public void Die()
	{
		isDead = true;
		Destroy(gameObject, 3);
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player") && isDead == false)
		{
			other.GetComponent<FPSController>().health -= 0.3f;
		}
	}	
}
