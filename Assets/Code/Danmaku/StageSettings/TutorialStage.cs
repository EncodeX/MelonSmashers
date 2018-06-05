using System.Collections.Generic;
using Code.Danmaku.SceneSettings;
using UnityEngine;

namespace Code.Danmaku.StageSettings {
    public class TutorialStage:StageSetting {
        public TutorialStage() {
            UnitySceneName = "Melon Tutorial";
        }

        public override Scenario GetScenario(DanmakuController parent) {
            var scenario = ScriptableObject.CreateInstance<Scenario>();
            scenario.Init(parent);
            
            var scene = new Scene {EnterFrame = 0, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 1 * 60};
            new TutorialScene0 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 1, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 2 * 60};
            new TutorialScene1 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 2, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 2 * 60};
            new TutorialScene2 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 3, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 2 * 60};
            new TutorialScene3 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 3, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 2 * 60};
            new TutorialScene4 ().AddActions (scene);
            scenario.AddScene(scene);
            scene = new Scene {EnterFrame = 4, MaxDuration = 80 * 60, MinDuration = 1 * 60, CoolingTime = 2 * 60};
            scenario.AddScene(scene);
            
            scenario.SortScenes();
            return scenario;
        }
    }
}