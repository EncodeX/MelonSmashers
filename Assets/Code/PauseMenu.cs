using Code.Danmaku;
using UnityEngine;
using UnityEngine.UI;

namespace Code {
    public partial class UIManager {
        private class PauseMenu : Menu {
            public PauseMenu() {
                // TODO fill me in
                if (Canvas != null) {
                    Go = (GameObject) Object.Instantiate(Resources.Load("PauseMenu"), Canvas.transform);
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
                        case "GameResumeBtn":
                            button.onClick.AddListener(() => DanmakuController.Instance.Resume());
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