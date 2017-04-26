using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
	public FPSController fpscontroller;
	public float fireRate = 0.6f;
	public GameObject grenadePrefab;
	public float throwingForce;
	public EnemySpawn enemySpawn;
	public int grenadeCount;
	public Text grenadeCountText;
	
	private Transform myTransform;
	private float nextFire;
	private RaycastHit hit;
	private float range = 500;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shot;
    public AudioSource explode;
    public AudioSource ambience;

    void Start()
	{
		myTransform = transform;
		grenadeCount = 2;
        ambience.Play();
	}
	
	void Update()
	{
		if(Input.GetMouseButton(0) && Time.time > nextFire && fpscontroller.GetComponent<FPSController>().health > 0) //Fire1 corresponds to Mouse0 (left click)
		{
            shooting();
		}
		
        if(Input.GetMouseButtonDown(1) && grenadeCount > 0)
        {
			SpawnGrenade();
			grenadeCount --;
        }
		
		//Vector3 shotPosition = new Vector3(myTransform.position.x + 1, myTransform.position.y, myTransform.position.z);
		grenadeCountText.text = "Grenades: " + grenadeCount.ToString();
	}
	
    public void shooting()
    {
        muzzleFlash.Play();
        shot.Play();

        //TransformPoint allows you to set coordinates relative to transform
        Debug.DrawRay(myTransform.TransformPoint(0, 0, 1), myTransform.forward, Color.green, 1);

        //transform refers to the transform of the object it's attached to
        if (Physics.Raycast(myTransform.TransformPoint(0,0,1), myTransform.forward, out hit, range)) //the raycast returns information of the object it hits
        {
			
            if (hit.transform.CompareTag("Enemy") && hit.transform.GetComponent<EnemyScript>().health > 10)
            {
                hit.transform.GetComponent<EnemyScript>().health -= 10;
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
            else if (hit.transform.CompareTag("Enemy") && hit.transform.GetComponent<EnemyScript>().health <= 10)
            {
				hit.transform.GetComponent<Rigidbody>().isKinematic = false;
                hit.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                hit.transform.GetComponent<Rigidbody>().AddForce(-hit.transform.forward * 60, ForceMode.Impulse);
                hit.transform.GetComponent<EnemyScript>().Die();
				
				enemySpawn.GetComponent<EnemySpawn>().enemyCount -= 1;
				
                fpscontroller.GetComponent<FPSController>().score += 80;
            }
        }

        nextFire = Time.time + fireRate;
    }
	
	void SpawnGrenade()
	{
		//Creates a reference to the instantiated GameObject.
		GameObject go = (GameObject)Instantiate(grenadePrefab,myTransform.TransformPoint(0,0,0.5f),myTransform.rotation); //Using positions from transform of player.
		go.GetComponent<Rigidbody>().AddForce(myTransform.forward * throwingForce, ForceMode.Impulse); //Takes parameters of direction and force, force type
        explode.Play();
		Destroy(go,6); //Destroys game object after 6 seconds.
	}
}