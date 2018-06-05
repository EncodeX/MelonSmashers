using Code.Danmaku;
using UnityEngine;
using UnityEngine.UI;

namespace Code {
    public partial class UIManager : IManager
    {
        public static Transform Canvas { get; private set; }

        private StartMenu _main;
        private PauseMenu _pause;

        public bool InMainMenu { get { return _main != null && _main.Showing; } }

        public UIManager () {
            Canvas = GameObject.Find("Canvas").transform; // There should only ever be one canvas
        }

        public void ShowMainMenu () {
            _main = new StartMenu();
            _main.Show();
            if (_pause != null) {
                GameResumed();
            }
			GameObject.Find ("Tutorial").GetComponent<Text>().enabled = false;
            GameObject.Find ("WinMsg").GetComponent<Text>().enabled = false;
            GameObject.Find ("LoseMsg").GetComponent<Text>().enabled = false;
        }

        public void HideMainMenu () {
            _main.Hide();
            _main = null;
            if (_pause != null) {
                GameResumed();
            }
			var _tutorial = GameObject.Find ("Tutorial").GetComponent<Text> ();
			_tutorial.enabled = true;
			_tutorial.text = DanmakuController.Instance._tutorialTexts [0];
            GameObject.Find ("WinMsg").GetComponent<Text>().enabled = false;
            GameObject.Find ("LoseMsg").GetComponent<Text>().enabled = false;
        }

        public void Win() {
            GameObject.Find ("WinMsg").GetComponent<Text>().enabled = true;
			GameObject.Find ("Tutorial").GetComponent<Text>().enabled = false;
        }

        public void Lose() {
            GameObject.Find ("LoseMsg").GetComponent<Text>().enabled = true;
			GameObject.Find ("Tutorial").GetComponent<Text>().enabled = false;
        }

//        public void Pause () {
//            _pause = new PauseMenu();
//            _pause.Show();
//        }
//
//        public void Unpause () {
//            _pause.Hide();
//            _pause = null;
//        }

        public void GameStart () { HideMainMenu(); }

        public void GameEnd () { ShowMainMenu(); }

        public void GamePaused() {
            _pause = new PauseMenu();
            _pause.Show();
        }
        
        public void GameResumed() {
            _pause.Hide();
            _pause = null;
        }

        private abstract class Menu
        {
            protected GameObject Go;
            public bool Showing { get; private set; }

            /// <summary>
            /// Show this menu
            /// </summary>
            public virtual void Show () {
                Showing = true;
                Go.SetActive(true);
            }

            /// <summary>
            /// Hide this menu
            /// </summary>
            public virtual void Hide () {
                GameObject.Destroy(Go);
                Showing = false;
            }
        }
    }
}