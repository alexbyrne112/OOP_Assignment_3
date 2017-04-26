using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPSController : MonoBehaviour {
	
	AsyncOperation ao;
	AsyncOperation ap;
	public float speed = 2f;
	public float sensitivity = 3f;
	public float jumpSpeed = 5f;
	public GameObject camera;
	public int score;
	public float health = 100f;
	public Text scoreText;
	public Text healthText;
	public Text gameOverText;
	
	CharacterController player;
	
	private float moveForwardBack;
	private float moveLeftRight;
	private float rotationX;
	private float rotationY;
	private float vertVelocity;
	private bool canJump;
	public bool isDead;
	private bool playerKnockedOver = false;

	void Start () 
	{
        Cursor.visible = false;
        player = GetComponent<CharacterController>();
		isDead = false;
		score = 1000;
		SetText();
    }
	
	void Update () 
	{
		if(health > 0)
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
			
			SetText();
		}
		else
		{
			gameOverText.text = "You are dead. Press F to return to the menu.";
			if(playerKnockedOver == false)
			{
				player.GetComponent<CapsuleCollider>().isTrigger = false;
				player.GetComponent<Rigidbody>().isKinematic = false;
				player.GetComponent<Rigidbody>().AddForce(10, -5, 10, ForceMode.Impulse);
				playerKnockedOver = true;
			}
			
			if(Input.GetButton("Submit"))
			{
				StartCoroutine(LoadMenu());
			}
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
	
	void SetText()
	{
		scoreText.text = "Score: " + score.ToString();
		healthText.text = "Health:" +health.ToString("0") + "%";
	}
	
	IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(1);
        ao = SceneManager.LoadSceneAsync(0);
        ao.allowSceneActivation = true;
	}
}
