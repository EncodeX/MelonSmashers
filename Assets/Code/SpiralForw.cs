using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;
using UnityEngine.Assertions.Comparers;

public class SpiralForw : MonoBehaviour, Collider2DIntf
{

	public float shoot;

	public float deathshoot;

	public float forwVel;

	public float slowTime;
	public GameObject droneShot;
	public float shotTime;
	public bool isLeft;
	public int killcount;
	
	// Use this for initialization
	void Start ()
	{
		deathshoot = Time.time;
		shotTime = Time.time;
		forwVel = 8f;
		slowTime = Time.time;
		droneShot = Resources.Load("Drone_Atk") as GameObject;
		isLeft = true;
		killcount = 0;
	}
	
	private void OnCollisionEnter(Collision other)
	{
		//Destroy(other.gameObject);

		OnCollision(other.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y + forwVel*Time.deltaTime, transform.position.z);
		if (Time.time > deathshoot + 7f)
			Die();
		if (Time.time > 0.25f + shotTime)
		{
			GameObject shooot = Instantiate(droneShot, transform.position, transform.rotation);
			if (isLeft)
			{
				shooot.GetComponent<Drone_Att>().leftOrRight = -2f;
			}
			else
			{
				shooot.GetComponent<Drone_Att>().leftOrRight = 2f;
			}
			shooot.GetComponent<Drone_Att>().forward = forwVel;
			isLeft = !isLeft;
			shotTime = Time.time;
		}
	}
	
	void Die()
	{
		Destroy(gameObject);
	}

	public void OnCollision(GameObject gObject)
	{
		forwVel = 4f;
		killcount++;
		if (killcount > 4)
			Die();
	}
}
