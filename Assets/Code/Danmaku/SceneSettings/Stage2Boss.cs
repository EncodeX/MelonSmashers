using UnityEngine;

namespace Code.Danmaku.SceneSettings {
    public class Stage2Boss : SceneSetting {

        public void AddActions(Scene scene) {
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("yellow").SetHealth(50000)
                    .SetAngle(-90).SetSpeed(16).SetSpeedOffset(-8).SetPrefabName("Boss")
                    .SetActionTime(120)//.AddPattern(_patternManager.GetPattern("never_shoot"))
                    .AddAction().SetHealthThreshold(0.8f).SetSpeed(0)
                    .AddPattern("random_shooting")
                    .AddPattern("random_shooting")
                    .AddPattern("random_shooting")
                    .AddPattern("chop_boss")
                    .AddAction().SetHealthThreshold(0.6f).SetSpeed(0)
                    .AddPattern("backward_shooting")
                    .AddAction().SetHealthThreshold(0.3f).SetSpeed(0)
                    .AddPattern("backward_ring")
//                    .AddPattern("7_way_acc")
//                    .AddAction().SetHealthThreshold(0.2f).SetSpeed(0)
//                    .AddPattern("4_way_spiral")
//                    .AddAction().SetSpeed(0)
//                    .AddPattern("4_way_spiral_acc")
//                    .AddPattern("p000")
                    .Build()
            );
        }
    }
}