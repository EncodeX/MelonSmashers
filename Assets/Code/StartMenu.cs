using Code.Danmaku;
using UnityEngine;
using UnityEngine.UI;

namespace Code {
    public partial class UIManager {
        private class StartMenu : Menu {
            public StartMenu() {
                // TODO fill me in
                if (Canvas != null) {
                    Go = (GameObject) Object.Instantiate(Resources.Load("StartMenu"), Canvas.transform);
                    InitializeButtons();
                }
            }

            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons() {
                // TODO fill me in
                foreach (Button button in Go.GetComponentsInChildren<Button>()) {
                    switch (button.name) {
                        case "GameStartBtn":
                            button.onClick.AddListener(() => DanmakuController.Instance.StartGame());
                            break;
                        case "Quit":
//                        button.onClick.AddListener(() => Game.Ctx.QuitGame());
                            break;
                    }
                }
            }
        }
    }
}