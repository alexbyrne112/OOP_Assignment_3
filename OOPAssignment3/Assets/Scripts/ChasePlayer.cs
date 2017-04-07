using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour 
{
	public LayerMask detectionLayer;
	private Transform myTransform;
	private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
	private Collider[] hitColliders;
	private float checkRate;
	private float nextCheck;
	private float detectionRadius = 50;

	void Start () {
		myTransform = transform;
		myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		checkRate = Random.Range(0.8f,1.2f);
	}
	
	void Update () 
	{
		CheckIfPLayerInRange();
		
	}
	
	void CheckIfPLayerInRange()
	{
		if(Time.time > nextCheck && myNavMeshAgent.enabled == true)
		{
			nextCheck = Time.time + checkRate;
				
			hitColliders = Physics.OverlapSphere(myTransform.position, detectionRadius, detectionLayer);
				
			if(hitColliders.Length > 0)
			{
				myNavMeshAgent.SetDestination(hitColliders[0].transform.position);
			}
		}
	}
}
