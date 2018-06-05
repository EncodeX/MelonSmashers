using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code {
    public class MenuHandler : MonoBehaviour {
        public void OnGameStartClicked(string name) {
            SceneManager.LoadScene(name);
        }
    }
}