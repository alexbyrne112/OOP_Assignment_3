using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeExplosion : MonoBehaviour 
{
	
	private Collider[] hitColliders;
	public float blastRadius;
	public FPSController fpscontroller;
	public float explosionPower;

	//Called when the rigidbody contacts another collider
	void OnCollisionEnter(Collision col)
	{
		ExplosionWork(col.contacts[0].point);
		Destroy(gameObject);
	}
	
	void ExplosionWork(Vector3 explosionPoint)
	{
		//Function OverlapSphere returns vector3 of any object within given area(radius)
		hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius);
		
		foreach(Collider hitCol in hitColliders)
		{
			//If the object has a navmesh agent, disable it.
			if(hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
			{
				hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
			}
			
			if(hitCol.GetComponent<Rigidbody>() != null && hitCol.CompareTag("Enemy"))
			{
				hitCol.GetComponent<Rigidbody>().isKinematic = false;
				//Parameters are force, position, radius, upwards modifier, force type.
				hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
				hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
				hitCol.GetComponent<EnemyScript>().Die();
			}
		}
	}
}