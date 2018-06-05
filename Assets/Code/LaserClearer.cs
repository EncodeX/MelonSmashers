using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class LaserClearer : MonoBehaviour, Collider2DIntf
{

	public float ready;

	public bool areReady;
	// Use this for initialization
	void Start ()
	{
		ready = Time.time;
		areReady = false;
	}
	
	private void OnCollisionEnter(Collision other)
	{
		OnCollision(other.gameObject);
	}
	
	public void OnCollision(GameObject gObject)
	{
		if (Time.time > ready + 23f)
			Die();
	}
	
	void Die()
	{
		Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
		if (Time.time > ready + 1f)
		{
			areReady = true;
			}
		if (areReady)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 125f*Time.deltaTime, transform.position.z);
		}
		if (Time.time > ready + 10f)
		    Die();
	}
}
