using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Danmaku;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Av_Bullet : MonoBehaviour, Collider2DIntf {

	public float direction = 0;
        
	public float Speed = 10;
	public float Acceleration = 0;
	public float AngularAccel = 0;

	public float LifeFrame = 30;
	public float LivedFrame = 0;
	public string Color = "Magenta";

	public int PatternType = 0;
	public Avatar_Control _parental;
	public int PowerLevel = 0;
	
	private Rect _cameraRect;
	private Vector2 upPos;

	
	
	// Use this for initialization
	void Start () {
		_cameraRect = Utils.GetGamePlayRect(Camera.main);
		_parental = FindObjectOfType<Avatar_Control>();
	}

	private void OnCollisionEnter(Collision other)
	{
		OnCollision(other.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		upPos = new Vector2(0, 25f * Time.deltaTime);
		Move(upPos);
		Vector2 tempPos = new Vector2(transform.position.x + upPos.x, transform.position.y + upPos.y);
		Vector3 temperPos = new Vector3(tempPos.x, tempPos.y, 0);
		if (!_cameraRect.Contains(temperPos))
		{
			Die();
		}
		LivedFrame += Time.deltaTime;
	}
	
	private void Move(Vector2 offset) {
		Vector2 newPos = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
			transform.position = new Vector3(newPos.x, newPos.y, 0);
	}
	
	void Die()
	{
		Destroy(gameObject);
	}

	public void OnCollision(GameObject gObject) {
		if (gObject.layer == 8) {
			//if (PatternType != 1)
			//{
			_parental.Score += 0.25f;
			if(gObject.GetComponent<Bullet>().Destroyable) Die();
			//}
		}
		else {

			Die();
		}
//		Destroy(gObject);
	}
}
