using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter1
{
	public class Chase : MonoBehaviour 
	{

		public LayerMask detectionLayer;
		private Transform myTransform;
		private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
		private Collider[] hitColliders;
		private float checkRate;
		private float nextCheck;
		private float detectionRadius = 50;

		void Start () 
		{
			SetInitialReferences();
		}
		
		void Update () 
		{
			CheckIfPlayerInRange();
		}
		
		void SetInitialReferences()
		{
			myTransform = transform;
			myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			//Check rate is random so every AI does not check at the same time
			checkRate = Random.Range(0.8f,1.2f);
		}
		
		void CheckIfPlayerInRange()
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
}