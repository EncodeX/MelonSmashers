using System.Collections.Generic;
using UnityEngine;

namespace Code.Danmaku {
    public abstract class StageSetting : MonoBehaviour {
        public string UnitySceneName;
        public abstract Scenario GetScenario(DanmakuController parent);
    }
}