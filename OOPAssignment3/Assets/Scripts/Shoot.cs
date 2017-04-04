using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	private Transform myTransform;
	private float fireRate = 0.3f;
	private float nextFire;
	private RaycastHit hit;
	private float range = 300;
	
	void Start()
	{
		myTransform = transform;
	}
	
	void Update()
	{
		if(Input.GetButton("Fire1") && Time.time > nextFire) //Fire1 corresponds to Mouse0 (left click)
			{
				//TransformPoint allows you to set coordinates relative to transform
				Debug.DrawRay(myTransform.TransformPoint(0,0,1),myTransform.forward,Color.green,1);
				
				//transform refers to the transform of the object it's attached to
				if(Physics.Raycast(myTransform.position,myTransform.forward,out hit,range)) //the raycast returns information of the object it hits
				{
					Debug.Log(hit.transform.name + " hit.");
				}
				
				nextFire = Time.time + fireRate;
			}
	}
	
}