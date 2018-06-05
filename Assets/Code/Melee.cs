using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class Melee : MonoBehaviour, Collider2DIntf {

	public float direction = 0;
        
	public float Speed = 10;
	public float Acceleration = 0;
	public float AngularAccel = 0;

	public float Lifespan;
	public float LivedFrame = 0;
	public bool dead;
	public int killcount;

	private Vector2 upPos;
	
	
	// Use this for initialization
	void Start ()
	{
		Lifespan = Time.time;
		killcount = 0;
	}

	private void OnCollisionEnter(Collision other)
	{
		//Destroy(other.gameObject);

		OnCollision(other.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
/*		if (Time.time > Lifespan + 0.25)
		{
			Die();
		}
		if (CompareTag("skewR"))
		{
			upPos = new Vector2(8.5f * Time.deltaTime, 23f * Time.deltaTime);
		}
		else if (CompareTag("skewL"))
		{
			upPos = new Vector2(-8.5f * Time.deltaTime, 23f * Time.deltaTime);
		}
		else if (CompareTag("sideR"))
		{
			upPos = new Vector2(16f * Time.deltaTime, 19f * Time.deltaTime);
		}
		else if (CompareTag("sideL"))
		{
			upPos = new Vector2(-16f * Time.deltaTime, 19f * Time.deltaTime);
		}
		else
		{
			upPos = new Vector2(0, 25f * Time.deltaTime);
		}
		
		Move(upPos);
		Vector2 tempPos = new Vector2(transform.position.x + upPos.x, transform.position.y + upPos.y);
		Vector3 temperPos = new Vector3(tempPos.x, tempPos.y, 0);
		*/
		gameObject.transform.localScale += new Vector3(1f, 1f, 0);
		if (Time.time > Lifespan + 0.45f)
		{
			Die();
		}
	}
	
	private void Move(Vector2 offset) {
		Vector2 newPos = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
		transform.position = new Vector3(newPos.x, newPos.y, 0);
	}
	
	void Die()
	{
		Destroy(gameObject);
	}

	public void OnCollision(GameObject gObject)
	{
//		Die();
		killcount++;
		if (killcount > 100)
		{
			Die();
		}
	}
}
