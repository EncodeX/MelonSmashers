using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Danmaku {
    public class Scenario : ScriptableObject {
        private List<Scene> _scenes;
        private int _currentFrame;
        private DanmakuController _parent;
        private bool _noShooter = false;

        public Scenario() {
            _scenes = new List<Scene>();
            _currentFrame = 0;
        }

        public void Init(DanmakuController danmakuController) {
            _parent = danmakuController;
        }

        public void UpdateScene() {
            foreach (Scene scene in _scenes) {
                if (scene.Activated) {
                    _currentFrame = scene.UpdateActions(_currentFrame, _noShooter);
                    _noShooter = false;
                }
                else {
                    scene.Activated = true;
//                    GameObject.Find("Tutorial").GetComponent<Text>().text = "";
                    break;
                }
                
                if(scene.Acting) break;
            }

            //_scenes.RemoveAll(s => s.Ended);
			_scenes.RemoveAll(SceneEnded);
            if (_scenes.Count == 0) {
                _parent.Win();
            }
            ++_currentFrame;
        }

		private bool SceneEnded(Scene s) {
			if (s.Ended) {
				_parent._tutorialCount++;
				var _tutorial = GameObject.Find ("Tutorial").GetComponent<Text> ();
				if (_parent._tutorialCount >= _parent._tutorialTexts.Count) {
					_tutorial.text = "";
					_tutorial.enabled = false;
				} else {
					_tutorial.text = DanmakuController.Instance._tutorialTexts [_parent._tutorialCount];
				}
//			    _tutorial.text = "";
				return true;
			} else
				return false;
		}

        public void AddScene(Scene scene) {
            scene.Parent = this;
            scene.SortActions();
            _scenes.Add(scene);
        }

        public void AddShooter(Shooter shooter) {
            _parent.ShooterController.AddShooter(shooter);
        }

        public void ClearScenes() {
            _scenes.Clear();
        }

		public void OnNoShooter() {
            _noShooter = true;
        }

        public void SortScenes() {
            _scenes = _scenes.OrderBy(s => s.EnterFrame).ToList();
        }
    }
}