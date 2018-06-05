using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Danmaku;
using UnityEngine;
using UnityEngine.UI;

public class Avatar_Control : MonoBehaviour, Collider2DIntf {
	
	public GameObject go;
	public Vector3 Av_Tr;
	public GameObject Cam;
	private Rect _cameraRect;
	public GameObject _rockBull;
	public GameObject _scissBull;
	public GameObject _paperBull;
	public GameObject slash;
	public GameObject slashA;
	public GameObject slashS;
	public GameObject waveLL;
	public GameObject waver;
	public GameObject clear;
	public bool fired;
	public float presently;
	public float invPresent;
	public float comboTime;
	public float Health;
	public float Energy;
	public float Heat;
	public Slider thy;
	public Slider power;
	public Slider burn;
	public GameObject _thythy;
	public GameObject _powpow;
	public bool invFrame;
	public bool powerLvlOne;
	public bool powerLvlTwo;
	public bool andAHalf;
	public float powerTime;
	public GameObject _upPow;
	public GameObject drone;
	public float itemTime;
	public GameObject tmpA;
	public GameObject tmpB;
	public GameObject tmpC;

	public float Score;
	public Text scoring;
	
	private int _currentPatternType;
	private float[] _patternWaitTime;
	
	
	// Use this for initialization
	void Start () {
		Av_Tr = gameObject.transform.position;
		go = gameObject;
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		_cameraRect = Utils.GetGamePlayRect(Camera.main);
		_rockBull = Resources.Load("_Rock") as GameObject;
		_scissBull = Resources.Load("_Scissors") as GameObject;
		_paperBull = Resources.Load("_Paper") as GameObject;
		slash = Resources.Load("Melee") as GameObject;
		slashA = Resources.Load("MeleeA") as GameObject;
		slashS = Resources.Load("MeleeS") as GameObject;
		waveLL = Resources.Load("WaveLeft") as GameObject;
		waver = Resources.Load("WaveRight") as GameObject;
		clear = Resources.Load("Clearing") as GameObject;
		drone = Resources.Load("Drone") as GameObject;
		fired = false;
		presently = Time.time;
		Health = 100;
		//thy = FindObjectsOfType<Slider>()[1];
		//power = FindObjectsOfType<Slider>()[0];
		thy = FindObjectOfType<Slider>();
		Energy = 20;
		Heat = 20;
		_thythy = GameObject.FindGameObjectWithTag("Healthbar");
		_powpow = GameObject.FindGameObjectWithTag("Energybar");
		Score = 0;
		scoring = GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<Text>();
		invFrame = false;
		invPresent = Time.time;
		powerLvlOne = false;
		powerLvlTwo = false;
		andAHalf = false;
		comboTime = Time.time;
		_currentPatternType = 0;
		_patternWaitTime = new[] {0.05f, 0.08f, 0.08f};
		powerTime = 0;
		_upPow = GameObject.FindGameObjectWithTag("powTime");
		itemTime = Time.time;
		tmpA = transform.Find("Ava_Body").gameObject;
		tmpB = transform.Find("Ava_Other").gameObject;
		tmpC = transform.Find("Ava_Sides").gameObject;
	}

	private void OnCollisionEnter(Collision other)
	{
		OnCollision(other.gameObject);
	}

