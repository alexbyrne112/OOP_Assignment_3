using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
	
	public float speed = 2f;
	CharacterController player;
	
	private float moveForwardBack;
	private float moveLeftRight;

	void Start () 
	{
		player = GetComponent<CharacterController>();
	}
	
	void Update () 
	{
		moveForwardBack = Input.GetAxis("Vertical") * speed;
		moveLeftRight = Input.GetAxis("Horizontal") * speed;
		
		Vector3 movement = new Vector3(moveLeftRight, 0, moveForwardBack);
		movement = transform.rotation * movement;
		player.Move(movement * Time.deltaTime);
		
	}
}
