using System.Collections.Generic;
using Code.Danmaku.SceneSettings;
using UnityEngine;

namespace Code.Danmaku.StageSettings {
    public class Stage2:StageSetting {
        public Stage2() {
            UnitySceneName = "Melon Two";
        }

        public override Scenario GetScenario(DanmakuController parent) {
            var scenario = ScriptableObject.CreateInstance<Scenario>();
            scenario.Init(parent);
            
            var scene = new Scene {EnterFrame = 0, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 4 * 60};
            new Stage2Scene0().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 1, MaxDuration = 240 * 60, MinDuration = 1 * 60, CoolingTime = 4 * 60};
            new Stage2Scene1 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 1, MaxDuration = 240 * 60, MinDuration = 1 * 60, CoolingTime = 4 * 60};
            new Stage2Boss ().AddActions (scene);
            scenario.AddScene(scene);
            
            scenario.SortScenes();
            return scenario;
        }
    }
}