	public void OnCollision(GameObject gObject) {
		if (gObject.layer == 12) {
			Health = Health + 10 > 100 ? 100 : Health + 10;
		}
		if (gObject.layer == 13)
		{
			if (powerLvlTwo == false && powerLvlOne)
				powerLvlTwo = true;
			if (powerLvlOne == false)
				powerLvlOne = true;
			powerTime = 24;
		}
		if (invFrame)
		{
			if (Time.time > invPresent + 1f)
				invFrame = false;
		}
		if (!invFrame && gObject.layer != 12 && gObject.layer != 13) {
			invFrame = true;
			invPresent = Time.time;
			Health -= 5;
			//Color tmp;
			//tmp = new Color(tmpA.GetComponent<MeshRenderer>().material.color.r, tmpA.GetComponent<MeshRenderer>().material.color.g, tmpA.GetComponent<MeshRenderer>().material.color.b, 0.0f);
			//tmpA.GetComponent<MeshRenderer>().material.color = tmp;
			//tmp = new Color(tmpB.GetComponent<MeshRenderer>().material.color.r, tmpB.GetComponent<MeshRenderer>().material.color.g, tmpB.GetComponent<MeshRenderer>().material.color.b, 0.0f);
			//tmpB.GetComponent<MeshRenderer>().material.color = tmp;
			//tmp = new Color(tmpC.GetComponent<MeshRenderer>().material.color.r, tmpC.GetComponent<MeshRenderer>().material.color.g, tmpC.GetComponent<MeshRenderer>().material.color.b, 0.0f);
			//tmpC.GetComponent<MeshRenderer>().material.color = tmp;
			if (gObject.layer == 10)
			{
				Health -= 5;
				Score++;
			}
			else
			{
				Energy += 2;
			}
			//thy.value = Health;
			if (Health <= 5)
			{
				Destroy(gameObject);
				//thy.value = 0f;
				_thythy.transform.position = new Vector3(_thythy.transform.position.x, -24, _thythy.transform.position.z);
#if UNITY_EDITOR
//				EditorApplication.isPlaying = false;
#endif
				DanmakuController.Instance.Lose();
			}
		}
		float tempHe = 0.24f * (100 - Health);
		_thythy.transform.position = new Vector3(_thythy.transform.position.x, -tempHe, _thythy.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
			
			
		if (powerTime <= 0)
		{
			if (powerLvlTwo)
			{
				powerTime = 24;
				powerLvlTwo = false;
			}
			else if (powerLvlOne)
			{
				powerLvlOne = false;
			}
		}
		else if (powerTime > 0)
		{
			powerTime -= Time.deltaTime;
		
		}
		_upPow.transform.position = new Vector3(-18 + powerTime*0.75f,_upPow.transform.position.y,_upPow.transform.position.z);
		//if (thy.value < 120f)
		//{
		//	thy.value = Time.time;
		//}
		if (Energy < 20)
		{
			Energy += 0.8f*Time.deltaTime;
			float tempEn = 1.2f * (20 - Energy);
			//power.value = Energy;
			_powpow.transform.position = new Vector3(_powpow.transform.position.x, -tempEn, _powpow.transform.position.z);
		}
		if (Energy > 15 && Health < 100)
		{
			Health += 0.5f*Time.deltaTime;
			float tempHe = 0.24f * (100 - Health);
			_thythy.transform.position = new Vector3(_thythy.transform.position.x, -tempHe, _thythy.transform.position.z);
		}
		if (Score >= 1000)
			scoring.text = "Score: " + Mathf.Floor(Score);
		else if (Score >= 100)
			scoring.text = "Score: 0" + Mathf.Floor(Score);
		else if (Score >= 10)
			scoring.text = "Score: 00" + Mathf.Floor(Score);
		else
			scoring.text = "Score: 000" + Mathf.Floor(Score);
		
		
		if(DanmakuController.Instance.Playing && DanmakuController.Instance.Paused) return;
		
		if (Input.GetKey("up"))
		{
			Move(new Vector2(0, 7.5f * Time.deltaTime));
		}
		if (Input.GetKey("down"))
		{
			Move(new Vector2(0, -7.5f * Time.deltaTime));
		}
		if (Input.GetKey("left"))
		{
			Move(new Vector2(-7.5f * Time.deltaTime, 0));
		}
		if (Input.GetKey("right"))
		{
			Move(new Vector2(7.5f * Time.deltaTime, 0));
		}
		if (invFrame && Time.time <= invPresent + 1f)
		{
			//if (tmpA.GetComponent<MeshRenderer>().material.color.a < 1)
			//{
				//tmpA.GetComponent<MeshRenderer>().material.color += new Color(0.0f, 0.0f, 0.0f, 0.8f * Time.deltaTime);
				tmpA.GetComponent<MeshRenderer>().material.color = new Color(tmpA.GetComponent<MeshRenderer>().material.color.r, tmpA.GetComponent<MeshRenderer>().material.color.g,
					tmpA.GetComponent<MeshRenderer>().material.color.b, Mathf.PingPong(Time.time*2.5f, 0.4f) + 0.25f);
			//}
			//if (tmpB.GetComponent<MeshRenderer>().material.color.a < 1)
			//{
				//tmpB.GetComponent<MeshRenderer>().material.color += new Color(0.0f, 0.0f, 0.0f, 0.8f * Time.deltaTime);
				tmpB.GetComponent<MeshRenderer>().material.color = new Color(tmpB.GetComponent<MeshRenderer>().material.color.r, tmpB.GetComponent<MeshRenderer>().material.color.g,
					tmpB.GetComponent<MeshRenderer>().material.color.b, Mathf.PingPong(Time.time*2.5f, 0.4f) + 0.25f);
			//}
			//if (tmpC.GetComponent<MeshRenderer>().material.color.a < 1)
			//{
				//tmpC.GetComponent<MeshRenderer>().material.color += new Color(0.0f, 0.0f, 0.0f, 0.8f * Time.deltaTime);
				tmpC.GetComponent<MeshRenderer>().material.color = new Color(tmpC.GetComponent<MeshRenderer>().material.color.r, tmpC.GetComponent<MeshRenderer>().material.color.g,
					tmpC.GetComponent<MeshRenderer>().material.color.b, Mathf.PingPong(Time.time*2.5f, 0.4f) + 0.25f);
			//}
		}
		else
		{
				tmpA.GetComponent<MeshRenderer>().material.color = new Color(tmpA.GetComponent<MeshRenderer>().material.color.r, tmpA.GetComponent<MeshRenderer>().material.color.g,
					tmpA.GetComponent<MeshRenderer>().material.color.b, 1f);
				tmpB.GetComponent<MeshRenderer>().material.color = new Color(tmpB.GetComponent<MeshRenderer>().material.color.r, tmpB.GetComponent<MeshRenderer>().material.color.g,
					tmpB.GetComponent<MeshRenderer>().material.color.b, 1f);
				tmpC.GetComponent<MeshRenderer>().material.color = new Color(tmpC.GetComponent<MeshRenderer>().material.color.r, tmpC.GetComponent<MeshRenderer>().material.color.g,
					tmpC.GetComponent<MeshRenderer>().material.color.b, 1f);
		}
		if (Energy > 0) {
			var powerlv = 1.0f;
			if (Input.GetKey("a"))
			{
				if (fired == false)
				{
					fired = true;
					
					if (powerLvlTwo)
					{
						GameObject attack = Instantiate(_rockBull, gameObject.transform.position, gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = _currentPatternType;
						GameObject attacks = Instantiate(_rockBull,
							new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attacks.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						GameObject attackd = Instantiate(_rockBull,
							new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attackd.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						attack.GetComponent<Av_Bullet>().PowerLevel = 2;
						attackd.GetComponent<Av_Bullet>().PowerLevel = 2;
						attacks.GetComponent<Av_Bullet>().PowerLevel = 2;
						powerlv = 1.3f;
					}
					else if (powerLvlOne)
					{
						GameObject attack = Instantiate(_rockBull,
							new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						GameObject attacks = Instantiate(_rockBull,
							new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attacks.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						attack.GetComponent<Av_Bullet>().PowerLevel = 1;
						attacks.GetComponent<Av_Bullet>().PowerLevel = 1;
						powerlv = 1.15f;
					}
					else
					{
						GameObject attack = Instantiate(_rockBull, gameObject.transform.position, gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = _currentPatternType;
						attack.GetComponent<Av_Bullet>().PowerLevel = 0;
					}
					//attack.transform.SetParent(gameObject.transform, false);
					presently = Time.time;
					Energy -= 0.12f;
				}
				if (presently + _patternWaitTime[(_currentPatternType + 2) % 3] < Time.time)
				{
					fired = false;
				}
			}
			if (Input.GetKey("s"))
			{
				if (fired == false)
				{
					fired = true;
					
					if (powerLvlTwo)
					{
						GameObject attack = Instantiate(_scissBull, gameObject.transform.position, gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = _currentPatternType;
						GameObject attacks = Instantiate(_scissBull,
							new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attacks.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						GameObject attackd = Instantiate(_scissBull,
							new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attackd.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						attack.GetComponent<Av_Bullet>().PowerLevel = 2;
						attackd.GetComponent<Av_Bullet>().PowerLevel = 2;
						attacks.GetComponent<Av_Bullet>().PowerLevel = 2;
						powerlv = 1.3f;
					}
					else if (powerLvlOne)
					{
						GameObject attack = Instantiate(_scissBull,
							new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						GameObject attacks = Instantiate(_scissBull,
							new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attacks.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						attack.GetComponent<Av_Bullet>().PowerLevel = 1;
						attacks.GetComponent<Av_Bullet>().PowerLevel = 1;
						powerlv = 1.15f;
					}
					else
					{
						GameObject attack = Instantiate(_scissBull, gameObject.transform.position, gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = _currentPatternType;
						attack.GetComponent<Av_Bullet>().PowerLevel = 0;
					}
					//attack.transform.SetParent(gameObject.transform, false);
					presently = Time.time;
					Energy -= 0.12f;
				}
				if (presently + _patternWaitTime[(_currentPatternType + 2) % 3] < Time.time)
				{
					fired = false;
				}
			}
			if (Input.GetKey("d"))
			{
				if (fired == false)
				{
					fired = true;
					if (powerLvlTwo)
					{
						GameObject attack = Instantiate(_paperBull, gameObject.transform.position, gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = _currentPatternType;
						GameObject attacks = Instantiate(_paperBull,
							new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attacks.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						GameObject attackd = Instantiate(_paperBull,
							new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attackd.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						attack.GetComponent<Av_Bullet>().PowerLevel = 2;
						attackd.GetComponent<Av_Bullet>().PowerLevel = 2;
						attacks.GetComponent<Av_Bullet>().PowerLevel = 2;
						powerlv = 1.3f;
					}
					else if (powerLvlOne)
					{
						GameObject attack = Instantiate(_paperBull,
							new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						GameObject attacks = Instantiate(_paperBull,
							new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y,
								gameObject.transform.position.z), gameObject.transform.rotation);
						attacks.GetComponent<Av_Bullet>().PatternType = (_currentPatternType + 1) % 3;
						attack.GetComponent<Av_Bullet>().PowerLevel = 1;
						attacks.GetComponent<Av_Bullet>().PowerLevel = 1;
						powerlv = 1.15f;
					}
					else
					{
						GameObject attack = Instantiate(_paperBull, gameObject.transform.position, gameObject.transform.rotation);
						attack.GetComponent<Av_Bullet>().PatternType = _currentPatternType;
						attack.GetComponent<Av_Bullet>().PowerLevel = 0;
					}
					//attack.transform.SetParent(gameObject.transform, false);
					presently = Time.time;
					Energy -= 0.12f;
				}
				if (presently + _patternWaitTime[(_currentPatternType + 2) % 3] < Time.time)
				{
					fired = false;
				}
			}

			if (Input.GetKey("q") || Input.GetKey("w")) {
				_currentPatternType = _currentPatternType == 0 ? 3 : _currentPatternType;
				_currentPatternType--;
			}

			if (Input.GetKey("r") || Input.GetKey("e")) {
				_currentPatternType++;
				_currentPatternType = _currentPatternType == 3 ? 0 : _currentPatternType;
			}

			if (Energy >= 6)
			{
				if (Input.GetKeyDown("v"))
				{
					GameObject assaultla = Instantiate(clear, new Vector3(transform.position.x, transform.position.y - 150, transform.position.z), clear.transform.rotation);
					Energy -= 6.0f;
				}
			}
			if (Energy >= 4)
			{
				if (Input.GetKeyDown("f"))
				{
/*					GameObject assault = Instantiate(slash, gameObject.transform.position, slash.transform.rotation);
					GameObject assaulta = Instantiate(slashA, gameObject.transform.position, slashA.transform.rotation);
					GameObject assaults = Instantiate(slashS, gameObject.transform.position, slashS.transform.rotation);
					GameObject assaultd = Instantiate(slashD, gameObject.transform.position, slashD.transform.rotation);
					GameObject assaultf = Instantiate(slashF, gameObject.transform.position, slashF.transform.rotation);
										*/
					GameObject assaultnu = Instantiate(slash, gameObject.transform.position, slash.transform.rotation);
					Energy -= 4.0f;

				}
				if (Input.GetKeyDown("x"))
				{
					GameObject assaultdr = Instantiate(drone, transform.position, drone.transform.rotation);
					Energy -= 4.0f;
				}
			}
			if (Energy >= 2)
			{
				if (Input.GetKeyDown("c"))
				{
					GameObject wavenu = Instantiate(waver, transform.position, transform.rotation);
					GameObject wavena = Instantiate(waver, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), transform.rotation);
					GameObject waveno = Instantiate(waveLL, transform.position, transform.rotation);
					GameObject waveni = Instantiate(waveLL, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), transform.rotation);
					Energy -= 2.0f;
				}
			}
		}
		
	}

	private void Move(Vector2 offset) {
		Vector2 newPos = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
		if (_cameraRect.Contains(newPos)) {
			transform.position = newPos;
		}
	}
}
