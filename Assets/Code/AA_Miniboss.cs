using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class AA_Miniboss : MonoBehaviour, Collider2DIntf {

	public int Health;
	
	// Use this for initialization
	void Start ()
	{
		Health = 250;
	}
	
	private void OnCollisionEnter(Collision other)
	{
		OnCollision(other.gameObject);
	}
	
	public void OnCollision(GameObject gObject)
	{
		Health -= 1;
		// if right color Health -=5
		// if (Health <= 0) {Die();}
	}
	
	void Die()
	{
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
