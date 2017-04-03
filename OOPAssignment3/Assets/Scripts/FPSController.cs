using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
	
	public float speed = 2f;
	public float sensitivity = 3f;
	public float jumpSpeed = 5f;
	public GameObject camera;
	CharacterController player;
	
	private float moveForwardBack;
	private float moveLeftRight;
	private float rotationX;
	private float rotationY;
	private float vertVelocity;
	private bool canJump;

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
		
		//Mathf.Clamp limits the rotationY to between -70 and 70 degrees.
		rotationY = Mathf.Clamp(rotationY, -70f, 70f);
		
		Vector3 movement = new Vector3(moveLeftRight, vertVelocity, moveForwardBack);
		
		//transform.rotate will change the rotation of the object this script is attatched to
		transform.Rotate(0,rotationX,0);
		camera.transform.localRotation = Quaternion.Euler(rotationY,0,0); //Quaternion.Euler returns a rotation in the x,y and z axes.
		
		movement = transform.rotation * movement;		
		player.Move(movement * Time.deltaTime);
		
		if(player.isGrounded == true)
		{
			canJump = true; //canJump stops the player from jumping in the air
		}
		
		if(Input.GetButtonDown("Jump") && canJump == true)
		{
			vertVelocity += jumpSpeed;
			canJump = false;
		}
	}
	
	//FixedUpdate runs every second frame.
	void FixedUpdate()
	{
		if(!player.isGrounded)
		{
			vertVelocity += Physics.gravity.y * Time.deltaTime; //Downward velocity is force of gravity * time
		}
		else
		{
			vertVelocity = 0f; //resets velocity when player lands
		}
	}
}
