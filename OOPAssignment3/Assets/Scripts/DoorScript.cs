using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {

	public float timeToMove = 3f;
	public int doorPrice;
	public Text doorText;
	public FPSController fpscontroller;
	private bool isOpen;
	Transform myTransform;
	
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerStay(Collider other)
	{	
		if(other.transform.CompareTag("Player") && Input.GetButton("Submit") && isOpen == false && fpscontroller.GetComponent<FPSController>().score >= doorPrice)
		{
			StartCoroutine("MoveDoor");
			fpscontroller.GetComponent<FPSController>().score -= doorPrice;
			isOpen = true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.CompareTag("Player") && isOpen == false)
		{
			doorText.text = "$ " + doorPrice.ToString();
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.transform.CompareTag("Player"))
		{
			doorText.text = "";
		}
	}
	
	IEnumerator MoveDoor()
	{
		float t = 0f;
		Vector3 destination = new Vector3(myTransform.position.x, myTransform.position.y + 5, myTransform.position.z);
		while(t < 1)
		{
			t+=Time.deltaTime/timeToMove;
			transform.position = Vector3.Lerp(myTransform.position, destination, t);
			yield return null;
		}
	}
}
