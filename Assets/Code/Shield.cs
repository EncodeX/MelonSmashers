using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class Shield : MonoBehaviour, Collider2DIntf
{

	public float shields;

	public float degrees;

	public Transform _parr;
	// Use this for initialization
	void Start ()
	{
		shields = 120f;
		degrees = 0f;
		_parr = gameObject.transform.parent;
	}
	
	private void OnCollisionEnter(Collision other)
	{
		OnCollision(other.gameObject);
	}

	public void OnCollision(GameObject gObject)
	{
		shields -= 10f;
	}

	// Update is called once per frame
	void Update ()
	{
		
		degrees += 24*Time.deltaTime;
		degrees = degrees % 360;
		if (CompareTag("roSh"))
		{
			transform.position = new Vector3(_parr.position.x + 4f*Mathf.Sin(degrees * Mathf.PI/180), _parr.position.y + 4f*Mathf.Cos(degrees * Mathf.PI/180), _parr.position.z);
		}
		if (CompareTag("paSh"))
		{
			var tegrees = degrees + 120f;
			tegrees = tegrees % 360;
			transform.position = new Vector3(_parr.position.x + 4f*Mathf.Sin(tegrees * Mathf.PI/180), _parr.position.y + 4f*Mathf.Cos(tegrees * Mathf.PI/180), _parr.position.z);
		}
		if (CompareTag("sciSh"))
		{
			var tegrees = degrees + 240f;
			tegrees = tegrees % 360;
			transform.position = new Vector3(_parr.position.x + 4f*Mathf.Sin(tegrees * Mathf.PI/180), _parr.position.y + 4f*Mathf.Cos(tegrees * Mathf.PI/180), _parr.position.z);
		}
	}
}
