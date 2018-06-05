using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class Drone_Att : MonoBehaviour, Collider2DIntf
{

	public float leftOrRight;

	public float forward;
	// Use this for initialization
	void Start ()
	{
	}
	
	private void OnCollisionEnter(Collision other)
	{
		//Destroy(other.gameObject);

		OnCollision(other.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(leftOrRight*Time.deltaTime,forward*Time.deltaTime, 0);
	}
	
	void Die()
	{
		Destroy(gameObject);
	}

	public void OnCollision(GameObject gObject)
	{
			Die();
	}
}

