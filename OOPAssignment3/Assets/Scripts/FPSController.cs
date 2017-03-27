using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
	
	public float speed = 2f;
	public float sensitivity = 3f;
	CharacterController player;
	
	public GameObject camera;
	
	private float moveForwardBack;
	private float moveLeftRight;
	private float rotationX;
	private float rotationY;

	void Start () 
	{
		player = GetComponent<CharacterController>();
	}
	
	void Update () 
	{
		moveForwardBack = Input.GetAxis("Vertical") * speed;
		moveLeftRight = Input.GetAxis("Horizontal") * speed;
		
		rotationX = Input.GetAxis("Mouse X") * sensitivity;
		rotationY = Input.GetAxis("Mouse Y") * sensitivity;
		
		Vector3 movement = new Vector3(moveLeftRight, 0, moveForwardBack);
		movement = transform.rotation * movement;
		player.Move(movement * Time.deltaTime);
		
		camera.transform.Rotate(-rotationY,0,0);
		transform.Rotate(0,rotationX,0);
		
	}
}
