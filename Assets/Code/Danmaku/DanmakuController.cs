using Code.Danmaku.SceneSettings;
using Code.Danmaku.Triggers;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using Code.Danmaku.StageSettings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Danmaku {
    public interface IManager {
        void GameStart();
        void GameEnd();
    }

    /// <inheritdoc />
    /// <summary>
    /// Danmaku Controller
    /// Main controller of the bullet system
    /// </summary>
    public class DanmakuController : MonoBehaviour {
        public static DanmakuController Instance;

		public List<string> _tutorialTexts = new List<string>
		{
			"Welcome! The objective of this game is to shoot down the cyan, magenta, and yellow enemies, as seen below. Use a, s, and d respectively, and the arrow keys to move around! All bullets will affect all enemies, but same-colored bullets will give more damage to same-colored enemies",
			"Be careful, enemies can shoot at you too!",
			"Powerups can help out in sticky situations! In your arsenal, you have forward spiral attacks, wave beams, lasers, and bombs! Use x, c, v, and f respectively. Watch out for your energy consumption!",
			"Enemies will sometimes drop items for you. Green items will give you some health, while blue items will give you an extra bullet for a limited amount of time!",
			"Good luck! :)"
		};
		public int _tutorialCount = 0;

        // Linked bullet manager / bullet layer
        public FlyObjectsController FlyObjectsController;
        public ShooterController ShooterController;
        public Avatar_Control Avatar;
        public StageSetting StageSetting;
        private BulletPatternBuilder _patternManager;
//        private UIManager _uiManager;

        public bool Paused = true;
        public bool Playing = false;

        private Scenario _scenario;

        private GameObject _background;
        private GameObject _avatar;
	    private GameObject _enemyHealthBar;
        private GameObject _menuButton;
        private GameObject _replayButton;
        private GameObject _nextButton;
        private GameObject _resumeButton;
        private GameObject _winMsg;
        private GameObject _loseMsg;

        private double _lastFrameTime;
        private bool _needInternalTimer;

        double GetTimeSinceStartup() {
#if UNITY_EDITOR
            return EditorApplication.timeSinceStartup;
#else
			return Time.timeSinceLevelLoad;
#endif
        }

        private void Awake() {
            Instance = this;
//            _uiManager = new UIManager();
            _needInternalTimer = Utils.CheckNeedInternalTimer();

            _lastFrameTime = GetTimeSinceStartup();

            // init all controllers at start up
            FlyObjectsController.Init(this);
            ShooterController.Init(this);
        }

        private void Start() {
            _lastFrameTime = GetTimeSinceStartup();
            _background = GameObject.FindGameObjectWithTag("Background");
	        _enemyHealthBar = GameObject.FindGameObjectWithTag("EnemyHealth");
            _menuButton = GameObject.Find("MenuButton");
            _replayButton = GameObject.Find("ReplayButton");
            _nextButton = GameObject.Find("NextButton");
            _resumeButton = GameObject.Find("ResumeButton");
            _winMsg = GameObject.Find("WinMsg");
            _loseMsg = GameObject.Find("LoseMsg");
//            EndGame();
            _resumeButton.GetComponent<Button>().onClick.AddListener(Resume);
            StartGame();
        }

        private void Update() {
            if (Input.GetKey("r") && Playing) {
                SceneManager.LoadScene(StageSetting.UnitySceneName);
                return;
            }
            
            if (Input.GetKey("p") && !Paused) {
                Pause();
                return;
            }
            
            double currentTime = GetTimeSinceStartup();

            if (_needInternalTimer || Application.isEditor) {
                double delta = currentTime - _lastFrameTime;
                // check if we're at a different frame
                if (delta < Utils.frameInterval) {
                    return;
                }

                // equal to frameInterval < delta && delta < frameInterval * 2
                // check if we're in the frame
                if (_lastFrameTime < currentTime - Utils.frameInterval &&
                    _lastFrameTime > currentTime - Utils.frameInterval * 2) {
                    _lastFrameTime += Utils.frameInterval;
                }
                else {
                    // we're "skipping" frames
                    _lastFrameTime = currentTime;
                }

                if (!Paused) UpdateFrame();
            }
            else {
                // Yay! We don't have to care about timer!
                if (!Paused) UpdateFrame();
            }
        }

        private void StartFrame() {
            FlyObjectsController.ClearObjects();

            ShooterController.ClearShooters();

            if (_avatar != null) {
                Avatar = null;
                Destroy(_avatar);
            }
            
            _avatar = Instantiate(Resources.Load<GameObject>("Avatar"));
            Avatar = _avatar.GetComponent<Avatar_Control>();
            Avatar.Health = 100;
            Avatar.Energy = 20;

            // Init shooters and their patterns here...
            InitPatterns();
            _scenario = ScriptableObject.CreateInstance<Scenario>();
            _scenario.Init(this);
            AddScene();
        }

        public void StartGame() {
            _menuButton.SetActive(false);
            _replayButton.SetActive(false);
            _resumeButton.SetActive(false);
            _nextButton.SetActive(false);
            _winMsg.SetActive(false);
            _loseMsg.SetActive(false);
            Paused = false;
//            _uiManager.GameStart();
            StartFrame();
            Playing = true;
        }

        public void EndGame() {
            FlyObjectsController.ClearObjects();
            ShooterController.ClearShooters();
            Paused = true;
//            _uiManager.GameEnd();
            
            Playing = false;

            if (_avatar != null) {
                Avatar = null;
                Destroy(_avatar);
            }

//            _avatar = Instantiate(Resources.Load<GameObject>("Avatar"));
//            Avatar = _avatar.GetComponent<Avatar_Control>();
        }

        public void Win() {
            EndGame();
            _menuButton.SetActive(true);
            _replayButton.SetActive(true);
            _nextButton.SetActive(true);
            _winMsg.SetActive(true);
//            _uiManager.Win();
            
        }

        public void Lose() {
            EndGame();
            _menuButton.SetActive(true);
            _replayButton.SetActive(true);
            _loseMsg.SetActive(true);
//            _uiManager.Lose();
        }

        public void Pause() {
            Paused = true;
            _menuButton.SetActive(true);
            _replayButton.SetActive(true);
            _resumeButton.SetActive(true);
//            _uiManager.GamePaused();
        }

        public void Resume() {
            Paused = false;
            _menuButton.SetActive(false);
            _replayButton.SetActive(false);
            _resumeButton.SetActive(false);
//            _uiManager.GameResumed();
        }

	    public void OnNoShooter() {
		    _scenario.OnNoShooter();
	    }

        private void UpdateFrame() {
            // calculate new positions of all bullets
            FlyObjectsController.UpdateObjects();
            // update shooters' status and see if
            // there's any shooter wants to shoot
            ShooterController.UpdateShooters();

            _scenario.UpdateScene();

            var newY = _background.transform.position.y - 0.08f;
            newY = newY <= -12 ? newY + 36 : newY;

            _background.transform.position = new Vector3(
                _background.transform.position.x,
                newY,
                _background.transform.position.z
            );
        }

	    public void updateEnemyHealth(float position) {
		    _enemyHealthBar.transform.position = new Vector3(
			    position,
			    _enemyHealthBar.transform.position.y,
			    _enemyHealthBar.transform.position.z);
	    }

        public void AddScene() {
            _scenario = StageSetting.GetScenario(this);
        }

        private void InitPatterns() {
            _patternManager = BulletPatternBuilder.GetInstance();
            _patternManager.Clear();


            _patternManager.CreatePattern("p000").SetIntervalFrame(60);
            _patternManager.CreatePattern("p000_left")
                .SetPositionOffset(new Vector2(-2.0f, 0f));
            _patternManager.CreatePattern("p000_right")
                .SetPositionOffset(new Vector2(2.0f, 0f));
            _patternManager.CreatePattern("p001")
                .SetTriggerType(TriggerType.ONCE).SetDelayFrame(90);
            _patternManager.CreatePattern("p002")
                .SetLockedOnAvatar(false);
            _patternManager.CreatePattern("p003")
                .SetTrigger(new ShootEveryEightFrame());
            _patternManager.CreatePattern("p003_undestroyable")
                .SetTrigger(new ShootEveryEightFrame()).SetDestroyableProportion(0);
            _patternManager.CreatePattern("p004")
                .SetBulletCount(5).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("p005")
                .SetBulletCount(5);
            _patternManager.CreatePattern("p006")
                .SetBulletCount(3).SetTrigger(new ShootEveryFourFrame())
                .SetIntervalFrame(144)
                .SetAngleOffset(10).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("p007")
                .SetBulletCount(4).SetTrigger(new ShootEveryEightFrame())
                .SetCoolDownFrame(30);
            _patternManager.CreatePattern("p007_partial_destroyable")
                .SetBulletCount(4).SetTrigger(new ShootEveryTwelveFrame())
                .SetCoolDownFrame(60).SetDestroyableProportion(30);
            _patternManager.CreatePattern("never_shoot")
                .SetTrigger(new NeverShoots());
            _patternManager.CreatePattern("aim_once_after_2s")
                .SetTriggerType(TriggerType.ONCE).SetDelayFrame(120);
            _patternManager.CreatePattern("aim_once_after_1s")
                .SetTriggerType(TriggerType.ONCE).SetDelayFrame(60);
            _patternManager.CreatePattern("polar_spiral")
                .SetBulletCount(2).SetTrigger(new ShootEveryFourFrame())
                .SetIntervalFrame(144)
                .SetBulletAngle(180).SetAngleOffset(10).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("4_way_spiral")
                .SetBulletCount(4).SetTrigger(new ShootEveryFourFrame())
                .SetIntervalFrame(96)
                .SetBulletAngle(90).SetAngleOffset(13).SetLockedOnAvatar(false)
                .SetDestroyableProportion(0);
            _patternManager.CreatePattern("4_way_spiral_acc")
                .SetBulletCount(4).SetTrigger(new ShootEveryFourFrame())
                .SetIntervalFrame(144)
                .SetBulletAngle(90).SetAngleOffset(10).SetLockedOnAvatar(false)
                .SetDestroyableProportion(0).SetAngleOffsetStepper(0.1f);
            _patternManager.CreatePattern("8_way_blocking")
                .SetBulletCount(8).SetTrigger(new ShootEveryFourFrame())
                .SetBulletAngle(45).SetLockedOnAvatar(true)
                .SetDestroyableProportion(50);
            _patternManager.CreatePattern("1_way_aiming_randomized_CD")
                .SetTrigger(new ShootEveryTwelveFrame())
                .SetCoolDownFrame(240).SetIntervalFrame(30).SetDelayFrame(90)
                .SetAngleRandomRange(new Vector2(-10, 10)).SetDestroyableProportion(0)
                .SetSpeedCorrection(0.5f);
            _patternManager.CreatePattern("2_way_spiral_CD")
                .SetTrigger(new ShootEveryFourFrame()).SetBulletCount(2)
                .SetCoolDownFrame(16).SetIntervalFrame(90).SetAngle(-90)
                .SetBulletAngle(180).SetAngleOffset(16).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("2_way_spiral_CD_0deg")
                .SetTrigger(new ShootEveryFourFrame()).SetBulletCount(2)
                .SetCoolDownFrame(16).SetIntervalFrame(90).SetAngle(0)
                .SetBulletAngle(180).SetAngleOffset(16).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("chop_-150deg_down")
                .SetTrigger(new ShootEveryTwoFrame()).SetBulletCount(1).SetSpeedCorrection(0.7f)
                .SetCoolDownFrame(150).SetIntervalFrame(30).SetAngle(-150).SetDelayFrame(60)
                .SetAngleOffset(6).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("chop_-30deg_down")
                .SetTrigger(new ShootEveryTwoFrame()).SetBulletCount(1).SetSpeedCorrection(0.7f)
                .SetCoolDownFrame(150).SetIntervalFrame(30).SetAngle(-30).SetDelayFrame(60)
                .SetAngleOffset(-6).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("chop_boss")
                .SetTrigger(new ShootEveryTwoFrame()).SetBulletCount(1).SetSpeedCorrection(0.7f)
                .SetCoolDownFrame(210).SetIntervalFrame(30).SetAngle(-15)
                .SetAngleOffset(-6).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("3_way_acc")
                .SetTrigger(new ShootEveryTwelveFrame()).SetBulletCount(3).SetSpeedCorrection(0.5f)
                .SetLockedOnAvatar(true).SetIntervalFrame(30).SetCoolDownFrame(120)
                .SetSpeedCorrectionOffset(0.01f).SetDelayFrame(40).SetAngleRandomRange(new Vector2(-15, 15));
            _patternManager.CreatePattern("ring")
                .SetTrigger(new ShootEveryTwelveFrame()).SetBulletCount(20)
                .SetSpeedCorrection(0.5f).SetSpeedCorrectionOffset(0.01f).SetLoopAngleOffset(5)
                .SetBulletAngle(18).SetIntervalFrame(10).SetCoolDownFrame(30)
                .SetDestroyableProportion(5).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("random_shooting")
                .SetAngle(-90).SetAngleRandomRange(new Vector2(-180, 180)).SetSpeedRandomRange(new Vector2(5, 15))
                .SetTrigger(new ShootEveryTwoFrame()).SetBulletCount(1).SetDestroyableProportion(20).SetLockedOnAvatar(false);
            _patternManager.CreatePattern("backward_shooting")
                .SetAngle(90).SetAngleRandomRange(new Vector2(-60, 60)).SetSpeedRandomRange(new Vector2(3, 8))
                .SetTrigger(new ShootEveryTwoFrame()).SetBulletCount(1).SetDestroyableProportion(20).SetLockedOnAvatar(false)
                .SetSpeedCorrectionOffset(-.01f);
            _patternManager.CreatePattern("backward_ring")
                .SetTrigger(new ShootEveryTwelveFrame()).SetBulletCount(40).SetSpeedCorrection(0.5f)
                .SetSpeedCorrection(0.5f).SetSpeedCorrectionOffset(-0.01f).SetLoopAngleOffset(5)
                .SetBulletAngle(9).SetIntervalFrame(48).SetAngleOffset(5)
                .SetDestroyableProportion(5).SetLockedOnAvatar(false).SetDelayFrame(30);
            _patternManager.CreatePattern("7_way_acc")
                .SetTrigger(new ShootEveryTwelveFrame()).SetBulletCount(7).SetSpeedCorrection(0.5f)
                .SetBulletAngle(7).SetAngleRandomRange(new Vector2(-10, 10))
                .SetLockedOnAvatar(true).SetIntervalFrame(60).SetCoolDownFrame(10)
                .SetSpeedCorrectionOffset(0.01f);
        }

        private Camera _mainCamera;

        public Camera MainCamera {
            set { _mainCamera = value; }
            get {
                if (_mainCamera != null)
                    return _mainCamera;
                return Camera.main;
            }
        }
    }
}