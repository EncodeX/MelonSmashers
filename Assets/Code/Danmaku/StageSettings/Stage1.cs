using System.Collections.Generic;
using Code.Danmaku.SceneSettings;
using UnityEngine;

namespace Code.Danmaku.StageSettings {
    public class Stage1:StageSetting {
        public Stage1() {
            UnitySceneName = "Melon One";
        }

        public override Scenario GetScenario(DanmakuController parent) {
            var scenario = ScriptableObject.CreateInstance<Scenario>();
            scenario.Init(parent);
            
            var scene = new Scene {EnterFrame = 0, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 1 * 60};
            new Stage1Scene0 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 1, MaxDuration = 600 * 60, MinDuration = 1 * 60, CoolingTime = 2 * 60};
            new Stage1Scene1 ().AddActions (scene);
            scenario.AddScene(scene);
            
            scenario.SortScenes();
            return scenario;
        }
    }
}