using UnityEngine;

namespace Code.Danmaku.SceneSettings {
    public class Stage1Scene0 : SceneSetting {
        private BulletPatternBuilder _patternManager;

        public void AddActions(Scene scene) {
            _patternManager = BulletPatternBuilder.GetInstance();
            // build a action queue for shooter using AddAction()
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("magenta") // "cyan" "yellow" or "magenta"
                    .SetDropItem(DropItemType.None).SetAngle(-90).SetSpeed(16)                     // this shooter will use a 16 speed at first 
                    .SetActionTime(120).SetSpeedOffset(-8) // then stops when its speed equals 0 (2 sec).
                    .AddPattern(_patternManager.GetPattern("never_shoot"))
                    .AddAction() // as soon as the speed goes to 0, its action
                    .SetSpeed(0).SetSpeedOffset(0) // changes to this one, which stay still in its
                    .AddPattern(_patternManager.GetPattern("never_shoot")) // position.
                    .Build() // you can also make it moving slowly by adding
            ); // a small speed offset value


            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-7, 13)).SetEnemyColor("cyan")
                    .SetDropItem(DropItemType.Powerup, 100).SetAngle(-90)
                    .SetSpeed(5)
                    .AddPattern(_patternManager.GetPattern("never_shoot"))
                    .Build()
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-3, 13)).SetEnemyColor("cyan")
                    .SetDropItem(DropItemType.Powerup, 100).SetAngle(-90)
                    .SetSpeed(5)
                    .AddPattern(_patternManager.GetPattern("never_shoot"))
                    .Build()
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(3, 13)).SetEnemyColor("yellow")
                    .SetDropItem(DropItemType.Health, 100).SetAngle(-90)
                    .SetSpeed(5)
                    .AddPattern(_patternManager.GetPattern("never_shoot"))
                    .Build()
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(7, 13)).SetEnemyColor("yellow")
                    .SetDropItem(DropItemType.Health, 100).SetAngle(-90)
                    .SetSpeed(5)
                    .AddPattern(_patternManager.GetPattern("p001"))
                    .Build()
            );
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(6, 13)).SetEnemyColor("magenta")
                    .SetAngle(-90).SetSpeed(10)
                    .AddPattern(_patternManager.GetPattern("aim_once_after_2s"))
                    .SetDelay(6 * 60).Build(),
                25,
                12
            );
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-10, 4)).SetEnemyColor("cyan")
                    .SetAngle(0).SetSpeed(10)
                    .AddPattern(_patternManager.GetPattern("aim_once_after_2s"))
                    .SetDelay(12 * 60).Build(),
                25,
                12
            );
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(7, 13)).SetEnemyColor("magenta")
                    .SetAngle(-90)
                    .SetAngleOffset(-40)
                    .SetSpeed(10)
                    .AddPattern(_patternManager.GetPattern("p002"))
                    .SetDelay(19 * 60)
                    .Build(),
                45,
                10
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-5, 13))
                    .SetAngle(-90)
                    .SetSpeed(8)
                    .SetSpeedOffset(-2)
                    .AddPattern(_patternManager.GetPattern("p006"))
                    .SetDelay(28 * 60)
                    .Build()
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(5, 13)).SetEnemyColor("cyan")
                    .SetAngle(-90)
                    .SetSpeed(8)
                    .SetSpeedOffset(-2)
                    .AddPattern(_patternManager.GetPattern("p006"))
                    .SetDelay(28 * 60)
                    .Build()
            );
        }
    }
}