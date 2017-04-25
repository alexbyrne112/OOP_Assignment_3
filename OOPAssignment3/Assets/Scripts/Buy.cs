using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour {

	public int vendorPrice = 500;
	public Text vendorText;
	public FPSController fpscontroller;
	public Shoot shoot;
	public bool isHealth;
	Transform myTransform;
	
	void OnTriggerStay(Collider other)
	{	
		if(other.transform.CompareTag("Player") && Input.GetButtonDown("Submit") && fpscontroller.GetComponent<FPSController>().score >= vendorPrice)
		{
			fpscontroller.GetComponent<FPSController>().score -= vendorPrice;
			if(isHealth == true)
			{
				fpscontroller.GetComponent<FPSController>().health += 15;
			}
			else
			{
				shoot.GetComponent<Shoot>().grenadeCount += 1;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.CompareTag("Player") && isHealth == true)
		{
			vendorText.text = "Buy Health: $500";
		}
		else if(other.transform.CompareTag("Player"))
		{
			vendorText.text = "Buy Grenade: $500";
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.transform.CompareTag("Player"))
		{
			vendorText.text = "";
		}
	}
}
