using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class WaveBeam : MonoBehaviour, Collider2DIntf
{

	public float wave;
	public float deather;
	
	// Use this for initialization
	void Start ()
	{
		wave = 90;
		transform.position = new Vector3(transform.position.x + 0.5f,transform.position.y,transform.position.z);
		deather = Time.time;
	}
	
	private void OnCollisionEnter(Collision other)
	{
		//Destroy(other.gameObject);

		OnCollision(other.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (CompareTag("wavLeft"))
		{
			wave += 1600f * Time.deltaTime;

			wave = wave % 360;
			transform.position += new Vector3(Mathf.Cos(wave * Mathf.PI / 180), 25f * Time.deltaTime, 0);
		}
		else {
			wave += 1600f * Time.deltaTime;

			wave = wave % 360;
			transform.position += new Vector3(Mathf.Sin(wave * Mathf.PI / 180), 25f * Time.deltaTime, 0);
		}
		if (Time.time > deather + 3f)
			Die();
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
