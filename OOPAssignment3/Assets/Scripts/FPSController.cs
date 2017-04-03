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
		rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
		
		rotationY = Mathf.Clamp(rotationY, -70f, 70f);
		
		Vector3 movement = new Vector3(moveLeftRight, 0, moveForwardBack);
		
		transform.Rotate(0,rotationX,0);
		camera.transform.localRotation = Quaternion.Euler(rotationY,0,0);
		
		movement = transform.rotation * movement;		
		player.Move(movement * Time.deltaTime);
	}
}
